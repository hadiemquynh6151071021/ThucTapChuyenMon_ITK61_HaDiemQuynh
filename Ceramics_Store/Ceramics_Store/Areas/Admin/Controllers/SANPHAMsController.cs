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
    public class SANPHAMsController : Controller
    {
        private CERAMICS_STOREEntities db = new CERAMICS_STOREEntities();

        // GET: Admin/SANPHAMs
        public ActionResult Index(int malsp=1)
        {
            IList<HINHANHSP> Listimages = new List<HINHANHSP>();
            var sHINHANHSPs = db.HINHANHSPs.ToList();
            Listimages = sHINHANHSPs;
            ViewBag.Listimages = Listimages;
            ViewBag.MALSP = new SelectList(db.LOAISANPHAMs, "MALSP", "TENSP");
            var sANPHAMs = db.SANPHAMs.Include(s => s.CHATLIEU).Include(s => s.LOAISANPHAM).Where(s => s.MALSP == malsp).OrderBy(s => s.TENSP).ToList();
            if (malsp == 1)
            {
                return View(sANPHAMs);
            }
            else
            {
                sANPHAMs = db.SANPHAMs.Include(s => s.CHATLIEU).Include(s => s.LOAISANPHAM).Where(s => s.MALSP == malsp).OrderBy(s => s.TENSP).ToList();
                return View(sANPHAMs);
            }
        }

        // GET: Admin/SANPHAMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            List<HINHANHSP> ls = db.HINHANHSPs.Where(m => m.MASP == sANPHAM.MASP).ToList();
            ViewBag.listImages = ls;
            ViewBag.imageMain = db.HINHANHSPs.Where(m => m.MASP == sANPHAM.MASP).First();
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Create
        public ActionResult Create()
        {
            ViewBag.MACL = new SelectList(db.CHATLIEUx, "MACL", "TENCL");
            ViewBag.MALSP = new SelectList(db.LOAISANPHAMs, "MALSP", "TENSP");
            return View();
        }

        // POST: Admin/SANPHAMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MASP,MACL,MALSP,TENSP,MOTA,SOLUONGTONKHO,SOLUONGDABAN,GIA")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.SANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("Create","HINHANHSPs");
            }

            ViewBag.MACL = new SelectList(db.CHATLIEUx, "MACL", "TENCL", sANPHAM.MACL);
            ViewBag.MALSP = new SelectList(db.LOAISANPHAMs, "MALSP", "TENSP", sANPHAM.MALSP);
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MACL = new SelectList(db.CHATLIEUx, "MACL", "TENCL", sANPHAM.MACL);
            ViewBag.MALSP = new SelectList(db.LOAISANPHAMs, "MALSP", "TENSP", sANPHAM.MALSP);
            return View(sANPHAM);
        }

        // POST: Admin/SANPHAMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MASP,MACL,MALSP,TENSP,MOTA,SOLUONGTONKHO,SOLUONGDABAN,GIA")] SANPHAM sANPHAM, [Bind(Include = "MAANH,MASP,TENANH")] HINHANHSP hINHANHSP, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sANPHAM).State = EntityState.Modified;
                db.SaveChanges();

                List<HINHANHSP> images = db.HINHANHSPs.Where(m => m.MASP == sANPHAM.MASP).ToList();
                if(files.Count() > 0)
                {
                    foreach(var item in images)
                    {
                        db.HINHANHSPs.Remove(item);
                        db.SaveChanges();
                    }
                    foreach (var file in files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/productImages"), fileName);
                            file.SaveAs(path);

                            hINHANHSP.MASP = sANPHAM.MASP;
                            hINHANHSP.TENANH = fileName;

                        }
                        db.HINHANHSPs.Add(hINHANHSP);
                        db.SaveChanges();
                    }
                }
                
                
                return RedirectToAction("Index");
            }
            ViewBag.MACL = new SelectList(db.CHATLIEUx, "MACL", "TENCL", sANPHAM.MACL);
            ViewBag.MALSP = new SelectList(db.LOAISANPHAMs, "MALSP", "TENSP", sANPHAM.MALSP);
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // POST: Admin/SANPHAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            List<HINHANHSP> images = db.HINHANHSPs.Where(m => m.MASP == sANPHAM.MASP).ToList();
            foreach(var item in images)
            {
                db.HINHANHSPs.Remove(item);
                db.SaveChanges();
            }
            db.SANPHAMs.Remove(sANPHAM);
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
