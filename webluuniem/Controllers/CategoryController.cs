using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webluuniem.Models;

namespace webluuniem.Controllers
{
    public class CategoryController : Controller
    {
        webluuniem.Models.Context _context = new webluuniem.Models.Context();
        // GET: Category
        public ActionResult Index()
        {
            var product = _context.Products.ToList();
            return View(product);
        }

        public ActionResult ProductCategory(int Id)
        {
            var productCategory = _context.Products.Where(x => x.CategoryID == Id).ToList();
            return View(productCategory);
        }
    }
}