
using LeafShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LeafShop.Areas.Administrator.Controllers
{
    public class ThongKeController : Controller
    {
        private LeafShopDb db = new LeafShopDb();

        public ActionResult Index()
        {
            var chiTietDatHangs = db.ChiTietDatHangs.Select(d => d);
            var sanPhams = db.SanPhams.Select(d => d);
            //Khởi tạo
            List<long> doanhThu = new List<long>();
            List<int> soLuongBan = new List<int>();
            List<string> tenSP = new List<string>();

            //Lọc
            var sortTenSP = sanPhams.OrderByDescending(x => x.SoLuongBan).Select(x => x.TenSanPham).Distinct();
            var sortSoLuongBan = sanPhams.Select(x => x.SoLuongBan).Distinct();

            foreach (var item in sortSoLuongBan)
            {
                soLuongBan.Add(sanPhams.Count(x => x.SoLuongBan == item));
            }
            foreach (var item in sortTenSP)
            {
                tenSP.Add(item);
            }

            //ViewBag.DATAS = datas;
            ViewBag.SoLuongBan = soLuongBan.ToList();
            ViewBag.TenSanPham = tenSP.ToList();
            return View();
        }

        //public ActionResult StatisticsByMonth(int? Month, int? Year)
        //{
        //    decimal sumMonth = 0;
        //    if (Month == null && Year == null)
        //    {
        //        ViewBag.MonthYear = DateTime.Now.Month + "/" + DateTime.Now.Year;
        //        //phòng

        //        var listRoomMonth = bookingService.StatisticsRoomByMonth(DateTime.Now.Month, DateTime.Now.Year);
        //        List<string> listRoomNameMonth = new List<string>();
        //        foreach (var item in listRoomMonth)
        //        {
        //            string roomName = item.RoomName;
        //            listRoomNameMonth.Add(roomName);
        //        }
        //        ViewBag.ListRoomNameMonth = listRoomNameMonth;
        //        List<decimal> listMoneyMonth = new List<decimal>();
        //        foreach (var item in listRoomMonth)
        //        {
        //            decimal tien = item.Money;
        //            listMoneyMonth.Add(tien);
        //            sumMonth += tien;
        //        }
        //        ViewBag.ListMoneyMonth = listMoneyMonth;
        //        ViewBag.Sum = sumMonth;

        //        //loại phòng
        //        var listRoomTypeMonth = bookingService.StatisticsRoomTypeByMonth(DateTime.Now.Month, DateTime.Now.Year);
        //        List<string> listRoomTypeNameMonth = new List<string>();
        //        foreach (var item in listRoomTypeMonth)
        //        {
        //            string roomTypeNameMonth = item.RoomTypeName;
        //            listRoomTypeNameMonth.Add(roomTypeNameMonth);
        //        }
        //        ViewBag.ListRoomTypeNameMonth = listRoomTypeNameMonth;
        //        List<decimal> listRoomTypeMoneyMonth = new List<decimal>();
        //        foreach (var item in listRoomTypeMonth)
        //        {
        //            decimal tien = item.Money;
        //            listRoomTypeMoneyMonth.Add(tien);
        //        }
        //        ViewBag.ListRoomTypeMoneyMonth = listRoomTypeMoneyMonth;
        //    }
        //    else
        //    {
        //        ViewBag.MonthYear = Month + "/" + Year;
        //        //phòng
        //        var listRoomMonth = bookingService.StatisticsRoomByMonth((int)Month, (int)Year);
        //        List<string> listRoomNameMonth = new List<string>();
        //        foreach (var item in listRoomMonth)
        //        {
        //            string roomName = item.RoomName;
        //            listRoomNameMonth.Add(roomName);
        //        }
        //        ViewBag.ListRoomNameMonth = listRoomNameMonth;
        //        List<decimal> listMoneyMonth = new List<decimal>();
        //        foreach (var item in listRoomMonth)
        //        {
        //            decimal tien = item.Money;
        //            listMoneyMonth.Add(tien);
        //            sumMonth += tien;
        //        }
        //        ViewBag.ListMoneyMonth = listMoneyMonth;
        //        ViewBag.Sum = sumMonth;

        //        //loại phòng
        //        var listRoomTypeMonth = bookingService.StatisticsRoomTypeByMonth((int)Month, (int)Year);
        //        List<string> listRoomTypeNameMonth = new List<string>();
        //        foreach (var item in listRoomTypeMonth)
        //        {
        //            string roomTypeNameMonth = item.RoomTypeName;
        //            listRoomTypeNameMonth.Add(roomTypeNameMonth);
        //        }
        //        ViewBag.ListRoomTypeNameMonth = listRoomTypeNameMonth;
        //        List<decimal> listRoomTypeMoneyMonth = new List<decimal>();
        //        foreach (var item in listRoomTypeMonth)
        //        {
        //            decimal tien = item.Money;
        //            listRoomTypeMoneyMonth.Add(tien);
        //        }
        //        ViewBag.ListRoomTypeMoneyMonth = listRoomTypeMoneyMonth;
        //    }
        //    return View();
        //}
    }
}
