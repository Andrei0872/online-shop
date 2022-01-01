using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.DTOs;
using server.Models;
using server.DAL.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using server.Helpers.Constants;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace server.Services.UserService
{
    public class UserService : IUserService
    {
    public byte[] PRIVATE_JWT_KEY = Encoding.UTF8.GetBytes("SECRET_KEY that should be longer than 32 bits");

    private UserManager<User> _userManager;
    private IUnitOfWork _unitOfWork;
    public UserService(UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await this._userManager.FindByEmailAsync(email);
    }

    public async Task<string> RegisterUserAsync(RegisterUserDto userDTO)
    {
        var registeredUser = new User();
        registeredUser.FirstName = userDTO.FirstName;
        registeredUser.LastName = userDTO.LastName;
        registeredUser.Email = userDTO.Email;
        registeredUser.UserName = userDTO.Email;

        var rawPassword = userDTO.Password;
        var res = await this._userManager.CreateAsync(registeredUser, rawPassword);
        if (!res.Succeeded) {
            return null;
        }

        await this._userManager.AddToRoleAsync(registeredUser, UserRoleType.User);

        return await this.GenerateJWT(registeredUser);
    }

    public async Task<bool> UserExists(RegisterUserDto userDTO)
    {
        return (await GetUserByEmail(userDTO.Email)) != null;
    }

    public async Task<string> Login (LoginUserDto userDto) {
        var existingUser = await GetUserByEmail(userDto.Email);

        var isNonExistingUser = existingUser == null;
        if (isNonExistingUser) {
            return null;
        }

        var isPasswordValid = await this._userManager.CheckPasswordAsync(existingUser, userDto.Password);
        if (!isPasswordValid) {
            return null;
        }

        return await this.GenerateJWT(existingUser);
    }

    private async Task<string> GenerateJWT (User user) {
        user = await this._unitOfWork.User.GetUserByIdWithRoles(user.Id);
        List<string> roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();

        var newJti = Guid.NewGuid().ToString();
        var tokenHandler = new JwtSecurityTokenHandler();
        var signinkey = new SymmetricSecurityKey(PRIVATE_JWT_KEY);

        var securityToken = GenerateSecurityToken(signinkey, user, roles, tokenHandler, newJti);

        this._unitOfWork.SessionToken.Insert(new SessionToken(newJti, user.Id, securityToken.ValidTo));
        await this._unitOfWork.SaveAsync();

        return tokenHandler.WriteToken(securityToken);

    }
    private SecurityToken GenerateSecurityToken(SymmetricSecurityKey signinkey, User user, List<string> roles, JwtSecurityTokenHandler tokenHandler, string newJti)
    {
        var subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, newJti)
        });

        foreach (var role in roles)
        {
            subject.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = subject,
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = new SigningCredentials(signinkey, SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return token;
        }
    }
}