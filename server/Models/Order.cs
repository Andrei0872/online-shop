using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace server.Models
{
    public class Order
    {
    public int ID { get; set; }

    public int UserID { get; set; }

    public DateTime IssuedAt { get; set; }

    public ICollection<OrderProduct> OrderProducts { get; set; }

    public User User { get; set; }
    public Order()
    {
        IssuedAt = DateTime.UtcNow;
    }
    }
}