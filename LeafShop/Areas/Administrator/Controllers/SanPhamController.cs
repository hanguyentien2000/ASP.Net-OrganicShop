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
    public class SanPhamController : Controller
    {
        private LeafShopDb db = new LeafShopDb();

        // GET: Administrator/SanPham
        //public ActionResult Index()
        //{
        //    var sanPhams = db.SanPhams.Include(s => s.DanhMuc).Include(s => s.KhuVuc).Include(s => s.ThuongHieu);
        //    return View(sanPhams.ToList());
        //}

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
            //var thuonghieus = db.ThuongHieux.Select(d => d);
            IQueryable<SanPham> sanphams = (from sp in db.SanPhams
                                                  select sp)
                    .OrderBy(x => x.MaSanPham);
            if (!String.IsNullOrEmpty(SearchString))
            {
                sanphams = sanphams.Where(p => p.TenSanPham.Contains(SearchString));
            }
            int pageSize = 5;

            int pageNumber = (page ?? 1);
            return View(sanphams.ToPagedList(pageNumber, pageSize));
        }

        // GET: Administrator/SanPham/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: Administrator/SanPham/Create
        public ActionResult Create()
        {
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaKhuVuc = new SelectList(db.KhuVucs, "MaKhuVuc", "TenKhuVuc");
            ViewBag.MaThuongHieu = new SelectList(db.ThuongHieux, "MaThuongHieu", "TenThuongHieu");
            return View();
        }

        // POST: Administrator/SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSanPham,TenSanPham,MaDanhMuc,MaThuongHieu,MaKhuVuc,DonViTinh,SoLuong,SoLuongBan,DonGia,MoTa,NgayCapNhat,HinhMinhHoa,BinhLuan")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
            ViewBag.MaKhuVuc = new SelectList(db.KhuVucs, "MaKhuVuc", "TenKhuVuc", sanPham.MaKhuVuc);
            ViewBag.MaThuongHieu = new SelectList(db.ThuongHieux, "MaThuongHieu", "TenThuongHieu", sanPham.MaThuongHieu);
            return View(sanPham);
        }

        // GET: Administrator/SanPham/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
            ViewBag.MaKhuVuc = new SelectList(db.KhuVucs, "MaKhuVuc", "TenKhuVuc", sanPham.MaKhuVuc);
            ViewBag.MaThuongHieu = new SelectList(db.ThuongHieux, "MaThuongHieu", "TenThuongHieu", sanPham.MaThuongHieu);
            return View(sanPham);
        }

        // POST: Administrator/SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSanPham,TenSanPham,MaDanhMuc,MaThuongHieu,MaKhuVuc,DonViTinh,SoLuong,SoLuongBan,DonGia,MoTa,NgayCapNhat,HinhMinhHoa,BinhLuan")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
            ViewBag.MaKhuVuc = new SelectList(db.KhuVucs, "MaKhuVuc", "TenKhuVuc", sanPham.MaKhuVuc);
            ViewBag.MaThuongHieu = new SelectList(db.ThuongHieux, "MaThuongHieu", "TenThuongHieu", sanPham.MaThuongHieu);
            return View(sanPham);
        }

        // GET: Administrator/SanPham/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Administrator/SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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
