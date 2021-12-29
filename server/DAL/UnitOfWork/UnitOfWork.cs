using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Repositories.ProductRepository;

using server.Data;

namespace server.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
    
    private ServerContext _serverContext;
    private IProductRepository _productRepository;
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

    public async Task SaveAsync()
    {
        await this._serverContext.SaveChangesAsync();
    }
    }
}