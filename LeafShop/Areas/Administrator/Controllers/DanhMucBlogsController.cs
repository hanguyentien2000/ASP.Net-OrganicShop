using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeafShop.Models;

namespace LeafShop.Areas.Administrator.Controllers
{
    public class DanhMucBlogsController : Controller
    {
        private LeafShopDb db = new LeafShopDb();

        // GET: Administrator/DanhMucBlogs
        public ActionResult Index()
        {
            return View(db.DanhMucBlogs.ToList());
        }

        // GET: Administrator/DanhMucBlogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMucBlog danhMucBlog = db.DanhMucBlogs.Find(id);
            if (danhMucBlog == null)
            {
                return HttpNotFound();
            }
            return View(danhMucBlog);
        }

        // GET: Administrator/DanhMucBlogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/DanhMucBlogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDanhMucBlog,TenDanhMucBlog")] DanhMucBlog danhMucBlog)
        {
            if (ModelState.IsValid)
            {
                db.DanhMucBlogs.Add(danhMucBlog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(danhMucBlog);
        }

        // GET: Administrator/DanhMucBlogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMucBlog danhMucBlog = db.DanhMucBlogs.Find(id);
            if (danhMucBlog == null)
            {
                return HttpNotFound();
            }
            return View(danhMucBlog);
        }

        // POST: Administrator/DanhMucBlogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDanhMucBlog,TenDanhMucBlog")] DanhMucBlog danhMucBlog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhMucBlog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(danhMucBlog);
        }

        // GET: Administrator/DanhMucBlogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMucBlog danhMucBlog = db.DanhMucBlogs.Find(id);
            if (danhMucBlog == null)
            {
                return HttpNotFound();
            }
            return View(danhMucBlog);
        }

        // POST: Administrator/DanhMucBlogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DanhMucBlog danhMucBlog = db.DanhMucBlogs.Find(id);
            db.DanhMucBlogs.Remove(danhMucBlog);
            db.SaveChanges();
            return RedirectToAction("Index");
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
