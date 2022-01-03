using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using server.DAL.GenericRepository;
using server.Data;
using server.Models;

namespace server.Repositories.OrderRepository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ServerContext serverContext) : base(serverContext) { }

        public async Task<List<Order>> GetAllOrders()
        {
            return await this._serverContext.Orders.ToListAsync();
        }
    }
}