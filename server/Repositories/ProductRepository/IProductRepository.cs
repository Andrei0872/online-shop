using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using server.DAL.GenericRepository;
using server.Models;

namespace server.Repositories.ProductRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        List<Product> GetAllProducts();
    }
}