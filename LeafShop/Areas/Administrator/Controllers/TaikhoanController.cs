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
    public class TaikhoanController : Controller
    {
        private LeafShopDb db = new LeafShopDb();

        // GET: Administrator/Taikhoan
        [HttpGet]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            ViewBag.searchString = searchString;
            ViewBag.nhanViens = db.NhanViens.Select(d => d);
            var staffs = db.Taikhoans.Select(p => p).Include(s => s.NhanVien);
            if (!String.IsNullOrEmpty(searchString))
            {
                staffs = staffs.Where(x => x.USERNAME.Contains(searchString));
            }
            return View(staffs.OrderBy(x => x.MaNhanVien).ToPagedList(page, pageSize));
        }

        // GET: Administrator/Taikhoan/Details/5
        [HttpPost]
        public JsonResult Index(string id)
        {
            Taikhoan tk = db.Taikhoans.Where(a => a.USERNAME.Equals(id)).FirstOrDefault();
            return Json(tk, JsonRequestBehavior.AllowGet);
        }

        // GET: Administrator/Taikhoan/Create
        [HttpPost]
        public JsonResult Create(Taikhoan tk)
        {
            try
            {
                var existData = db.Taikhoans.Where(x => x.MaNhanVien == tk.MaNhanVien).FirstOrDefault();
                if (existData == null)
                {
                    db.Taikhoans.Add(tk);
                    db.SaveChanges();
                    return Json(new { status = true, message = "Thêm thành công" });
                }
                else
                    return Json(new { status = false, message = "Nhân viên này đã tồn tại tài khoản" });
            }
            catch (Exception)
            {
                return Json(new { status = false, message = "Đã có lỗi xảy ra" });
            }
        }


        // GET: Administrator/Taikhoan/Edit/5
        [HttpPost]
        public JsonResult Update(Taikhoan tk)
        {
            try
            {
                Taikhoan update = db.Taikhoans.Where(a => a.USERNAME.Equals(tk.USERNAME)).FirstOrDefault();
                update.USERNAME = tk.USERNAME;
                update.PASSWORD = tk.PASSWORD;
                update.Quantri = tk.Quantri;
                update.MaNhanVien = tk.MaNhanVien;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { status = true, message = "Sửa thông tin thành công" });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return Json(new { status = false, message = "Đã có lỗi xảy ra" });
            }
        }

        // GET: Administrator/Taikhoan/Delete/5
        [HttpPost]
        public JsonResult Delete(string id)
        {
            try
            {
                Taikhoan tk = db.Taikhoans.Where(a => a.USERNAME.Equals(id)).FirstOrDefault();
                db.Taikhoans.Remove(tk);
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
