using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webluuniem.Models;

namespace webluuniem.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        Context _context = new Context();
        // GET: Admin/User
        public ActionResult Index()
        {
            var user = _context.Users.ToList();
            return View(user);
        }

  
        public ActionResult Details(int id)
        {
            var user = _context.Users.SingleOrDefault(c => c.UserId == id);
            return View(user);
        }


        public ActionResult Edit(int id)
        {
            var user = _context.Users.SingleOrDefault(c => c.UserId == id);
            return View(user);
        }

        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Delete(int id)
        {
           
            var user = _context.Users.SingleOrDefault(c => c.UserId == id);
            if(user.Username == "admin" || user.IsAdmin == true)
            {
                ViewBag.error = "Bạn không thể xóa hoặc sửa tài khoản này";
                return View();
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}