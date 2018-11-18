using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolShop.Models;
using SchoolShop.Data;
using SchoolShop.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolShop.Controllers
{
    [Route("products")]

    public class ProductsController : Controller
    {
        private ShopContext db;

        public ProductsController(ShopContext context)
        {
            db = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            var products = db.Products.Include(x => x.Category).ToList();

            return View(products);
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            var viewModel = new ProductViewModel
            {
                Categories = db.Categories.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [Route("save")]
        public IActionResult Save(Product product)
        {
            if (product.Id == default(int))        //???
            {
                db.Products.Add(product);
            }
            else
            {
                db.Products.Attach(product);
                db.Entry(product).State = EntityState.Modified;
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var product = db.Products.Find(id);

            db.Products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var viewModel = new ProductViewModel
            {
                Categories = db.Categories.Select(x => new SelectListItem {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };
            viewModel.Product = db.Products.Find(id);
            return View(viewModel);
        }

        [HttpGet]
        [Route("{productId:int}/addToCart")]
        public IActionResult AddToCart(int productId)
        {
            var order = db.Orders
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .FirstOrDefault(x => x.Status == OrderStatus.NotPayed);

            if (order == null)
            {
                order = new Order();
                db.Orders.Add(order);
                db.SaveChanges();
            }

            var orderItem = order.Items.FirstOrDefault(x => x.ProductId == productId);

            if (orderItem == null)
            {
                orderItem = new OrderItem
                {
                    ProductId = productId,
                    OrderId = order.Id,
                    Count = 1
                };

                db.OrderItems.Add(orderItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            orderItem.Count++;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
