using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolShop.Models;
using System.Collections.Generic;

namespace SchoolShop.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
