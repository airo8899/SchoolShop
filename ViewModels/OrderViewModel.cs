using SchoolShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolShop.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public List<OrderItemViewModel> Items { get; set; }
        public OrderStatus Status { get; internal set; }
        public decimal TotalSum => Items.Sum(x => x.TotalSum);
    }

    public class OrderItemViewModel
    {
        public decimal TotalSum => ProductPrice * Amount;

        public string ProductName { get; set; }
        public int Amount { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
