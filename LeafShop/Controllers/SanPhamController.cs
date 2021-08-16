using LeafShop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LeafShop.Controllers
{
    public class SanPhamController : Controller
    {
        LeafShopDb db = new LeafShopDb();
        // GET: SanPham
        public ActionResult Index(int? page,string searchString)
        {
            
            var listall = db.SanPhams.ToList();
            ViewBag.totalSP = listall.Count;

            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(listall.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult Category(int? id,int? page, string currentFilter,string currentSort)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.idCategory = id;
            var product = db.SanPhams.Where(s => s.MaDanhMuc == id).ToList();
            DanhMuc dm = db.DanhMucs.Find(id);
            if(dm.DanhMuc1 != null)
            {
                foreach (var dmCon in dm.DanhMuc1)
                {
                    var sp1 = db.SanPhams.Where(s => s.MaDanhMuc == dmCon.MaDanhMuc).ToList();
                    product = product.Concat(sp1).ToList();
                    if (dmCon.DanhMuc1 != null)
                    {
                        foreach(var dmCon2 in dmCon.DanhMuc1)
                        {
                            var sp2 = db.SanPhams.Where(s => s.MaDanhMuc == dmCon2.MaDanhMuc).ToList();
                            product = product.Concat(sp2).ToList();
                        }    
                    }    
                }
            }
            ViewBag.dmSP =  db.DanhMucs.Include("DanhMuc1").Where(p => p.DanhMuc2 == null).Select(p => p).ToList();
            switch (currentSort)
            {
                case "price-ascending":
                    product = product.OrderByDescending(s => s.DonGia).ToList();
                    break;
                case "price-descending":
                    product = product.OrderBy(s => s.TenSanPham).ToList();
                    break;
                case "title-ascending":
                    product = product.OrderBy(s => s.TenSanPham).ToList();
                    break;
                case "title-descending":
                    product = product.OrderBy(s => s.TenSanPham).ToList();
                    break;
                case "created-ascending":
                    product = product.OrderBy(s => s.MaSanPham).ToList();
                    break;
                case "created-descending":
                    product = product.OrderBy(s => s.MaSanPham).ToList();
                    break;
                case "best-selling":
                    product = product.OrderBy(s => s.SoLuongBan).ToList();
                    break;
                default:
                    product = product.ToList();
                    break;
            }
            ViewBag.totalSP = product.Count;    
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
        }
        [ChildActionOnly]
        public ActionResult CategoryTreeProduct()
        {
            IEnumerable<DanhMuc> danhmucs = db.DanhMucs.Include("DanhMuc1").Where(p => p.DanhMuc2 == null).Select(p => p);

            return PartialView("CategoryTreeProduct", danhmucs);
        }
        public ActionResult Detail(int id)
        {
            SanPham sp = db.SanPhams.Find(id);
            //List<SanPham> list = db.SanPhams.Where(s => s.MaSanPham.Equals(id)).ToList();
            //ViewBag.ChiTietSanPham = list;
            //ViewBag.Exitst = list[0];
            return View(sp);
        }
    }
}