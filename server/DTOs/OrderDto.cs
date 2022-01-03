using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.DTOs
{
    public class OrderProduct {
        public int Id { get; set; }
        public int Quantity { get; set; }
    };
    public class OrderDto
    {
        public OrderProduct[] Products { get; set; }
    }
}