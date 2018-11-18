using Microsoft.AspNetCore.Mvc;
using SchoolShop.Models;
using System.Linq;

namespace SchoolShop.Controllers
{
    [Route("categories")]
    public class CategoriesController : Controller
    {
        private ShopContext db;

        public CategoriesController(ShopContext context)
        {
            db = context;
        }

        [Route("")]
        public IActionResult Index()
        {
            var categories = db.Categories.ToList();

            return View(categories);
        }

        [Route("add")]
        [Route("{categoryId:int}/edit")]
        public IActionResult Category(int? categoryId)
        {
            if (categoryId.HasValue)
            {
                var category = db.Categories.FirstOrDefault(x => x.Id == categoryId);
                return View(category);
            }

            return View();
        }

        [HttpPost]
        [Route("save")]
        public IActionResult Save(Category category)
        {
            if(category.Id == default(int))
            {
                db.Categories.Add(category);
            }
            else
            {
                db.Categories.Update(category);
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("{categoryId:int}/remove")]
        public IActionResult Remove(int CategoryId)
        {
            var category = db.Categories.FirstOrDefault(x => x.Id == CategoryId);

            if (category == null)
                return NotFound();

            db.Categories.Remove(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
