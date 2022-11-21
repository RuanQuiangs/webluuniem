using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using webluuniem.Models;

namespace webluuniem.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        Context _context = new Context();
        // GET: Admin/Brand
      
        public ActionResult Index(string searchString, int? page)
        {

            if (page == null) page = 1;


            var brand = _context.Brands.OrderBy(x => x.BrandID).ToList();

            int pageSize = 10;

            int pageNumber = (page ?? 1);

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                brand = brand.Where(b => b.BrandName.ToLower().Contains(searchString)).ToList();

            }

            return View(brand.ToList().ToPagedList(pageNumber, pageSize));
        }

        //create
        public ActionResult Create()
        {
          
            return View();
        }
        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brand model, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                model.Deleted = false;
                model.UpdateOnUtc = DateTime.UtcNow;
                model.CreateOnUtc = DateTime.UtcNow;
                model.Slug = convertToUnSign2(model.BrandName);
                _context.Configuration.ValidateOnSaveEnabled = false;
                _context.Brands.Add(model);
                _context.SaveChanges();

                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = model.BrandID;

                    string _FileName = "";

                    int index = uploadhinh.FileName.IndexOf('.');

                    _FileName = "Brand" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Content/images/items"), _FileName);
                    uploadhinh.SaveAs(_path);
                    model.Avatar = _FileName;
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Tạo nhãn hiệu không thành công";
                return View();
            }
        } 


        //edit
        public ActionResult Edit(int id)
        {
          
            var brand = _context.Brands.SingleOrDefault(c => c.BrandID == id);
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brand model, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {

                var brand = _context.Brands.SingleOrDefault(c => c.BrandID == model.BrandID);
                if (_context.Brands.FirstOrDefault(c => c.BrandName == model.BrandName && c.BrandID != model.BrandID) != null)
                {
                    ViewBag.error = "Nhãn hiệu đã tồn tại";
                    return View();
                }

                  brand.BrandName = model.BrandName;
                brand.UpdateOnUtc = DateTime.UtcNow;
                brand.Country = model.Country;
                brand.DisplayOrder = model.DisplayOrder;
                brand.ShowOnHomePage = model.ShowOnHomePage;
                brand.Slug = convertToUnSign2(model.BrandName);
                brand.Deleted = model.Deleted;

                _context.Configuration.ValidateOnSaveEnabled = false;
                _context.Brands.AddOrUpdate(brand);
                _context.SaveChanges();

                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = brand.BrandID;

                    string _FileName = "";

                    int index = uploadhinh.FileName.IndexOf('.');

                    _FileName = "Brand" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Content/images/items"), _FileName);
                    uploadhinh.SaveAs(_path);
                    brand.Avatar = _FileName;
                }
               
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = " cập nhật không thành công";
                return View();
            }
        }


        public ActionResult Details(int id)
        {

            var brand = _context.Brands.SingleOrDefault(c => c.BrandID == id);
            return View(brand);
        }



        //xóa vĩnh viễn
        // GET: Book/Delete/5
        // GET: /Movies/Delete/5
        public ActionResult Delete(int id)
        {
            var brand = _context.Brands.Find(id);
            
            _context.Brands.Remove(brand);
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