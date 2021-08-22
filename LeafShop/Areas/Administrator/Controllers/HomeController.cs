using LeafShop.Models;
using LeafShop.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeafShop.Areas.Administrator.Controllers
{
    public class HomeController : Controller
    {
        LeafShopDb db = new LeafShopDb();
        // GET: Administrator/Home

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            Taikhoan user = db.Taikhoans.SingleOrDefault(x => x.USERNAME == username && x.PASSWORD == password);
            if(user != null)
            {
                Session["username"] = user.USERNAME;
                
                Session.Add(ConstaintUser.ADMIN_SESSION, user);
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu!";
            return View();
        }


        public ActionResult Index()
        {
            ViewBag.TaiKhoan = db.Taikhoans.Select(p => p);
            return View();
        }
    }
}