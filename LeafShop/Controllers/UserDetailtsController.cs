using LeafShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeafShop.Controllers
{
    public class UserDetailtsController : Controller
    {
        // GET: UserDetailts
        LeafShopDb db = new LeafShopDb();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserInfor(int id)
        {
            KhachHang session = (KhachHang)Session[LeafShop.Session.ConstaintUser.USER_SESSION];
            if (session == null)
            {
                return RedirectToAction("PageNotFound", "Error");
            }
            else
            {
                KhachHang tk = db.KhachHangs.Where(a => a.MaKhachHang.Equals(id)).FirstOrDefault();
                return View(tk);
            }
        }

        [HttpPost]
        public ActionResult UserInfor([Bind(Include = "MaKhachHang,TenKhachHang,DiaChiKhachHang,SoDienThoai")] KhachHang kh)
        {
            KhachHang res = db.KhachHangs.Where(a => a.MaKhachHang.Equals(kh.MaKhachHang)).FirstOrDefault();
            try
            {
                res.TenKhachHang = kh.TenKhachHang;
                res.DiaChiKhachHang = kh.DiaChiKhachHang;
                res.DienThoaiKhachHang = kh.DienThoaiKhachHang;
                db.SaveChanges();
                Session[LeafShop.Session.ConstaintUser.USER_SESSION] = res;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ErrorUpdate", "Cập nhật thông tin không thành công ! Thử lại sau !" + ex.Message);
            }
            return View(res);
        }
    }
}