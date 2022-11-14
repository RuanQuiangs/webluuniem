using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webluuniem.Models;

namespace webluuniem.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        Context _context = new Context();
        public ActionResult Index()
        {
            var x = _context.tests.ToList();
            return View(x);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(test model)
        {
            if(model != null)
            {
                var x = _context.tests.FirstOrDefault(c => c.name == model.name);
                if(x != null)
                {
                    ViewData["error"] = "Trùng!";
                    return View();
                }
                _context.tests.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}