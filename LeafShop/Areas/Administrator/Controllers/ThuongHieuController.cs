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
    public class ThuongHieuController : Controller
    {
        LeafShopDb db = new LeafShopDb();

        [HttpGet]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            ViewBag.searchString = searchString;
            var brands = db.ThuongHieux.Select(p => p);
            if (!String.IsNullOrEmpty(searchString))
            {
                brands = brands.Where(x => x.TenThuongHieu.Contains(searchString));
            }
            return View(brands.OrderBy(x => x.MaThuongHieu).ToPagedList(page, pageSize));
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(string thuonghieu, HttpPostedFileBase uploadhinh)
        {
            try
            {
                JavaScriptSerializer convert = new JavaScriptSerializer();
                ThuongHieu th = convert.Deserialize<ThuongHieu>(thuonghieu);
                var existData = db.ThuongHieux.Select(d => d).Where(x => x.MaThuongHieu == th.MaThuongHieu);

                var f = uploadhinh;
                if (f != null && f.ContentLength > 0)
                {
                    string fileName = new Random().Next() + System.IO.Path.GetFileName(f.FileName);
                    string uploadPath = Server.MapPath("~/Areas/UploadFile/ThuongHieu/" + fileName);
                    f.SaveAs(uploadPath);
                    th.AnhThuongHieu = "/Areas/UploadFile/ThuongHieu/" + fileName;
                }
                else
                {
                    th.AnhThuongHieu = "";
                }
                db.ThuongHieux.Add(th);
                db.SaveChanges();

                return Json(new { status = true, message = "Thêm thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Thêm không thành công - Lỗi " + ex.Message });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Update(string thuonghieu, HttpPostedFileBase uploadhinh)
        {
            try
            {
                JavaScriptSerializer convert = new JavaScriptSerializer();
                ThuongHieu th = convert.Deserialize<ThuongHieu>(thuonghieu);
                ThuongHieu update = db.ThuongHieux.Where(s => s.MaThuongHieu.Equals(th.MaThuongHieu)).FirstOrDefault();
                var f = uploadhinh;
                if (f != null && f.ContentLength > 0)
                {
                    string fileName = new Random().Next() + System.IO.Path.GetFileName(f.FileName);
                    string uploadPath = Server.MapPath("~/Areas/UploadFile/ThuongHieu/" + fileName);
                    f.SaveAs(uploadPath);
                    update.AnhThuongHieu = "/Areas/UploadFile/ThuongHieu/" + fileName;
                }
                update.TenThuongHieu = th.TenThuongHieu;
                update.MoTaThuongHieu = th.MoTaThuongHieu;
                update.DienThoaiThuongHieu = th.DienThoaiThuongHieu;
                update.DiaChiThuongHieu = th.DiaChiThuongHieu;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { status = true, message = "Sửa thông tin thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Sửa không thành công - Lỗi " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                ThuongHieu th = db.ThuongHieux.Where(a => a.MaThuongHieu.Equals(id)).FirstOrDefault();
                db.ThuongHieux.Remove(th);
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
            ThuongHieu th = db.ThuongHieux.Where(s => s.MaThuongHieu.Equals(id)).FirstOrDefault();
            return Json(th, JsonRequestBehavior.AllowGet);
        }
    }
}
