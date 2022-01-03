using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using server.Models;

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

    public class OrderProduct : Product {

    public int Quantity { get; set; }
    public OrderProduct(Product p, int quantity)
    {
        Quantity = quantity;
        ID = p.ID;
        Name = p.Name;
        Price = p.Price;
        Category = p.Category;
        AddedAt = p.AddedAt;
    }
    }

    public class OrderInDetail {
        public int Id { get; set; }
        public int TotalPrice { get; set; }
        public List<OrderProduct> Products { get; set; }

        public OrderInDetail()
        {
            this.Products = new List<OrderProduct>();
        }
    }
}