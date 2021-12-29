using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using server.DAL.GenericRepository;
using server.Data;
using server.Models;

namespace server.Repositories.ProductRepository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
    public ProductRepository(ServerContext serverContext) : base(serverContext)
    {
    }

    public List<Product> GetAllProducts()
    {
      return GetAll().ToList();
    }
  }
}