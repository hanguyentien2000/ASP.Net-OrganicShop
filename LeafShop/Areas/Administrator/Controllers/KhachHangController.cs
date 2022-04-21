using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeafShop.Models;
using PagedList;

namespace LeafShop.Areas.Administrator.Controllers
{
    public class KhachHangController : Controller
    {
        private LeafShopDb db = new LeafShopDb();

        // GET: Administrator/KhachHang
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            ViewBag.searchString = searchString;
            var khachhangs = db.KhachHangs.Select(kh => kh);
            if (!String.IsNullOrEmpty(searchString))
            {
                khachhangs = khachhangs.Where(dm => dm.TenKhachHang.Contains(searchString));
            }
            return View(khachhangs.OrderBy(dm => dm.MaKhachHang).ToPagedList(page, pageSize));
        }

        // GET: Administrator/KhachHang/Details/5
        [HttpPost]
        public JsonResult Index(int id)
        {
            KhachHang kh = db.KhachHangs.Where(a => a.MaKhachHang.Equals(id)).FirstOrDefault();
            return Json(kh, JsonRequestBehavior.AllowGet);
        }

        // GET: Administrator/KhachHang/Create
        [HttpPost]
        public JsonResult Create(KhachHang kh)
        {
            try
            {
                KhachHang existData = db.KhachHangs.FirstOrDefault(x => x.TenDangNhap == kh.TenDangNhap);
                if (existData != null)
                {
                    return Json(new { status = false, message = "Đã tồn tại tên đăng nhập này" });
                }
                else
                {
                    db.KhachHangs.Add(kh);
                    db.SaveChanges();
                    return Json(new { status = true, message = "Thêm thành công" });
                }
            }
            catch (Exception)
            {
                return Json(new { status = false, message = "Đã có lỗi xảy ra" });
            }
        }

        // GET: Administrator/KhachHang/Edit/5
        [HttpPost]
        public JsonResult Update(KhachHang kh)
        {
            try
            {
                KhachHang update = db.KhachHangs.Where(a => a.MaKhachHang.Equals(kh.MaKhachHang)).FirstOrDefault();
                update.TenKhachHang = kh.TenKhachHang;
                update.TenDangNhap = kh.TenDangNhap;
                update.NgaySinh = kh.NgaySinh;
                update.GioiTinh = kh.GioiTinh;
                update.TrangThai = kh.TrangThai;
                update.DiaChiKhachHang = kh.DiaChiKhachHang;
                update.DienThoaiKhachHang = kh.DienThoaiKhachHang;
                update.Email = kh.Email;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { status = true, message = "Sửa thông tin thành công" });
            }
            catch (Exception)
            {
                return Json(new { status = false, message = "Đã có lỗi xảy ra" });
            }
        }

        // GET: Administrator/KhachHang/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                KhachHang kh = db.KhachHangs.Where(a => a.MaKhachHang.Equals(id)).FirstOrDefault();
                db.KhachHangs.Remove(kh);
                db.SaveChanges();
                return Json(new { status = true });
            }
            catch (Exception)
            {
                return Json(new { status = false });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
