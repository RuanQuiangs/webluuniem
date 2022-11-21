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
    public class ProductController : Controller
    {
        Context _context = new Context();
        // GET: Admin/Product
        public ActionResult Index()
        {
            var product = _context.Products.ToList();
            
            return View(product);
        }

        public ActionResult Create()
        {
            var cate = _context.Categorys.ToList();
            var barn = _context.Brands.ToList();
            SelectList cateList = new SelectList(cate, "CategoryID", "CategoryName");
            SelectList brandlist = new SelectList(barn, "BrandID", "BrandName");
            // Set vào ViewBag
            ViewBag.CategoryList = cateList;
            ViewBag.BrandList = brandlist;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product model, HttpPostedFileBase uploadhinh)
        {

            if (ModelState.IsValid)
            {
               
                model.Deleted = false;
                model.Amount = 0;

                var x = _context.Products.FirstOrDefault(c => c.ProductName == model.ProductName);
                if(x != null)
                {
                     ViewBag.error = "Tên sản phẩm đã tồn tại";
                    return View();

                }

                _context.Configuration.ValidateOnSaveEnabled = false;
                _context.Products.Add(model);
                _context.SaveChanges();
                
                
                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = model.ProductID;

                    string _FileName = "";

                    int index = uploadhinh.FileName.IndexOf('.');

                    _FileName = "Product" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Content/images/items"), _FileName);
                    uploadhinh.SaveAs(_path);
                    model.Image = _FileName;
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Tạo sản phẩm không thành công";
                return View();
            }
        }


        public ActionResult Edit(int id)
        {
           
            var product = _context.Products.SingleOrDefault(c => c.ProductID == id);
            var cate = _context.Categorys.ToList();
            var barn = _context.Brands.ToList();
            SelectList cateList = new SelectList(cate, "CategoryID", "CategoryName");
            SelectList brandlist = new SelectList(barn, "BrandID", "BrandName");
            // Set vào ViewBag
            ViewBag.CategoryList = cateList;
            ViewBag.BrandList = brandlist;
            return View(product);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model, HttpPostedFileBase uploadhinh)
        {

            if (ModelState.IsValid)
            {
                var product = _context.Products.SingleOrDefault(c => c.ProductID == model.ProductID);

                var check = _context.Products.FirstOrDefault(c => c.ProductName == model.ProductName && c.ProductID != model.ProductID);
                if(check != null)
                {
                    ViewBag.error = "Tên sản phẩm đã tồn tại";
                    return View();
                }
                product.ProductName = model.ProductName;
                product.CategoryID = model.CategoryID;
                product.BrandID = model.BrandID;
                product.DescribeFull = model.DescribeFull;
                product.DescribeShort = model.DescribeShort;
                product.Discount = model.Discount;
                product.Price = model.Price;
              
                _context.Configuration.ValidateOnSaveEnabled = false;
                _context.Products.AddOrUpdate(product);
                _context.SaveChanges();

                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = model.ProductID;

                    string _FileName = "";

                    int index = uploadhinh.FileName.IndexOf('.');

                    _FileName = "Product" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Content/images/items"), _FileName);
                    uploadhinh.SaveAs(_path);
                    product.Image = _FileName;
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "cập nhật sản phẩm không thành công";
                return View();
            }
        }

        public ActionResult Details(int id)
        {

            var product = _context.Products.SingleOrDefault(c => c.ProductID == id);
            return View(product);
           
        }


        public ActionResult Delete(int id)
        {

            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
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
    }
}