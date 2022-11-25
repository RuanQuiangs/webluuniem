using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using webluuniem.Models;

namespace webluuniem.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        // GET: Admin/Post
        Context _context = new Context();
       

        public ActionResult Index(string searchString, int? page)
        {
            
            if (page == null) page = 1;


            var post = _context.Posts.OrderBy(x => x.PostID).ToList();

            int pageSize = 10;

            int pageNumber = (page ?? 1);

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                post = post.Where(b => b.PostTitle.ToLower().Contains(searchString)).ToList();

            }

            return View(post.ToList().ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Details(int id)
        {
            var post = _context.Posts.SingleOrDefault(c => c.UserID == id);

            return View(post);
        }


        public ActionResult Create() { 
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post model, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                var post = _context.Posts.SingleOrDefault(c => c.PostTitle == model.PostTitle);
                if (post != null)
                {
                    ViewBag.error = "Tên bài viết  đã tồn tại";
                    return View();
                }
                model.CreateDate = DateTime.UtcNow;

                if (model.PostTitle.Length > 150)
                {
                    ViewBag.error = "Tiêu đề không quá 150 kí tự ( Khoảng 30 từ )";
                    return View();
                }


                model.UserID = (int)Session["UserID"];
                model.Slug = convertToUnSign2(model.PostTitle);
                _context.Configuration.ValidateOnSaveEnabled = false;
                _context.Posts.Add(model);
                _context.SaveChanges();

                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = model.PostID;

                    string _FileName = "";

                    int index = uploadhinh.FileName.IndexOf('.');

                    _FileName = "post" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Content/images/items"), _FileName);
                    uploadhinh.SaveAs(_path);
                    model.Image = _FileName;
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Tạo không thành công";
                return View();
            }
        }



        public ActionResult Edit(int id)
        {
            var cate = _context.Posts.FirstOrDefault(c => c.PostID == id);
            return View(cate);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post model, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                var post = _context.Posts.FirstOrDefault(c => c.PostTitle == model.PostTitle && c.PostID != model.PostID);
                if (post != null)
                {
                    ViewBag.error = "Tiêu đề đã tồn tại";
                    return View();
                }
                if (model.PostTitle.Length > 150)
                {
                    ViewBag.error = "Tiêu đề không quá 150 kí tự ( Khoảng 30 từ )";
                    return View();
                }

                var cat = _context.Posts.SingleOrDefault(c => c.PostID == model.PostID);
                cat.PostTitle = model.PostTitle;
                cat.Slug = convertToUnSign2(model.PostTitle);             
                cat.Deleted = model.Deleted;
                cat.PostText = model.PostText;
                _context.Configuration.ValidateOnSaveEnabled = false;
                _context.Posts.AddOrUpdate(cat);
                _context.SaveChanges();

                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = model.PostID;

                    string _FileName = "";

                    int index = uploadhinh.FileName.IndexOf('.');

                    _FileName = "post" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Content/images/items"), _FileName);
                    uploadhinh.SaveAs(_path);
                    cat.Image = _FileName;
                }

                if (cat.Image == null)
                {
                    ViewBag.error = "Đặt hình ảnh thất bại";
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "cập nhật không thành công";
                return View();
            }
        }

   

        public ActionResult Delete(int id)
        {
            var post = _context.Posts.Find(id);

            _context.Posts.Remove(post);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public string convertToUnSign2(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace(' ', '-');
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }

        [HttpPost]
        public JsonResult KeepSessionAlive()
        {
            return new JsonResult { Data = "Success" };
        }

    }
}