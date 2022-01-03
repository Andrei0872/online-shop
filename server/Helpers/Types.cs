using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Helpers
{
    public class OrderSummary {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int NrProducts { get; set; }
        public int TotalPrice { get; set; }
        public DateTime IssuedAt { get; set; }

        public OrderSummary(int orderId, int userId, DateTime issuedAt, int nrProducts, int totalPrice)
        {
            OrderId = orderId;
            UserId = userId;
            NrProducts = nrProducts;
            TotalPrice = totalPrice;
            IssuedAt = issuedAt;
        }
    }
}