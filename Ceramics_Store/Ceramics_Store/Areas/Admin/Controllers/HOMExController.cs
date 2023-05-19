using Ceramics_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ceramics_Store.Areas.Admin.Controllers
{
    public class HOMExController : Controller
    {
        CERAMICS_STOREEntities db = new CERAMICS_STOREEntities();
        // GET: Admin/HOMEX
        public ActionResult Index()
        {
            var top10Sp = db.SANPHAMs.OrderByDescending(m=>m.SOLUONGDABAN).Take(10);
            ViewBag.PageView = HttpContext.Application["PageView"].ToString();
            ViewBag.Online = HttpContext.Application["Online"].ToString();
            return View(top10Sp.ToList());
        }
    }
}