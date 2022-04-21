using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using LeafShop.Models;
using PagedList;

namespace LeafShop.Areas.Administrator.Controllers
{
    public class NhanVienController : Controller
    {
        private LeafShopDb db = new LeafShopDb();

        [HttpGet]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            ViewBag.searchString = searchString;
            var staffs = db.NhanViens.Select(p => p);
            if (!String.IsNullOrEmpty(searchString))
            {
                staffs = staffs.Where(x => x.TenNhanVien.Contains(searchString));
            }
            return View(staffs.OrderBy(x => x.MaNhanVien).ToPagedList(page, pageSize));
        }

        [HttpPost]
        public JsonResult Create(string nhanvien, HttpPostedFileBase uploadhinh)
        {
            try
            {
                JavaScriptSerializer convert = new JavaScriptSerializer();
                NhanVien nv = convert.Deserialize<NhanVien>(nhanvien);
                var f = uploadhinh;
                if (f != null && f.ContentLength > 0)
                {
                    string fileName = new Random().Next() + System.IO.Path.GetFileName(f.FileName);
                    string uploadPath = Server.MapPath("~/Areas/UploadFile/NhanVien/" + fileName);
                    f.SaveAs(uploadPath);
                    nv.Avatar = "/Areas/UploadFile/NhanVien/" + fileName;
                }
                else
                {
                    nv.Avatar = "";
                }
                db.NhanViens.Add(nv);
                db.SaveChanges();

                return Json(new { status = true, message = "Thêm thành công!" });
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return Json(new { status = false, message = "Đã có lỗi xảy ra!" });
            }
        }

        [HttpPost]
        public JsonResult Update(string nhanvien, HttpPostedFileBase uploadhinh)
        {
            try
            {
                JavaScriptSerializer convert = new JavaScriptSerializer();
                NhanVien nv = convert.Deserialize<NhanVien>(nhanvien);
                NhanVien update = db.NhanViens.Where(s => s.MaNhanVien.Equals(nv.MaNhanVien)).FirstOrDefault();
                var f = uploadhinh;
                if (f != null && f.ContentLength > 0)
                {
                    string fileName = new Random().Next() + System.IO.Path.GetFileName(f.FileName);
                    string uploadPath = Server.MapPath("~/Areas/UploadFile/NhanVien/" + fileName);
                    f.SaveAs(uploadPath);
                    update.Avatar = "/Areas/UploadFile/NhanVien/" + fileName;
                }
                update.MaNhanVien = nv.MaNhanVien;
                update.TenNhanVien = nv.TenNhanVien;
                update.NgaySinh = nv.NgaySinh;
                update.DiaChi = nv.DiaChi;
                update.DienThoai = nv.DienThoai;
                update.Email = nv.Email;
                update.GioiTinh = nv.GioiTinh;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { status = true, message = "Sửa thông tin thành công" });
            }
            catch (Exception)
            {
                return Json(new { status = false, message = "Sửa thông tin không thành công" });
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                NhanVien nv = db.NhanViens.Where(a => a.MaNhanVien.Equals(id)).FirstOrDefault();
                var data = db.Taikhoans.Where(x => x.MaNhanVien == id).ToList();
                foreach (var item in data)
                {
                    db.Taikhoans.Remove(item);
                }
                db.NhanViens.Remove(nv);
                db.SaveChanges();
                return Json(new { status = true });
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không xoá được bản ghi này!" + " " + ex.Message;

                return Json(new { status = false });
            }
        }

        [HttpPost]
        public JsonResult Index(int id)
        {
            NhanVien sp = db.NhanViens.Where(s => s.MaNhanVien.Equals(id)).FirstOrDefault();
            return Json(sp, JsonRequestBehavior.AllowGet);
        }
    }
}

