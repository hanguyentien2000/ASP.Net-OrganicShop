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
    public class BlogController : Controller
    {
        private LeafShopDb db = new LeafShopDb();

        // GET: Administrator/Blog
        [HttpGet]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            ViewBag.searchString = searchString;
            ViewBag.nhanViens = db.NhanViens.Select(d => d);
            ViewBag.danhMucBlogs = db.DanhMucBlogs.Select(d => d);
            var staffs = db.Blogs.Select(p => p).Include(s => s.NhanVien).Include(s=> s.DanhMucBlog);
            if (!String.IsNullOrEmpty(searchString))
            {
                staffs = staffs.Where(x => x.TieuDe.Contains(searchString));
            }
            return View(staffs.OrderBy(x => x.MaBaiViet).ToPagedList(page, pageSize));
        }

        // GET: Administrator/Blog/Details/5
        [HttpPost]
        public JsonResult Index(int id)
        {
            Blog blogs = db.Blogs.Where(s => s.MaBaiViet.Equals(id)).FirstOrDefault();
            return Json(blogs, JsonRequestBehavior.AllowGet);
        }

        // GET: Administrator/Blog/Create
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(string blog, HttpPostedFileBase uploadhinh)
        {
            try
            {
                JavaScriptSerializer convert = new JavaScriptSerializer();
                Blog blogs = convert.Deserialize<Blog>(blog);
                blogs.NgayKhoiTao = DateTime.Now;
                
                var f = uploadhinh;
                if (f != null && f.ContentLength > 0)
                {
                    string fileName = new Random().Next() + System.IO.Path.GetFileName(f.FileName);
                    string uploadPath = Server.MapPath("~/Areas/UploadFile/Blog/" + fileName);
                    f.SaveAs(uploadPath);
                    blogs.Anh = "/Areas/UploadFile/Blog/" + fileName;
                }
                else
                {
                    blogs.Anh = "";
                }
                db.Blogs.Add(blogs);
                db.SaveChanges();

                return Json(new { status = true, message = "Thêm thành công!" });
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return Json(new { status = false, message = "Đã có lỗi xảy ra!" });
            }
        }

        // GET: Administrator/Blog/Edit/5
        
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Update(string blog, HttpPostedFileBase uploadhinh)
        {
            try
            {
                JavaScriptSerializer convert = new JavaScriptSerializer();
                Blog sp = convert.Deserialize<Blog>(blog);
                Blog update = db.Blogs.Where(s => s.MaBaiViet.Equals(sp.MaBaiViet)).FirstOrDefault();
                var f = uploadhinh;
                if (f != null && f.ContentLength > 0)
                {
                    string fileName = new Random().Next() + System.IO.Path.GetFileName(f.FileName);
                    string uploadPath = Server.MapPath("~/Areas/UploadFile/Blog/" + fileName);
                    f.SaveAs(uploadPath);
                    update.Anh = "/Areas/UploadFile/Blog/" + fileName;
                }
                update.MaDanhMucBlog = sp.MaDanhMucBlog;
                update.MaNhanVien = sp.MaNhanVien;
                update.TieuDe = sp.TieuDe;
                update.Tomtat = sp.Tomtat;
                update.Noidung = sp.Noidung;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { status = true, message = "Sửa thông tin thành công" });
            }
            catch (Exception)
            {
                return Json(new { status = false, message = "Sửa thông tin không thành công" });
            }
        }

        // GET: Administrator/Blog/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                Blog blog = db.Blogs.Where(a => a.MaBaiViet.Equals(id)).FirstOrDefault();
                var blog1 = db.NhanViens.Select(d => d).Where(x => x.MaNhanVien == blog.MaNhanVien);
                var blog2 = db.DanhMucBlogs.Select(d => d).Where(x => x.MaDanhMucBlog == blog.MaDanhMucBlog);
                foreach (var item in blog1)
                {
                    db.NhanViens.Remove(item);
                }
                foreach (var item in blog2)
                {
                    db.DanhMucBlogs.Remove(item);
                }
                db.Blogs.Remove(blog);
                db.SaveChanges();
                return Json(new { status = true });
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không xoá được bản ghi này!" + " " + ex.Message;

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