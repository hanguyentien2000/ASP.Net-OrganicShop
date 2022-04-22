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
    public class SanPhamController : Controller
    {
        private LeafShopDb db = new LeafShopDb();

        [HttpGet]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            ViewBag.searchString = searchString;
            ViewBag.danhMucs = db.DanhMucs.Select(d => d);
            ViewBag.thuongHieus = db.ThuongHieux.Select(d => d);
            var products = db.SanPhams.Select(p => p).Include(s => s.ThuongHieu).Include(s => s.DanhMuc);
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(x => x.TenSanPham.Contains(searchString));
            }
            return View(products.OrderBy(x => x.MaSanPham).ToPagedList(page, pageSize));
        }

        //[HttpPost]
        //[ValidateInput(false)]

        //public JsonResult Create(string sanpham, HttpPostedFileBase uploadhinh)
        //{
        //    try
        //    {
        //        JavaScriptSerializer convert = new JavaScriptSerializer();
        //        SanPham sp = convert.Deserialize<SanPham>(sanpham);
        //        sp.NgayKhoiTao = DateTime.Now;
        //        sp.NgayCapNhat = DateTime.Now;

        //        var f = uploadhinh;
        //        if (f != null && f.ContentLength > 0)
        //        {
        //            string fileName = new Random().Next() + System.IO.Path.GetFileName(f.FileName);
        //            string uploadPath = Server.MapPath("~/Areas/UploadFile/SanPham/" + fileName);
        //            f.SaveAs(uploadPath);
        //            sp.HinhMinhHoa = "/Areas/UploadFile/SanPham/" + fileName;
        //        }
        //        else
        //        {
        //            sp.HinhMinhHoa = "";
        //        }
        //        db.SanPhams.Add(sp);
        //        db.SaveChanges();

        //        return Json(new { status = true, message = "Thêm thành công!" });
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.error = ex.Message;
        //        return Json(new { status = false, message = "Đã có lỗi xảy ra!" });
        //    }
        //}

        //[HttpPost]
        //[ValidateInput(false)]

        //public JsonResult Update(string sanpham, HttpPostedFileBase uploadhinh)
        //{
        //    try
        //    {
        //        JavaScriptSerializer convert = new JavaScriptSerializer();
        //        SanPham sp = convert.Deserialize<SanPham>(sanpham);
        //        SanPham update = db.SanPhams.Where(s => s.MaSanPham.Equals(sp.MaSanPham)).FirstOrDefault();
        //        var f = uploadhinh;
        //        if (f != null && f.ContentLength > 0)
        //        {
        //            string fileName = new Random().Next() + System.IO.Path.GetFileName(f.FileName);
        //            string uploadPath = Server.MapPath("~/Areas/UploadFile/SanPham/" + fileName);
        //            f.SaveAs(uploadPath);
        //            update.HinhMinhHoa = "/Areas/UploadFile/SanPham/" + fileName;
        //        }
        //        update.MaDanhMuc = sp.MaDanhMuc;
        //        update.TenSanPham = sp.TenSanPham;
        //        update.MaThuongHieu = sp.MaThuongHieu;
        //        update.MoTa = sp.MoTa;
        //        update.NgayCapNhat = DateTime.Now;
        //        update.SoLuong = sp.SoLuong;
        //        update.SoLuongBan = sp.SoLuongBan;
        //        update.DonGia = sp.DonGia;
        //        update.DonViTinh = sp.DonViTinh;
        //        db.Entry(update).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return Json(new { status = true, message = "Sửa thông tin thành công" });
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { status = false, message = "Sửa thông tin không thành công" });
        //    }
        //}


        //[HttpPost]
        //public JsonResult Delete(int id)
        //{
        //    try
        //    {
        //        SanPham sp = db.SanPhams.Where(a => a.MaSanPham.Equals(id)).FirstOrDefault();
        //        db.SanPhams.Remove(sp);
        //        db.SaveChanges();
        //        return Json(new { status = true });
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = "Không xoá được bản ghi này!" + " " + ex.Message;

        //        return Json(new { status = false });
        //    }
        //}

        //[HttpPost]
        //public JsonResult Index(int id)
        //{
        //    SanPham sp = db.SanPhams.Where(s => s.MaSanPham.Equals(id)).FirstOrDefault();
        //    return Json(sp, JsonRequestBehavior.AllowGet);
        //}
    }
}
