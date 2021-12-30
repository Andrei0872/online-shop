using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using server.Models;
using server.DAL.GenericRepository;

namespace server.Repositories.UserRepository
{
    public interface IUserRepository : IGenericRepository<User>
        {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByIdWithRoles(int id);
    }
}