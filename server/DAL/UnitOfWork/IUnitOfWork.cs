using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using server.Repositories.ProductRepository;
using server.Repositories.SessionRepository;
using server.Repositories.UserRepository;

namespace server.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        ISessionRepository SessionToken { get; }
        IUserRepository User { get; }

        Task SaveAsync();
    }
}