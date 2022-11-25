using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webluuniem.Models;

namespace webluuniem.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        Context _context = new Context();
        public ActionResult Index()
        {
            var x = _context.Posts.OrderByDescending(c=>c.CreateDate).ToList();
            return View(x);
        }

        public ActionResult Details(string slug)
        {
            PostModel post = new PostModel();
            post.UserList = _context.Users.ToList();
            post.Post = _context.Posts.SingleOrDefault(c => c.Slug == slug);
            post.PostList = _context.Posts.OrderByDescending(c => c.CreateDate).ToList();
            post.CommentList = _context.Comments.OrderByDescending(c => c.DateComment).ToList();
            return View(post);
        }

        public ActionResult PostNews()
        {
            var x = _context.Posts.OrderByDescending(c => c.CreateDate).ToList();
            return View(x);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(Comment cm)
        {
            var post1 = _context.Posts.SingleOrDefault(c => c.PostID == cm.PostID);
            PostModel post = new PostModel();
            post.Post = post1;
            post.UserList = _context.Users.ToList();
            post.PostList = _context.Posts.OrderByDescending(c => c.CreateDate).ToList();
            post.CommentList = _context.Comments.OrderByDescending(c => c.DateComment).ToList();
            if (cm.Text == null)
            {
                ViewBag.error = "Bạn chưa nhập bình luận ";
                return View(post);
            }
            if (Session["UserID"] == null)
            {
                ViewBag.error = "Bạn chưa đăng nhập ";
                return View(post);
            }

            cm.UserID = (int)Session["UserID"];
            cm.DateComment = DateTime.UtcNow;
            _context.Configuration.ValidateOnSaveEnabled = false;
            _context.Comments.Add(cm);
            cm = null;
            _context.SaveChanges();
            ViewBag.error = "Bình luận thành công ";
            return View(post);
        }
    }
}