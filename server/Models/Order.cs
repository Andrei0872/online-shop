using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public class Order
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }

        public User User { get; set; }
    }
}