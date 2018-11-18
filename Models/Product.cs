using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
