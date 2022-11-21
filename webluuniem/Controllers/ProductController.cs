using System;
using System.Linq;
using System.Web.Mvc;
using webluuniem.Models;

namespace webluuniem.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        Context _context = new Context();
        public ActionResult Detail(string slug)
        {
           
            var x = _context.Products.FirstOrDefault(c=>c.Slug == slug);       
            return View(x);
        }

        
        public ActionResult  Index()
        {
            var ProductList = _context.Products.ToList();
            return View(ProductList);
        }

        [HttpGet]

   
        public ActionResult Offers()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }
        
    }
}