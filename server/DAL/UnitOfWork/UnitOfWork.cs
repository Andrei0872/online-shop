using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Repositories.ProductRepository;

using server.Data;
using server.Repositories.SessionRepository;
using server.Repositories.UserRepository;
using server.Repositories.OrderRepository;
using server.Repositories.OrderProductRepository;

namespace server.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
    
    private ServerContext _serverContext;
    private IProductRepository _productRepository;
    private ISessionRepository _sessionRepository;
    private IUserRepository _userRepository;
    private IOrderRepository _orderRepository;
    private IOrderProductRepository _orderProductRepository;
    public UnitOfWork(ServerContext serverContext)
    {
        _serverContext = serverContext;
    }

    public IProductRepository Product {
        get {
            if (this._productRepository == null) {
                this._productRepository = new ProductRepository(this._serverContext);
            }

            return this._productRepository;
        }
    }

    public ISessionRepository SessionToken {
        get {
            if (this._sessionRepository == null) {
                this._sessionRepository = new SessionRepository(this._serverContext);
            }

            return this._sessionRepository;
        }
    }

    public IUserRepository User {
        get {
            if (this._userRepository == null) {
                this._userRepository = new UserRepository(this._serverContext);
            }

            return this._userRepository;
        }
    }

    public IOrderRepository Order {
        get {
            if (this._orderRepository == null) {
                this._orderRepository = new OrderRepository(this._serverContext);
            }

            return this._orderRepository;
        }
    }
    public IOrderProductRepository OrderProduct {
        get {
            if (this._orderProductRepository == null) {
                this._orderProductRepository = new OrderProductRepository(this._serverContext);
            }

            return this._orderProductRepository;
        }
    }

    public async Task SaveAsync()
    {
        await this._serverContext.SaveChangesAsync();
    }
    }
}