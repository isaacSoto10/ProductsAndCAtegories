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
    public class ProductController : Controller

    {
        private MyContext contexto;
        public ProductController(MyContext context)
        {
            contexto = context;
        }
        [HttpGet("AllProducts")]
        public IActionResult AllProducts()
            {
                ViewBag.allProducts = contexto.Products.ToList();
                return View();
            }

        [HttpGet("product/{productId}")]
        public IActionResult OneProduct(int productId)
            {
                Product RetrievedProduct = contexto.Products
                    .SingleOrDefault(product => product.ProductId == productId);
                ViewBag.OneProduct = RetrievedProduct;
                ViewBag.thisProdsCats = contexto.Categories
                    .Include(cat => cat.CategoriesProducts)
                    .Where(cat => cat.CategoriesProducts.Any(c => c.ProductId == productId))
                    .ToList(); 
                ViewBag.categoriesUnrelated = contexto.Categories
                    .Include(cat => cat.CategoriesProducts)
             
                    .Where(cat => cat.CategoriesProducts.All(cat => cat.ProductId != productId));


                return View();
            }
        
        [HttpPost("ProductAdd")]
        public IActionResult ProductAdd(Product newProduct)
            {
                contexto.Add(newProduct);
                contexto.SaveChanges();
                return RedirectToAction("AllProducts");
            }
        
        [HttpPost("product/{productId}")]
        public IActionResult AddCaategoryToProduct(Association newCatInProduct)
            {
                contexto.Associations.Add(newCatInProduct);
                contexto.SaveChanges();
                return RedirectToAction("OneProduct");
            }
    }
}