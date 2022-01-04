using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using server.Models;

namespace server.DTOs
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public Category Category { get; set; }
    }
}