using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using server.DAL.GenericRepository;
using server.Models;

namespace server.Repositories.OrderRepository
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetAllOrders();
    }
}