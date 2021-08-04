using LeafShop.Models;
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
            Taikhoan user = db.Taikhoans.SingleOrDefault(x => x.USERNAME == username && x.PASSWORD == password && x.Quantri == true);
            if(user != null)
            {
                Session["username"] = user.USERNAME;
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu!";
            return View();
        }


        public ActionResult Index()
        {
            return View();
        }
    }
}