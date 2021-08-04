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
    public class DatHangController : Controller
    {
        private LeafShopDb db = new LeafShopDb();

        // GET: Administrator/DatHang
        public ActionResult Index(string SearchString, string currentFilter, int? page)
        {
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            //var datHangs = db.DatHangs.Select(d => d.KhachHang).Select(d => d.NhanVien);
            IQueryable<DatHang> datHangs = (from hang in db.DatHangs
                                            select hang)
                    .OrderBy(student => student.MaDatHang);
            if (!String.IsNullOrEmpty(SearchString))
            {
                datHangs = datHangs.Where(p => p.MaDatHang.Contains(SearchString));
            }
            int pageSize = 5;

            int pageNumber = (page ?? 1);
            return View(datHangs.ToPagedList(pageNumber, pageSize));
        }

        // GET: Administrator/DatHang/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatHang datHang = db.DatHangs.Find(id);
            if (datHang == null)
            {
                return HttpNotFound();
            }
            return View(datHang);
        }

        // GET: Administrator/DatHang/Create
        public ActionResult Create()
        {
            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "TenKhachHang");
            ViewBag.MaNhanVien = new SelectList(db.NhanViens, "MaNhanVien", "TenNhanVien");
            return View();
        }

        // POST: Administrator/DatHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDatHang,MaKhachHang,MaNhanVien,TongTien,NgayKhoiTao")] DatHang datHang)
        {
            if (ModelState.IsValid)
            {
                db.DatHangs.Add(datHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "TenKhachHang", datHang.MaKhachHang);
            ViewBag.MaNhanVien = new SelectList(db.NhanViens, "MaNhanVien", "TenNhanVien", datHang.MaNhanVien);
            return View(datHang);
        }

        // GET: Administrator/DatHang/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatHang datHang = db.DatHangs.Find(id);
            if (datHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "TenKhachHang", datHang.MaKhachHang);
            ViewBag.MaNhanVien = new SelectList(db.NhanViens, "MaNhanVien", "TenNhanVien", datHang.MaNhanVien);
            return View(datHang);
        }

        // POST: Administrator/DatHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDatHang,MaKhachHang,MaNhanVien,TongTien,NgayKhoiTao")] DatHang datHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "TenKhachHang", datHang.MaKhachHang);
            ViewBag.MaNhanVien = new SelectList(db.NhanViens, "MaNhanVien", "TenNhanVien", datHang.MaNhanVien);
            return View(datHang);
        }

        // GET: Administrator/DatHang/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatHang datHang = db.DatHangs.Find(id);
            if (datHang == null)
            {
                return HttpNotFound();
            }
            return View(datHang);
        }

        // POST: Administrator/DatHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DatHang datHang = db.DatHangs.Find(id);
            db.DatHangs.Remove(datHang);
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
