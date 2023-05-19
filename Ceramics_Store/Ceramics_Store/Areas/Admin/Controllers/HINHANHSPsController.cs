using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ceramics_Store.Models;

namespace Ceramics_Store.Areas.Admin.Controllers
{
    public class HINHANHSPsController : Controller
    {
        private CERAMICS_STOREEntities db = new CERAMICS_STOREEntities();

        // GET: Admin/HINHANHSPs
        public ActionResult Index()
        {
            var hINHANHSPs = db.HINHANHSPs.Include(h => h.SANPHAM);
            return View(hINHANHSPs.ToList());
        }

        // GET: Admin/HINHANHSPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HINHANHSP hINHANHSP = db.HINHANHSPs.Find(id);
            if (hINHANHSP == null)
            {
                return HttpNotFound();
            }
            return View(hINHANHSP);
        }

        // GET: Admin/HINHANHSPs/Create
        public ActionResult Create()
        {
            //ViewBag.MACL = new SelectList(db.CHATLIEUx, "MACL", "TENCL");
            //ViewBag.MALSP = new SelectList(db.LOAISANPHAMs, "MALSP", "TENSP");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAANH,MASP,TENANH")] HINHANHSP hINHANHSP, IEnumerable<HttpPostedFileBase> TENANH)
        {
            var sp = db.SANPHAMs.OrderByDescending(m => m.MASP).First();
            if (ModelState.IsValid)
            {
                foreach (var file in TENANH)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/productImages"), fileName);
                        file.SaveAs(path);

                        hINHANHSP.MASP = sp.MASP;
                        hINHANHSP.TENANH = fileName;

                    }
                    db.HINHANHSPs.Add(hINHANHSP);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(hINHANHSP);
        }

        // GET: Admin/HINHANHSPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HINHANHSP hINHANHSP = db.HINHANHSPs.Find(id);
            if (hINHANHSP == null)
            {
                return HttpNotFound();
            }
            ViewBag.MASP = new SelectList(db.SANPHAMs, "MASP", "TENSP", hINHANHSP.MASP);
            return View(hINHANHSP);
        }

        // POST: Admin/HINHANHSPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAANH,MASP,TENANH")] HINHANHSP hINHANHSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hINHANHSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hINHANHSP);
        }

        // GET: Admin/HINHANHSPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HINHANHSP hINHANHSP = db.HINHANHSPs.Find(id);
            if (hINHANHSP == null)
            {
                return HttpNotFound();
            }
            return View(hINHANHSP);
        }

        // POST: Admin/HINHANHSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HINHANHSP hINHANHSP = db.HINHANHSPs.Find(id);
            db.HINHANHSPs.Remove(hINHANHSP);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
