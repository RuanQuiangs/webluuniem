using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using webluuniem.Models;

namespace webluuniem.Controllers
{
    public class HomeController : Controller
    {
        Context _context = new Context();

        public ActionResult Index()
        {
            
            HomeModel ObjHomeModel = new HomeModel();
            ObjHomeModel.CategoryList = _context.Categorys.ToList();
            ObjHomeModel.ProductList = _context.Products.ToList();
            return View(ObjHomeModel);
        }


               
        

        [HttpGet]
        public ActionResult ViewUser()
        {
            var userList  = _context.Users.ToList();
            return View(userList);
        }

        
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserModel model, HttpPostedFileBase uploadhinh)
        {

            if (ModelState.IsValid)
            {
                var _User = _context.Users.SingleOrDefault(c => c.Username == model.Username);
                if (_User == null)
                {
                    var check = _context.Users.FirstOrDefault(c => c.Email == model.Email);
                    if(check != null)
                    {
                        ViewBag.error = "Email đã có người sử dụng";
                        return View();
                    }

                    if (IsEmail(model.Email) == false)
                    {
                        ViewBag.error = "Email không đúng định dạng vd: user@gmail.com";
                        return View();
                    }

                    var pass = new LoginModel
                    {
                        Username = model.Username,
                        Password = model.Password
                    };

                  
                    var _user = new User
                    {
                        Username = model.Username,
                        Password = PasswordEncryption(pass),
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Phone = model.Phone,
                        Introduce = model.Introduce,
                        CreationTime = DateTime.UtcNow,
                        Slug = convertToUnSign2(model.FirstName + model.LastName),
                        IsAdmin = false,
                        IsActive = true,
                        Address = model.Address,

                    };

                    _context.Configuration.ValidateOnSaveEnabled = false;
                    _context.Users.Add(_user);
                    _context.SaveChanges();
                    if (uploadhinh != null && uploadhinh.ContentLength > 0)
                    {
                        int id = _user.UserId;

                        string _FileName = "";

                        int index = uploadhinh.FileName.IndexOf('.');

                        _FileName = "avatar" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                        string _path = Path.Combine(Server.MapPath("~/Content/images/avatars"), _FileName);
                        uploadhinh.SaveAs(_path);
                        _user.Avatar = _FileName;
                    }
                    
                    _context.SaveChanges();
                    return RedirectToAction("ViewUser");
                }

              

                else
                {
                    ViewBag.error = "Username đã có người sử dụng";
                    return View();
                }
            }

            ViewBag.error = "Đăng kí thất bại";
            return View();

        }
        /*
        [HttpPost]
        public ActionResult Edit(NhanVien nv, HttpPostedFileBase uploadhinh)
        {
            NhanVien unv = db.NhanVien.FirstOrDefault(x => x.nhanvien_id == nv.nhanvien_id);
            unv.TenNhanVien = nv.TenNhanVien;
            unv.DiaChi = nv.DiaChi;
            unv.SoDienThoai = nv.SoDienThoai;

            if (uploadhinh != null && uploadhinh.ContentLength > 0)
            {
                int id = nv.nhanvien_id;

                string _FileName = "";
                int index = uploadhinh.FileName.IndexOf('.');
                _FileName = "nv" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                string _path = Path.Combine(Server.MapPath("~/Upload/nhanvien"), _FileName);
                uploadhinh.SaveAs(_path);
                unv.hinh = _FileName;
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }
        */


        public ActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]     
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                var pass = PasswordEncryption(model);
                var data = _context.Users.SingleOrDefault(s => s.Username.Equals(model.Username) && s.Password.Equals(pass));
                if (data != null)
                {
                    //add session
                    Session["FullName"] = data.FirstName + " " + data.LastName;
                    Session["Email"] = data.Email;
                    Session["UserID"] = data.UserId;
                    Session["IsAdmin"] = data.IsAdmin;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Tài khoản hoặc mật khẩu không chính xác";
                    return RedirectToAction("Login");
                }
            }
            else
            {
                ViewBag.error = "Đăng nhập thất bại, thử lại sau";
                return RedirectToAction("Login");
            }
        }

        //kiểm tra email

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

        //cài đăt time
        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1979, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();

            return dateTimeInterval;
        }

        //mã hóa pass
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


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
    }

    
}