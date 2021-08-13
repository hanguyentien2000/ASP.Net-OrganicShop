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
        //public ActionResult Products(string SearchString, string currentFilter, int? page)
        //{
        //    if (SearchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        SearchString = currentFilter;
        //    }
        //    ViewBag.CurrentFilter = SearchString;
        //    var danhmucs = db.DanhMucs.Select(d => d);
        //    if (!String.IsNullOrEmpty(SearchString))
        //    {
        //        danhmucs = danhmucs.Where(p => p.TenDanhMuc.Contains(SearchString));
        //    }
        //    int pageSize = 10;

        //    int pageNumber = (page ?? 1);
        //    return View(danhmucs.ToPagedList(pageNumber, pageSize));
        //}
        public ActionResult Category(int? id,int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var product = db.SanPhams.Where(s => s.MaDanhMuc == id).ToList();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
        }
    }
}