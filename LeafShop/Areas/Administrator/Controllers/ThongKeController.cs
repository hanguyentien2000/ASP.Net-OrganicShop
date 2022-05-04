
using LeafShop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;

namespace LeafShop.Areas.Administrator.Controllers
{
    public class ThongKeController : Controller
    {
        private LeafShopDb db = new LeafShopDb();

        public ActionResult Index()
        {
            var existData = db.ChiTietDatHangs.Select(d => d);
            var existData2 = db.SanPhams.Select(d => d);

            List<long> doanhThu = new List<long>();
            List<string> tensp = new List<string>();

            var datas = existData.Select(x => x.MaSanPham).Distinct();
            var datas2 = existData2.Select(x => x.TenSanPham).Distinct();

            foreach (var item in datas)
            {
                doanhThu.Add(existData.Count(x => x.MaSanPham == item));
            }
            foreach (var item in datas2)
            {
                doanhThu.Add(existData2.Count(x => x.TenSanPham == item));
            }
            var rep = doanhThu;
            var rep2 = tensp;
            ViewBag.DATAS = datas;
            ViewBag.DOANHTHU = doanhThu.ToList();
            ViewBag.TENSP = tensp.ToList();

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
