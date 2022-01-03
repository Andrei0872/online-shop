using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using server.DAL.GenericRepository;
using server.Data;
using server.Models;

namespace server.Repositories.OrderProductRepository
{
    public class OrderProductRepository : GenericRepository<OrderProduct>, IOrderProductRepository
    {
        public OrderProductRepository(ServerContext serverContext) : base(serverContext)
        {
        }
    }
}