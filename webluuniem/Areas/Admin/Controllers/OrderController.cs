using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webluuniem.Models;

namespace webluuniem.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {

        Context _context = new Context();
        // GET: Admin/Order
        public ActionResult Index()
        {
            var order = _context.Orders.ToList();
            return View(order);
        }
    }
}