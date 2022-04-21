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
    public class DanhMucBlogsController : Controller
    {
        private LeafShopDb db = new LeafShopDb();

        // GET: Administrator/DanhMucBlogs
        [HttpGet]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            ViewBag.searchString = searchString;
            var danhmucs = db.DanhMucBlogs.Select(dm => dm);
            if (!String.IsNullOrEmpty(searchString))
            {
                danhmucs = danhmucs.Where(dm => dm.TenDanhMucBlog.Contains(searchString));
            }
            return View(danhmucs.OrderBy(dm => dm.MaDanhMucBlog).ToPagedList(page, pageSize));
        }

        [HttpPost]
        public JsonResult Index(int id)
        {
            DanhMucBlog dm = db.DanhMucBlogs.Where(a => a.MaDanhMucBlog.Equals(id)).FirstOrDefault();
            return Json(dm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(DanhMucBlog dm)
        {
            try
            {
                var existData = db.DanhMucBlogs.Where(x => x.TenDanhMucBlog == dm.TenDanhMucBlog).FirstOrDefault();
                if(existData == null)
                {
                    db.DanhMucBlogs.Add(dm);
                    db.SaveChanges();
                    return Json(new { status = true, message = "Thêm thành công" });
                }
                else
                    return Json(new { status = false, message = "Tên danh mục blog đã tồn tại" });

            }
            catch (Exception)
            {
                return Json(new { status = false, message = "Đã có lỗi xảy ra" });
            }
        }

        // GET: Administrator/DanhMucBlogs/Edit/5
        [HttpPost]
        public JsonResult Update(DanhMucBlog dm)
        {
            try
            {
                var existData = db.DanhMucBlogs.Where(x => x.TenDanhMucBlog == dm.TenDanhMucBlog).FirstOrDefault();
                if (existData == null)
                {
                    DanhMucBlog update = db.DanhMucBlogs.Where(a => a.MaDanhMucBlog.Equals(dm.MaDanhMucBlog)).FirstOrDefault();
                    update.TenDanhMucBlog = dm.TenDanhMucBlog;
                    update.MaDanhMucBlog = dm.MaDanhMucBlog;
                    db.Entry(update).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { status = true, message = "Sửa thông tin thành công" });
                }
                else
                    return Json(new { status = false, message = "Tên danh mục blog đã tồn tại" });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return Json(new { status = false, message = "Đã có lỗi xảy ra" });
            }
        }
        // GET: Administrator/DanhMucBlogs/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                DanhMucBlog dm = db.DanhMucBlogs.Where(a => a.MaDanhMucBlog.Equals(id)).FirstOrDefault();
                db.DanhMucBlogs.Remove(dm);
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
