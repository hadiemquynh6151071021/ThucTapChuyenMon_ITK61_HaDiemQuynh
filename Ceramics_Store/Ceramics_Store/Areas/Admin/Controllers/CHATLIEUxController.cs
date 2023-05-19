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
    public class CHATLIEUxController : Controller
    {
        private CERAMICS_STOREEntities db = new CERAMICS_STOREEntities();

        public ActionResult Index_1()
        {
            return View();
        }

        //public ActionResult View()
        //{
        //    return View();
        //}

        // GET: Admin/CHATLIEUx
        public ActionResult Index()
        {
            return View(db.CHATLIEUx.ToList());
        }


        // GET: Admin/CHATLIEUx/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CHATLIEUx/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MACL,TENCL")] CHATLIEU cHATLIEU)
        {
            if (ModelState.IsValid)
            {
                db.CHATLIEUx.Add(cHATLIEU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cHATLIEU);
        }

        // GET: Admin/CHATLIEUx/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHATLIEU cHATLIEU = db.CHATLIEUx.Find(id);
            if (cHATLIEU == null)
            {
                return HttpNotFound();
            }
            return View(cHATLIEU);
        }

        // POST: Admin/CHATLIEUx/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MACL,TENCL")] CHATLIEU cHATLIEU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHATLIEU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cHATLIEU);
        }

        // GET: Admin/CHATLIEUx/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHATLIEU cHATLIEU = db.CHATLIEUx.Find(id);
            if (cHATLIEU == null)
            {
                return HttpNotFound();
            }
            return View(cHATLIEU);
        }

        // POST: Admin/CHATLIEUx/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CHATLIEU cHATLIEU = db.CHATLIEUx.Find(id);
            db.CHATLIEUx.Remove(cHATLIEU);
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
