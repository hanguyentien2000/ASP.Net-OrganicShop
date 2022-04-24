using LeafShop.Models;
using MoMo;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeafShop.Controllers
{
    public class MomoController : Controller
    {
        LeafShopDb db = new LeafShopDb();

        // GET: Momo
        public ActionResult Index()
        {
            return View();
        }
        int tongTien;
        public ActionResult Payment([Bind(Include = "GhiChu,DiaChi")] DatHang dh)
        {
            string diaChi = dh.DiaChi;
            string ghiChu = dh.GhiChu;

            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOOJOI20210710";
            string accessKey = "iPXneGmrJH0G8FOP";
            string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
            string orderInfo = "Thanh toán đơn hàng RND";
            string returnUrl = "https://localhost:44352/Home/Index";
            string notifyurl = "http://ba1adf48beba.ngrok.io/Home/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test
            List<ChiTietDatHang> res = (List<ChiTietDatHang>)Session[LeafShop.Session.ConstaintCart.CART];
            foreach (ChiTietDatHang item in res)
            {
                if (item.DonGia != null)
                {
                    tongTien += (item.SoLuong.HasValue ? item.SoLuong.Value : 0) * (item.DonGia.HasValue ? item.DonGia.Value : 0);
                }
            }
            tongTien += 35000;
            string amount = tongTien.ToString();
            string orderid = DateTime.Now.Ticks.ToString();
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);
            KhachHang kh = (KhachHang)Session[LeafShop.Session.ConstaintUser.USER_SESSION];

            dh.MaKhachHang = kh.MaKhachHang;
            dh.NgayKhoiTao = DateTime.Now;
            dh.NgayGiaoHang = null;
            dh.DiaChi = diaChi;
            dh.TongTien = tongTien;
            dh.TrangThai = true;
            dh.MaNhanVien = null;
            dh.GhiChu = "Đặt hàng thành công";
            db.DatHangs.Add(dh);
            db.SaveChanges();
            List<ChiTietDatHang> list = (List<ChiTietDatHang>)Session[LeafShop.Session.ConstaintCart.CART];
            if(list != null)
            {
                foreach (ChiTietDatHang item in list)
                {
                    item.SanPham = null;
                    item.MaDatHang = dh.MaDatHang;
                    db.ChiTietDatHangs.Add(item);
                    db.SaveChanges();
                }
                Session.Remove(LeafShop.Session.ConstaintCart.CART);
            }
            else
                Session.Remove(LeafShop.Session.ConstaintCart.CART);

            return Redirect(jmessage.GetValue("payUrl").ToString());

            //return RedirectToAction("CreateBill", "Bill", new { tongTien = tongTien, ghiChu = ghiChu, diaChi = diaChi });
        }

        //Khi thanh toán xong ở cổng thanh toán Momo, Momo sẽ trả về một số thông tin, trong đó có errorCode để check thông tin thanh toán
        //errorCode = 0 : thanh toán thành công (Request.QueryString["errorCode"])
        //Tham khảo bảng mã lỗi tại: https://developers.momo.vn/#/docs/aio/?id=b%e1%ba%a3ng-m%c3%a3-l%e1%bb%97i
        public ActionResult ConfirmPaymentClient()
        {
            //hiển thị thông báo cho người dùng
            return View();
        }

        [HttpPost]
        public ActionResult SavePayment([Bind(Include = "GhiChu,DiaChi")] DatHang dh)
        {
            string diaChi = dh.DiaChi;
            string ghiChu = dh.GhiChu;
            List<ChiTietDatHang> res = (List<ChiTietDatHang>)Session[LeafShop.Session.ConstaintCart.CART];
            foreach (ChiTietDatHang item in res)
            {
                if (item.DonGia != null)
                {
                    tongTien += (item.SoLuong.HasValue ? item.SoLuong.Value : 0) * (item.DonGia.HasValue ? item.DonGia.Value : 0);
                }
            }
            tongTien += 35000;
            return RedirectToAction("CreateBill", "Bill", new { tongTien = tongTien, ghiChu = ghiChu, diaChi = diaChi });
        }
    }
}