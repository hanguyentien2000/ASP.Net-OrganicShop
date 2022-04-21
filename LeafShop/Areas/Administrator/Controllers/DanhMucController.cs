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
    public class DanhMucController : Controller
    {
        private LeafShopDb db = new LeafShopDb();

        // GET: Administrator/DanhMuc
        [HttpGet]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            ViewBag.searchString = searchString;
            ViewBag.parentId = db.DanhMucs.Select(d => d).Where(x => x.ParentId == null).Include("DanhMuc2").OrderBy(x => x.MaDanhMuc);
            ViewBag.parentId2 = db.DanhMucs.Select(d => d).Include("DanhMuc2").OrderBy(x => x.MaDanhMuc).Distinct();

            var categories = db.DanhMucs.Include("DanhMuc2").OrderBy(x => x.MaDanhMuc).Select(p => p);
            if (!String.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(x => x.TenDanhMuc.Contains(searchString));
            }
            return View(categories.OrderBy(x => x.MaDanhMuc).ToPagedList(page, pageSize));
        }

        [HttpPost]
        public JsonResult Index(int id)
        {
            DanhMuc dm = db.DanhMucs.Where(a => a.MaDanhMuc.Equals(id)).FirstOrDefault();
            return Json(dm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(DanhMuc dm)
        {
            try
            {
                db.DanhMucs.Add(dm);
                db.SaveChanges();
                return Json(new { status = true, message = "Thêm thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message  });
            }
        }

        // GET: Administrator/DanhMuc/Edit/5
        [HttpPost]
        public JsonResult Update(DanhMuc dm)
        {
            try
            {
                DanhMuc update = db.DanhMucs.Where(a => a.MaDanhMuc.Equals(dm.MaDanhMuc)).FirstOrDefault();
                update.TenDanhMuc = dm.TenDanhMuc;
                update.ParentId = dm.ParentId;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { status = true, message = "Sửa thông tin thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                DanhMuc dm = db.DanhMucs.Where(a => a.MaDanhMuc == id).FirstOrDefault();
                db.DanhMucs.Remove(dm);
                db.SaveChanges();
                return Json(new { status = true });
            }
            catch (Exception)
            {
                return Json(new { status = false });
            }
        }


        [HttpGet]
       
        public ActionResult GetAllParentId()
        {
            try
            {
                var res = db.DanhMucs.Select(s => s.ParentId);
                return View(res);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Đã có lỗi " + ex.Message;
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
