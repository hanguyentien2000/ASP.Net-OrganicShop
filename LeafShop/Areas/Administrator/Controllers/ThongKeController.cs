
using LeafShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LeafShop.Areas.Administrator.Controllers
{
    public class ThongKeController : Controller
    {
        private LeafShopDb db = new LeafShopDb();

        public ActionResult Index(int? Month, int? Year)
        {
            decimal sumMonth = 0;
            if (Month == null && Year == null)
            {
                ViewBag.MonthYear = DateTime.Now.Month + "/" + DateTime.Now.Year;

                var listSpMonth = this.IndexByMonth(DateTime.Now.Month, DateTime.Now.Year);
                List<string> listTenSP = new List<string>();
                foreach (var item in listSpMonth)
                {
                    string tenSp = item.TenSanPham;
                    listTenSP.Add(tenSp);
                }
                ViewBag.ListSanPhamMonth = listTenSP;
                List<decimal> listMoneyMonth = new List<decimal>();
                foreach (var item in listSpMonth)
                {
                    decimal tien = item.SoLuong * item.DonGia;
                    listMoneyMonth.Add(tien);
                    sumMonth += tien;
                }
                ViewBag.ListMoneyMonth = listMoneyMonth;
                ViewBag.Sum = sumMonth;
            }
            else if (Year != null && Month == null)
            {
                ViewBag.MonthYear = Year;
                var listSpMonth = this.IndexByYear(DateTime.Now.Month, (int)Year);
                List<string> listTenSP = new List<string>();
                foreach (var item in listSpMonth)
                {
                    string tenSp = item.TenSanPham;
                    listTenSP.Add(tenSp);
                }
                ViewBag.ListSanPhamMonth = listTenSP;
                List<decimal> listMoneyMonth = new List<decimal>();
                foreach (var item in listSpMonth)
                {
                    decimal tien = item.SoLuong * item.DonGia;
                    listMoneyMonth.Add(tien);
                    sumMonth += tien;
                }
                
                ViewBag.ListMoneyMonth = listMoneyMonth;
                ViewBag.Sum = sumMonth;
            }

            else if (Year == null && Month != null)
            {
                ViewBag.MonthYear = Month + "/" + DateTime.Now.Year;

                var listSpMonth = this.IndexByMonth((int)Month, DateTime.Now.Year);
                List<string> listTenSP = new List<string>();
                foreach (var item in listSpMonth)
                {
                    string tenSp = item.TenSanPham;
                    listTenSP.Add(tenSp);
                }
                ViewBag.ListSanPhamMonth = listTenSP;
                List<decimal> listMoneyMonth = new List<decimal>();
                foreach (var item in listSpMonth)
                {
                    decimal tien = item.SoLuong * item.DonGia;
                    listMoneyMonth.Add(tien);
                    sumMonth += tien;
                }

                ViewBag.ListMoneyMonth = listMoneyMonth;
                ViewBag.Sum = sumMonth;
            }

            else if (Month != null && Year != null)
            {
                ViewBag.MonthYear = Month + "/" + Year;
                var listSpMonth = this.IndexByMonth((int)Month, (int)Year);
                List<string> listTenSP = new List<string>();
                foreach (var item in listSpMonth)
                {
                    string tenSp = item.TenSanPham;
                    listTenSP.Add(tenSp);
                }
                ViewBag.ListSanPhamMonth = listTenSP;
                List<decimal> listMoneyMonth = new List<decimal>();
                foreach (var item in listSpMonth)
                {
                    decimal tien = item.SoLuong * item.DonGia;
                    listMoneyMonth.Add(tien);
                    sumMonth += tien;
                }
                ViewBag.ListMoneyMonth = listMoneyMonth;
                ViewBag.Sum = sumMonth;
            }
            return View();
        }

        public List<SanPham> IndexByMonth(int Month, int Year)
        {
            DateTime startDay = new DateTime(Year, Month, 1);
            DateTime endDay = startDay.AddMonths(1).AddDays(-1);
            ViewBag.MonthYear = Month + "/" + Year;
            var result = new List<SanPham>();
            var response = db.SanPhams.Select(d => d).ToList();
            foreach (var item in response)
            {
                var list = new SanPham
                {
                    TenSanPham = item.TenSanPham,
                    SoLuong = item.SoLuong,
                    SoLuongBan = item.SoLuongBan,
                    DonGia = (int)this.StatisticsMonth(item.MaSanPham, Month, Year)
                };
                result.Add(list);
            }
            return result;
        }

        public List<SanPham> IndexByYear(int Month, int Year)
        {
            DateTime startDay = new DateTime(Year, Month, 1);
            DateTime endDay = startDay.AddMonths(1).AddDays(-1);
            ViewBag.MonthYear = Month + "/" + Year;
            var result = new List<SanPham>();
            var response = db.SanPhams.Select(d => d).ToList();
            foreach (var item in response)
            {
                var list = new SanPham
                {
                    TenSanPham = item.TenSanPham,
                    SoLuong = item.SoLuong,
                    SoLuongBan = item.SoLuongBan,
                    DonGia = (int)this.StatisticsByYear(item.MaSanPham, Year)
                };
                result.Add(list);
            }
            return result;
        }

        public decimal StatisticsMonth(int Id, int Month, int Year)
        {
            DateTime startDay = new DateTime(Year, Month, 1);
            DateTime endDay = startDay.AddMonths(1).AddDays(-1);
            var list = from c in db.DatHangs
                       join p in db.ChiTietDatHangs on c.MaDatHang equals p.MaDatHang
                       join d in db.SanPhams on p.MaSanPham equals d.MaSanPham
                       where d.MaSanPham == Id
                       && c.NgayKhoiTao >= startDay
                       && c.NgayKhoiTao <= endDay
                       select d;
            var a = list.OrderByDescending(x => x.SoLuongBan).Take(1).ToList();
            var b = list.OrderByDescending(x => x.SoLuong).Take(1).ToList();
            foreach (var item in a)
            {
                ViewBag.SanPhamBanChay = item.TenSanPham;
            }
            foreach (var item in b)
            {
                ViewBag.SanPhamTonKho = item.TenSanPham;
            }
            decimal count = 0;
            if (list.Count() == 0)
                count = 0;
            else 
                count = (decimal)list.Sum(s => s.SoLuong * s.DonGia);
            return count;
        }

        public decimal StatisticsByYear(int Id, int Year)
        {
            DateTime startDay = new DateTime(Year, 1, 1);
            DateTime endDay = new DateTime(Year, 12, 31);
            var list = from c in db.DatHangs
                       join p in db.ChiTietDatHangs on c.MaDatHang equals p.MaDatHang
                       join d in db.SanPhams on p.MaSanPham equals d.MaSanPham
                       where d.MaSanPham == Id
                       && c.NgayKhoiTao >= startDay
                       && c.NgayKhoiTao <= endDay
                       select d;
            var a = list.OrderByDescending(x => x.SoLuongBan).Take(1).ToList();
            var b = list.OrderByDescending(x => x.SoLuong).Take(1).ToList();
            foreach (var item in a)
            {
                ViewBag.SanPhamBanChay = item.TenSanPham;
            }
            foreach (var item in b)
            {
                ViewBag.SanPhamTonKho = item.TenSanPham;
            }
            decimal count = 0;
            if (list.Count() == 0)
                count = 0;
            else
                count = (decimal)list.Sum(s => s.SoLuong * s.DonGia);
            return count;
        }
    }
}
