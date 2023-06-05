using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ceramics_Store.Models;

namespace Ceramics_Store.Areas.Admin.Controllers
{
    public class THONGTINCUAHANGsController : Controller
    {
        private CERAMICS_STOREEntities db = new CERAMICS_STOREEntities();

        // GET: Admin/THONGTINCUAHANGs
        public ActionResult Index()
        {
            return View(db.THONGTINCUAHANGs.ToList());
        }

        // GET: Admin/THONGTINCUAHANGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THONGTINCUAHANG tHONGTINCUAHANG = db.THONGTINCUAHANGs.Find(id);
            if (tHONGTINCUAHANG == null)
            {
                return HttpNotFound();
            }
            return View(tHONGTINCUAHANG);
        }

        // GET: Admin/THONGTINCUAHANGs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/THONGTINCUAHANGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MATT,TENCH,DIACHI,EMAIL,SDT,FLUGINPAGE,URLMAP")] THONGTINCUAHANG tHONGTINCUAHANG)
        {
            if (ModelState.IsValid)
            {
                db.THONGTINCUAHANGs.Add(tHONGTINCUAHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tHONGTINCUAHANG);
        }

        // GET: Admin/THONGTINCUAHANGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THONGTINCUAHANG tHONGTINCUAHANG = db.THONGTINCUAHANGs.Find(id);
            if (tHONGTINCUAHANG == null)
            {
                return HttpNotFound();
            }
            return View(tHONGTINCUAHANG);
        }

        // POST: Admin/THONGTINCUAHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MATT,TENCH,DIACHI,EMAIL,SDT,FLUGINPAGE,URLMAP")] THONGTINCUAHANG tHONGTINCUAHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHONGTINCUAHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tHONGTINCUAHANG);
        }

        // GET: Admin/THONGTINCUAHANGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THONGTINCUAHANG tHONGTINCUAHANG = db.THONGTINCUAHANGs.Find(id);
            if (tHONGTINCUAHANG == null)
            {
                return HttpNotFound();
            }
            return View(tHONGTINCUAHANG);
        }

        // POST: Admin/THONGTINCUAHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            THONGTINCUAHANG tHONGTINCUAHANG = db.THONGTINCUAHANGs.Find(id);
            db.THONGTINCUAHANGs.Remove(tHONGTINCUAHANG);
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
