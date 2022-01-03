using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using server.Repositories.ProductRepository;
using server.Repositories.SessionRepository;
using server.Repositories.UserRepository;
using server.Repositories.OrderRepository;
using server.Repositories.OrderProductRepository;

namespace server.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        ISessionRepository SessionToken { get; }
        IUserRepository User { get; }
        IOrderRepository Order { get; }

        IOrderProductRepository OrderProduct { get; }

        Task SaveAsync();
    }
}