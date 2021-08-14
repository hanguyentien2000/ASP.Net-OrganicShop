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
        // GET: SanPham
        public ActionResult Index()
        {
            var dbContext = new LeafShopDb();
            List<SanPham> listall = dbContext.SanPhams.ToList();
            return View();
        }
        LeafShopDb db = new LeafShopDb();
        
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
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CategoryDetails(int id)
        {
            SanPham sp = db.SanPhams.Include("DanhMuc").Where(s => s.MaSanPham.Equals(id)).FirstOrDefault();
            List<SanPham> list = db.SanPhams.Where(s => s.MaSanPham.Equals(id)).ToList();
            ViewBag.ChiTietSanPham = list;
            ViewBag.Exitst = list[0];
            return View(sp);
        }
    }
}