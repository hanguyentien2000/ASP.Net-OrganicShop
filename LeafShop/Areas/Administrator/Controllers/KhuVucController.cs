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
    public class KhuVucController : Controller
    {
        private LeafShopDb db = new LeafShopDb();

        // GET: Administrator/KhuVuc
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
            //var khuvucs = db.KhuVucs.Select(d => d);
            IQueryable<KhuVuc> khuvucs = (from kv in db.KhuVucs
                                            select kv)
                    .OrderBy(x => x.MaKhuVuc);
            if (!String.IsNullOrEmpty(SearchString))
            {
                khuvucs = khuvucs.Where(p => p.TenKhuVuc.Contains(SearchString));
            }
            int pageSize = 5;

            int pageNumber = (page ?? 1);
            return View(khuvucs.ToPagedList(pageNumber, pageSize));
        }

        // GET: Administrator/KhuVuc/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhuVuc khuVuc = db.KhuVucs.Find(id);
            if (khuVuc == null)
            {
                return HttpNotFound();
            }
            return View(khuVuc);
        }

        // GET: Administrator/KhuVuc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/KhuVuc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKhuVuc,TenKhuVuc")] KhuVuc khuVuc)
        {
            try
            {
                KhuVuc existData = db.KhuVucs.FirstOrDefault(x => x.MaKhuVuc == khuVuc.MaKhuVuc);
                if (existData != null)
                {
                    ViewBag.Error = "Mã khu vực đã tồn tại";
                    return View(khuVuc);
                }
                else if (existData == null)
                {
                    if (ModelState.IsValid)
                    {
                        db.KhuVucs.Add(khuVuc);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!";
                return View(khuVuc);
            }
        }

        // GET: Administrator/KhuVuc/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhuVuc khuVuc = db.KhuVucs.Find(id);
            if (khuVuc == null)
            {
                return HttpNotFound();
            }
            return View(khuVuc);
        }

        // POST: Administrator/KhuVuc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKhuVuc,TenKhuVuc")] KhuVuc khuVuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khuVuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khuVuc);
        }

        // GET: Administrator/KhuVuc/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhuVuc khuVuc = db.KhuVucs.Find(id);
            if (khuVuc == null)
            {
                return HttpNotFound();
            }
            return View(khuVuc);
        }

        // POST: Administrator/KhuVuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhuVuc khuVuc = db.KhuVucs.Find(id);
            db.KhuVucs.Remove(khuVuc);
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
