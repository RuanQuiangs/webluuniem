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
        // GET: Category
       
        Context _context = new Context();
        // GET: Category
        public ActionResult Index()
        {
            var category = _context.Categorys.ToList();
            return View(category);
        }

        public ActionResult ProductCategory(string slug)
        {
            var productCategory = _context.Categorys.FirstOrDefault(x => x.Slug == slug);
            var productList = _context.Products.Where(c => c.CategoryID == productCategory.CategoryID);

            var cate = _context.Categorys.ToList();
            var barn = _context.Brands.ToList();
            SelectList cateList = new SelectList(cate, "Slug", "CategoryName");
            SelectList brandlist = new SelectList(barn, "Slug", "BrandName");
            // Set vào ViewBag
            ViewBag.CategoryList = cateList;
            ViewBag.BrandList = brandlist;
            return View(productList);
        }
    }
}