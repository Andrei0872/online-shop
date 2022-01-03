using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using server.DAL.UnitOfWork;
using server.Helpers.Constants;
using server.Services.OrderService;
using server.Responses;
using server.DTOs;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = UserRoleType.Admin + "," + UserRoleType.User)]
        [HttpGet]
        public async Task<IActionResult> GetAll () {
            var orderResp = new OrderResponse();

            var orders = await this._orderService.GetAllOrdersSummary();
            orderResp.Data = orders;

            return Ok(orderResp);
        }

        [Authorize(Roles = UserRoleType.Admin + "," + UserRoleType.User)]
        [HttpPost]
        public async Task<IActionResult> CreateOrder ([FromBody] OrderDto orderDto) {
            var orderResp = new OrderResponse();

            var res = await this._orderService.CreateOrder(orderDto.Products);
            if (res) {
                orderResp.Data = new { message = "Order created successfully." };
                return Ok(orderResp);
            } 

            return Problem(title: "An error occurred while creating the order."); 
        }
    }
}