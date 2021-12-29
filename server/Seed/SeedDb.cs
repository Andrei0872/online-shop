using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using server.Models;
using server.Data;
using server.Helpers.Constants;

namespace server.Seed
{
    public class SeedDb
    {
        private RoleManager<Role> _roleManager;
        private UserManager<User> _userManager;
        private ServerContext _serverContext;
        public SeedDb(RoleManager<Role> roleManager, UserManager<User> userManager, ServerContext serverContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _serverContext = serverContext;
        }

        public async Task SeedRoles () {
            if (_serverContext.Roles.Any()) {
                return;
            }

            string[] roleNames = {
                UserRoleType.Admin,
                UserRoleType.User
            };

            foreach (var roleName in roleNames) {
                var roleExists = await _roleManager.RoleExistsAsync(roleName);
                if (roleExists) {
                    continue;
                }

                await _roleManager.CreateAsync(new Role { Name = roleName });
                await _serverContext.SaveChangesAsync();
            }
        }

        public async Task SeedUsers () {
            var usersExist = this._serverContext.Users.Count() > 0;
            if (usersExist) {
                return;
            }
            
            User[] rawUsers = new User[] {
                new User { UserName = "foo", Email = "foo@foo.com", LastName = "fooLast", FirstName = "fooFirst" },
                new User { UserName = "bar", Email = "bar@bar.com", LastName = "barLast", FirstName = "barFirst" }
            };

            foreach (var rawU in rawUsers) {
                await this._userManager.CreateAsync(rawU, "pass" + rawU.Id);
                await this._userManager.AddToRoleAsync(rawU, rawU.Id == 1 ? UserRoleType.Admin : UserRoleType.User);
            }
        }
    }
}