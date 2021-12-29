using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public enum Category { Category1, Category2, Category3, Category4 }

    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public Category Category { get; set; }
        public DateTime AddedAt { get; set; }

        public ICollection<OrderProduct> Orders { get; set; }
    }
}