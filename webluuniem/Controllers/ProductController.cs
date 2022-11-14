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
        public ActionResult Detail(int id)
        {

            var x = _context.Products.SingleOrDefault(c => c.ProductID == id);
            return View(x);
        }

        
        public ActionResult  Index()
        {
            var ProductList = _context.Products.ToList();
            return View(ProductList);
        }

        [HttpGet]

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product model)
        {
            try
            {
                if(model == null)
                {
                    ViewBag.error = "Lỗi không lấy được dữ liệu";
                    return View();
                }
                else
                {
                    _context.Products.Add(model);
                    _context.SaveChanges();
                    return ViewBag.success = "Tạo thành công";
                }
            }
            catch
            {
                ViewBag.error = "Tạo không thành công";
                return View();
            }
            
        }
    }
}