using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using server.DAL.UnitOfWork;
using server.Models;
using server.Responses;
using server.Services.UserService;
using Microsoft.EntityFrameworkCore;
using server.Helpers;

namespace server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork _unitOfWork;
        private IUserService _userService;

        public OrderService(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<bool> CreateOrder(DTOs.OrderProduct[] orderProducts)
        {
            var o = new Order();
            o.UserID = Int32.Parse(this._userService.GetCrtUserId());
            o.OrderProducts = new List<OrderProduct>();

            this._unitOfWork.Order.Insert(o);
            
            foreach (var p in orderProducts) {
                var op = new OrderProduct();
                op.OrderID = o.ID;
                op.ProductID = p.Id;
                op.Quantity = p.Quantity;

                o.OrderProducts.Add(op);
            }

            try {
                await this._unitOfWork.SaveAsync();
                return true;
            } catch (Exception err) {
                Console.WriteLine("[OrderService#CreateOrder]", err);
                return false;
            }
        }

    public async Task<List<OrderSummary>> GetAllOrdersSummary () {
        var crtUserId = Int32.Parse(this._userService.GetCrtUserId());
        
        return await this._unitOfWork.Order
            .GetAll()
            .Where(o => o.UserID == crtUserId)
            .Join(
            this._unitOfWork.OrderProduct.GetAll(),
                o => o.ID,
                op => op.OrderID,
                (o, op) => new {
                    OrderId = o.ID,
                    ProductId = op.ProductID,
                    Quantity = op.Quantity,
                    IssuedAt = o.IssuedAt,
                    UserId = o.UserID
                }
            )
            .Join(
                this._unitOfWork.Product.GetAll(),
                o => o.ProductId,
                p => p.ID,
                (orderProductsJoined, p) => new { OrderProductsJoined = orderProductsJoined, Price = p.Price }
            )
            .GroupBy(r => new { OrderId = r.OrderProductsJoined.OrderId, IssuedAt = r.OrderProductsJoined.IssuedAt, UserId = r.OrderProductsJoined.UserId })
            .Select(r => new OrderSummary(r.Key.OrderId, r.Key.UserId, r.Key.IssuedAt, r.Count(), r.Sum(o => o.Price * o.OrderProductsJoined.Quantity)))
            .ToListAsync();
        }
    }
}