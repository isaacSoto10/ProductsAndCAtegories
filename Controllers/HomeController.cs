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
    public class HomeController : Controller
    {
        private MyContext contexto;
        public HomeController(MyContext context)
        {
            contexto = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        { 
            ViewBag.allCategories = contexto.Categories.ToList();

            return View();
        }

    }
}
