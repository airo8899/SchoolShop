using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolShop.Models;
using SchoolShop.Data;
using SchoolShop.ViewModels;

namespace SchoolShop.Controllers
{
    [Route("basket")]

    public class BasketController : Controller
    {
        public ShopContext db;

        public BasketController(ShopContext context)
        {
            db = context;
        }

        [Route("")]
        public IActionResult Index()
        {
            var orders = db.Orders
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .Where(x => x.Status == OrderStatus.NotPayed)
                .ToList();

            var ordersViewModel = orders
                .Select(order => new OrderViewModel
                {
                    Id = order.Id,
                    Number = order.Number,
                    Status = order.Status,
                    Items = order.Items
                        .Select(item => new OrderItemViewModel
                        {
                            ProductName = item.Product.Name,
                            ProductPrice = item.Product.Price,
                            Amount = item.Count
                        }).ToList()
                });

            return View(ordersViewModel);
        }

        [Route("GRID")]
        public IActionResult GRID()
        {
            var orders = db.Orders
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .Where(x => x.Status == OrderStatus.NotPayed)
                .ToList();

            var ordersViewModel = orders
                .Select(order => new OrderViewModel
                {
                    Id = order.Id,

                    Number = order.Number,
                    Status = order.Status,
                    Items = order.Items
                        .Select(item => new OrderItemViewModel
                        {
                            ProductName = item.Product.Name,
                            ProductPrice = item.Product.Price,
                            Amount = item.Count
                        }).ToList()
                });

            return View(ordersViewModel);
        }
    }
}
