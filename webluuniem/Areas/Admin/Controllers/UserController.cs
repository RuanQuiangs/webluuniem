using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using webluuniem.Models;

namespace webluuniem.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        Context _context = new Context();
        // GET: Admin/User
        
        public ActionResult Index(string searchString, int? page)
        {

            if (page == null) page = 1;


            var user = _context.Users.OrderBy(x => x.UserId).ToList();

            int pageSize = 10;

            int pageNumber = (page ?? 1);

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                user = user.Where(b => b.Username.ToLower().Contains(searchString)).ToList();

            }

            return View(user.ToList().ToPagedList(pageNumber, pageSize));
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User model, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                
                var _User = _context.Users.SingleOrDefault(c => c.UserId == model.UserId);
                if (_User != null)
                {
                    var check = _context.Users.FirstOrDefault(c => c.Email == model.Email);
                    if (check != null && check.UserId != model.UserId)
                    {
                        ViewBag.error = "Email đã có người sử dụng";
                        return View();
                    }

                    if (IsEmail(model.Email) == false)
                    {
                        ViewBag.error = "Email không đúng định dạng vd: user@gmail.com";
                        return View();
                    }

                    if(model.Password != _User.Password)
                    {
                        var pass2 = new LoginModel
                        {
                            Username = model.Username,
                            Password = model.Password,

                        };

                        _User.Password = PasswordEncryption(pass2);
                       
                    }

                    _User.FirstName = model.FirstName;
                    _User.LastName = model.LastName;
                    _User.Address = model.Address;
                    _User.Introduce = model.Introduce;
                    _User.IsActive = model.IsActive;
                    _User.IsAdmin = model.IsAdmin;


                    if (model.Email != null)
                    {
                        _User.Email = model.Email;
                    }
                   
                    _User.Introduce = model.Introduce;
                    _User.Phone = model.Phone;
                    _User.Slug = convertToUnSign2(model.FirstName + model.LastName);
                    _context.Users.AddOrUpdate(_User);
                    _context.SaveChanges();
                    if (uploadhinh != null && uploadhinh.ContentLength > 0)
                    {
                        int id = _User.UserId;

                        string _FileName = "";

                        int index = uploadhinh.FileName.IndexOf('.');

                        _FileName = "avatar" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                        string _path = Path.Combine(Server.MapPath("~/Content/images/avatars"), _FileName);
                        uploadhinh.SaveAs(_path);
                        _User.Avatar = _FileName;

                    }

                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }


            }
            ViewBag.error = "Thay đổi thất bại";
            return View();
        }
    

    public static bool IsEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        string strRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        Regex regex = new Regex(strRegex);

        return regex.IsMatch(email);
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
    private string PasswordEncryption(LoginModel user)
    {
        MD5 mh = MD5.Create();
        //Chuyển kiểu chuổi thành kiểu byte
        byte[] _user = System.Text.Encoding.ASCII.GetBytes(user.Password + user.Username);
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(user.Password + Encoding.UTF8.GetBytes("daylaconghoaxahoichunghiavietnam"));

        //mã hóa chuỗi đã chuyển
        byte[] hash = mh.ComputeHash(inputBytes);
        byte[] hash2 = mh.ComputeHash(_user);
        //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
            sb.Append(hash2[i].ToString("X2"));
        }
        return (sb.ToString());
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