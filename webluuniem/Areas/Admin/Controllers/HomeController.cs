﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webluuniem.Models;

namespace webluuniem.Areas.Admin.Controllers
{
    
   
    public class HomeController : Controller
    {
        // GET: Admin/Home
        Context _context = new Context();
        public ActionResult Index()
        {
            if (Session["Admin"] == "Admin")
            {
                var lstProduct = _context.Products.ToList();
                return View(lstProduct);
            }
            return View();
        }

        [HttpPost]
        public JsonResult KeepSessionAlive()
        {
            return new JsonResult { Data = "Success" };
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}