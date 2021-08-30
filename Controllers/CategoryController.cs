using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ProductsAndCategories.Models;

namespace ProductsAndCategories.Controllers
{
    public class CategoryController : Controller
    {
        private MyContext contexto;
        public CategoryController(MyContext context)
        {
            contexto = context;
        }
        [HttpGet("category/{categoryId}")]
        public IActionResult OneCategory(int categoryId )
            {
                Category RetrievedCategory = contexto.Categories
                    .SingleOrDefault(cat => cat.CategoryId == categoryId);
                ViewBag.OneCategory = RetrievedCategory;
                ViewBag.thisCatsProducts = contexto.Products
                    .Include(p => p.ProductsCategories)
                    .Where(p => p.ProductsCategories.Any(p => p.CategoryId == categoryId))
                    .ToList();
                ViewBag.productsUnrelated = contexto.Products
                    .Include(p => p.ProductsCategories)
                    .Where(p => p.ProductsCategories.All(p => p.CategoryId != categoryId));
                return View();
            }
        [HttpPost("CategoryAdd")]
        public IActionResult CategoryAdd(Category newCategory)
            {
                contexto.Add(newCategory);
                contexto.SaveChanges();
                return RedirectToAction("Index");
            }
        [HttpPost("category/{categoryId}")]
        public IActionResult AddProductToCategory(Association newProductInCat)
            {
                contexto.Associations.Add(newProductInCat);
                contexto.SaveChanges();
                return RedirectToAction("OneCategory");
            }
    }
}
