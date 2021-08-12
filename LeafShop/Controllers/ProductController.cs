﻿using LeafShop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeafShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        LeafShopDb db = new LeafShopDb();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products(string SearchString, string currentFilter, int? page)
        {
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            var danhmucs = db.DanhMucs.Select(d => d);
            if (!String.IsNullOrEmpty(SearchString))
            {
                danhmucs = danhmucs.Where(p => p.TenDanhMuc.Contains(SearchString));
            }
            int pageSize = 10;

            int pageNumber = (page ?? 1);
            return View(danhmucs.ToPagedList(pageNumber, pageSize));
        }

    }
}