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
    public class CategoryController : Controller
    {
        Context _context = new Context();
        // GET: Admin/Category
        public ActionResult Index()
        {
            var category = _context.Categorys.ToList();
            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category model, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                if (_context.Categorys.FirstOrDefault(c => c.CategoryName == model.CategoryName) != null)
                {
                    ViewBag.error = "Danh mục đã tồn tại";
                    return View();
                }
                model.UpdateOnUtc = DateTime.UtcNow;
                model.CreateOnUtc = DateTime.UtcNow;
                model.Deleted = false;
                model.Slug = convertToUnSign2(model.CategoryName);
                _context.Configuration.ValidateOnSaveEnabled = false;
                _context.Categorys.Add(model);
                _context.SaveChanges();

                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = model.CategoryID;

                    string _FileName = "";

                    int index = uploadhinh.FileName.IndexOf('.');

                    _FileName = "category" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Content/images/items"), _FileName);
                    uploadhinh.SaveAs(_path);
                    model.Avatar = _FileName;
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
            var cate = _context.Categorys.FirstOrDefault(c => c.CategoryID == id);
            return View(cate);
        }
        
    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                if (_context.Categorys.FirstOrDefault(c => c.CategoryName == model.CategoryName && c.CategoryID != model.CategoryID) != null)
                {
                    ViewBag.error = "Danh mục đã tồn tại";
                    return View();
                }

                var cat = _context.Categorys.SingleOrDefault(c => c.CategoryID == model.CategoryID);
                cat.CategoryName = model.CategoryName;
                cat.Slug = convertToUnSign2(model.CategoryName);
                cat.UpdateOnUtc = DateTime.UtcNow;
                cat.DisplayOrder = model.DisplayOrder;
                cat.ShowOnHomePage = model.ShowOnHomePage;
                cat.Deleted = model.Deleted;
                _context.Configuration.ValidateOnSaveEnabled = false;
                _context.Categorys.AddOrUpdate(cat);
                _context.SaveChanges();

                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = cat.CategoryID;

                    string _FileName = "";

                    int index = uploadhinh.FileName.IndexOf('.');

                    _FileName = "category" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Content/images/items"), _FileName);
                    uploadhinh.SaveAs(_path);
                    cat.Avatar = _FileName;
                }
                if (cat.Avatar == null)
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

        public ActionResult Details(int id)
        {
            var cate = _context.Categorys.FirstOrDefault(c => c.CategoryID == id);
            return View(cate);
        }


        public ActionResult Delete(int id)
        {
            var brand = _context.Categorys.Find(id);

            _context.Categorys.Remove(brand);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        //slug hóa
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
    }
}