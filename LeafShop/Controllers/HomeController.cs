﻿using LeafShop.Models;
using LeafShop.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeafShop.Controllers
{
    public class HomeController : Controller
    {
        private LeafShopDb db = new LeafShopDb();
        public ActionResult Index()
        {
            ViewBag.SanPhamMoiNhat = db.SanPhams.Select(p => p).OrderByDescending(x => x.NgayKhoiTao).Take(10);
            ViewBag.SanPhamNoiBat = db.SanPhams.Select(p => p).OrderBy(x => x.SoLuongBan).Take(10);
            return View();
        }

        [ChildActionOnly]
        public ActionResult SearchBox()
        {
            IEnumerable<DanhMuc> danhmucs = db.DanhMucs.Select(p => p);
            return PartialView(danhmucs);
        }


        public ActionResult TestLayOut()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DangNhap_DangKy()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(KhachHang user)
        {
            KhachHang kh = db.KhachHangs.Where
            (a => a.TenDangNhap.Equals(user.TenDangNhap) && a.MatKhau.Equals(user.MatKhau)).FirstOrDefault();
            if (kh != null)
            {
                if (kh.TrangThai == false)
                {
                    ModelState.AddModelError("ErrorLogin", "Tài khoản của bạn đã bị vô hiệu hóa !");
                }
                else
                {
                    Session.Add(ConstaintUser.USER_SESSION, kh);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("ErrorLogin", "Tài khoản hoặc mật khẩu không đúng!");
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            KhachHang session = (KhachHang)Session[LeafShop.Session.ConstaintUser.USER_SESSION];
            if (session != null)
            {
                return RedirectToAction("PageNotFound", "Error");
            }
            return View("Login");
        }

        [HttpPost]
        public ActionResult SignUp(KhachHang kh)
        {
            KhachHang check = db.KhachHangs.Where
                (a => a.TenDangNhap.Equals(kh.TenDangNhap)).FirstOrDefault();
            if (check != null)
            {
                ModelState.AddModelError("ErrorSignUp", "Tên đăng nhập đã tồn tại");
            }
            else
            {
                try
                {
                    kh.TrangThai = true;
                    db.KhachHangs.Add(kh);
                    db.SaveChanges();
                    KhachHang session = db.KhachHangs.Where(a => a.TenDangNhap.Equals(kh.TenDangNhap)).FirstOrDefault();
                    Session[LeafShop.Session.ConstaintUser.USER_SESSION] = session;
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ErrorSignUp", "Đăng ký không thành công. Thử lại sau !" + ex.Message);
                }
            }

            return View("Login", kh);

        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Remove(ConstaintUser.USER_SESSION);
            return RedirectToAction("Index");
        }
    }
}