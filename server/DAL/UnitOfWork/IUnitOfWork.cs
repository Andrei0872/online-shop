using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using server.Repositories.ProductRepository;

namespace server.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }

        Task SaveAsync();
    }
}