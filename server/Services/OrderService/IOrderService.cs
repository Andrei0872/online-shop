using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using server.Models;
using server.DTOs;
using server.Helpers;

namespace server.Services.OrderService
{
    public interface IOrderService
    {
        Task<List<OrderSummary>> GetAllOrdersSummary(int specificUserId);
        Task<bool> CreateOrder(server.DTOs.OrderProduct[] orderProducts);
        Task<OrderInDetail> GetOrder(int orderId);
    }
}