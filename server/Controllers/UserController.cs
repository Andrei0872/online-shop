using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Services.UserService;
using server.DTOs;
using server.Responses;
using Microsoft.AspNetCore.Authorization;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

    private IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register ([FromBody] RegisterUserDto registerUserDTO) {
            var response = new UserResponse();
        
            var userExists = await this._userService.UserExists(registerUserDTO);
            if (userExists) {
                // TODO: from a security point of view, it might not be a good idea to reveal this information.
                response.Error = "The user already exists.";
                return BadRequest(response);
            }

            var token = await this._userService.RegisterUserAsync(registerUserDTO);
            if (token != null) {
                response.Data.Message = "The user has been successfully registered.";
                response.Data.Token = token;

                return Ok(response);
            }

            response.Error = "A problem occurred while trying to register the";
            return BadRequest(response);
        }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login ([FromBody] LoginUserDto userDto) {
        var response = new UserResponse();
        
        string token = await this._userService.Login(userDto);
        if (token == null) {
            response.Error = "Something went wrong while trying to log in.";
            return Unauthorized(response);
        }

        response.Data.Message = "Successfully logged in.";
        response.Data.Token = token;
        return Ok(response);
    }
    }
}