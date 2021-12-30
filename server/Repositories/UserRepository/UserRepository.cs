using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using server.DAL.GenericRepository;
using server.Data;
using server.Models;
using Microsoft.EntityFrameworkCore;

namespace server.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
    public UserRepository(ServerContext serverContext) : base(serverContext) {}

    public async Task<List<User>> GetAllUsers()
    {
        return await this._serverContext.Users.ToListAsync();
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await this._serverContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
    }

    public async Task<User> GetUserByIdWithRoles(int id)
    {
        return await this._serverContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id.Equals(id));
    }
    }
}