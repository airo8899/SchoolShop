using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolShop.Data;
using SchoolShop.Models;
using SchoolShop.ViewModels;


namespace SchoolShop.Controllers
{
    [Route("orders")]
    public class OrdersController : Controller
    {
        public ShopContext db;

        public OrdersController(ShopContext context)
        {
            db = context;
        }

        [Route("")]
        public IActionResult Index()
        {
            var orders = db.Orders
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .OrderByDescending(x => x.Number)
                .ToList();

            var ordersViewModel = orders
                .Select(order => new OrderViewModel
                {
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

        [HttpGet]
        [Route("{orderId:int}/payed")]
        public async Task<ActionResult<bool>> Payed(int orderId)
        {
            var order = await db.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
                return false;

            order.Status = OrderStatus.Payed;    //??????
            await db.SaveChangesAsync();

            return true;
        }

    }
}
