using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using server.DTOs;
using server.Models;

namespace server.Services.UserService
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string email);
        Task<bool> UserExists(RegisterUserDto userDTO);
        Task<string> RegisterUserAsync(RegisterUserDto userDTO);
    }
}