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
        public ActionResult Index(int? page)
        {
            
            var listall = db.SanPhams.ToList();
            ViewBag.totalSP = listall.Count;

            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(listall.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult Category(int? id,int? page, string SearchString, string currentFilter)
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

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var product = db.SanPhams.Where(s => s.MaDanhMuc == id).ToList();
            ViewBag.totalSP = product.Count;    
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
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