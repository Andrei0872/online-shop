using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Repositories.ProductRepository;

using server.Data;
using server.Repositories.SessionRepository;
using server.Repositories.UserRepository;

namespace server.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
    
    private ServerContext _serverContext;
    private IProductRepository _productRepository;
    private ISessionRepository _sessionRepository;
    private IUserRepository _userRepository;
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

    public async Task SaveAsync()
    {
        await this._serverContext.SaveChangesAsync();
    }
    }
}