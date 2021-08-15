using LeafShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeafShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        LeafShopDb db = new LeafShopDb();
        public ActionResult Index()
        {
            return View();
        }

        // GET: Cart
        [HttpGet]
        public ActionResult Orders()
        {
            List<SanPham> list = new List<SanPham>();
            if (Session[LeafShop.Session.ConstaintCart.CART] != null)
            {
                List<ChiTietDatHang> res = (List<ChiTietDatHang>)Session[LeafShop.Session.ConstaintCart.CART];
                foreach (ChiTietDatHang item in res)
                {
                    list.Add(db.SanPhams.Where(s => s.MaSanPham == item.MaSanPham).FirstOrDefault());
                }
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].ChiTietDatHangs.Add(res[i]);
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult AddToCart(ChiTietDatHang chiTiet)
        {
            bool isExists = false;
            List<ChiTietDatHang> list = new List<ChiTietDatHang>();
            if (Session[LeafShop.Session.ConstaintCart.CART] != null)
            {
                list = (List<ChiTietDatHang>)Session[LeafShop.Session.ConstaintCart.CART];
                foreach (ChiTietDatHang item in list)
                {
                    if (item.MaSanPham == chiTiet.MaSanPham)
                    {
                        item.SoLuong += chiTiet.SoLuong;
                        isExists = true;
                    }
                }
                if (!isExists)
                {
                    list.Add(chiTiet);
                }
            }
            else
            {
                list = new List<ChiTietDatHang>();
                list.Add(chiTiet);
            }
            list.RemoveAll((x) => x.SoLuong <= 0);
            foreach (ChiTietDatHang item in list)
            {
                item.DonGia = db.SanPhams.Where(s => s.MaSanPham == item.MaSanPham).FirstOrDefault().DonGia;
                SanPham sp = db.SanPhams.Where(x => x.MaSanPham == item.MaSanPham).FirstOrDefault();
                item.SanPham = sp;
            }
            Session[LeafShop.Session.ConstaintCart.CART] = list;
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getCart()
        {
            List<ChiTietDatHang> list = new List<ChiTietDatHang>();
            if (Session[LeafShop.Session.ConstaintCart.CART] != null)
            {
                list = (List<ChiTietDatHang>)Session[LeafShop.Session.ConstaintCart.CART];
            }
            foreach (ChiTietDatHang item in list)
            {
                SanPham sp = db.SanPhams.Where(x => x.MaSanPham == item.MaSanPham).FirstOrDefault();
                item.SanPham = sp;
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteFromCart(int id)
        {
            List<ChiTietDatHang> list = (List<ChiTietDatHang>)Session[LeafShop.Session.ConstaintCart.CART];
            list.RemoveAll((x) => x.MaSanPham == id);
            Session[LeafShop.Session.ConstaintCart.CART] = list;
            return Json(new {status = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CheckOut()
        {
            KhachHang kh = (KhachHang)Session[LeafShop.Session.ConstaintUser.USER_SESSION];
            if (kh == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<SanPham> list = new List<SanPham>();
            List<ChiTietDatHang> res = (List<ChiTietDatHang>)Session[LeafShop.Session.ConstaintCart.CART];
            ViewBag.TaiKhoan = kh;
            foreach (ChiTietDatHang item in res)
            {
                list.Add(db.SanPhams.Where(s => s.MaSanPham == item.MaSanPham).FirstOrDefault());
            }
            for (int i = 0; i < list.Count; i++)
            {
                list[i].ChiTietDatHangs.Add(res[i]);
            }
            return View(list);
        }
    }
}