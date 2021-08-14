using LeafShop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeafShop.Controllers
{
    public class BlogController : Controller
    {
        LeafShopDb db = new LeafShopDb();
        // GET: Blog
        public ActionResult Index(int? page)
        {
            var blogs = db.Blogs.ToList();
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(blogs.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult Detail(int id)
        {
            Blog bl = db.Blogs.Find(id);
            //List<SanPham> list = db.SanPhams.Where(s => s.MaSanPham.Equals(id)).ToList();
            //ViewBag.ChiTietSanPham = list;
            //ViewBag.Exitst = list[0];
            return View(bl);
        }
    }
}