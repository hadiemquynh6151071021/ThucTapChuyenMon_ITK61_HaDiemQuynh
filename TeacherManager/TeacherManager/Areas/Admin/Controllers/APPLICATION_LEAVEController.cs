﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeacherManager.Models;

namespace TeacherManager.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class APPLICATION_LEAVEController : Controller
    {
        private TeacherWorkEntities db = new TeacherWorkEntities();

        // GET: Admin/APPLICATION_LEAVE
        public ActionResult Index()
        {
            var aPPLICATION_LEAVE = db.APPLICATION_LEAVE.Include(a => a.TEACHER).Where(m => m.STATUS=="Đã duyệt");
            return View(aPPLICATION_LEAVE.ToList());
        }

        // GET: Admin/APPLICATION_LEAVE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APPLICATION_LEAVE aPPLICATION_LEAVE = db.APPLICATION_LEAVE.Find(id);
            if (aPPLICATION_LEAVE == null)
            {
                return HttpNotFound();
            }
            return View(aPPLICATION_LEAVE);
        }

        // GET: Admin/APPLICATION_LEAVE/Create
        public ActionResult Create()
        {
            ViewBag.ID_TEACHER = new SelectList(db.TEACHERs, "ID", "NAME");
            return View();
        }

        // POST: Admin/APPLICATION_LEAVE/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_TEACHER,DATESTART,REASON,STATUS,DATEEND,TYPELEAVE")] APPLICATION_LEAVE aPPLICATION_LEAVE)
        {
            if (ModelState.IsValid)
            {
                db.APPLICATION_LEAVE.Add(aPPLICATION_LEAVE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_TEACHER = new SelectList(db.TEACHERs, "ID", "NAME", aPPLICATION_LEAVE.ID_TEACHER);
            return View(aPPLICATION_LEAVE);
        }

        // GET: Admin/APPLICATION_LEAVE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APPLICATION_LEAVE aPPLICATION_LEAVE = db.APPLICATION_LEAVE.Find(id);
            if (aPPLICATION_LEAVE == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_TEACHER = new SelectList(db.TEACHERs, "ID", "NAME", aPPLICATION_LEAVE.ID_TEACHER);
            return View(aPPLICATION_LEAVE);
        }

        // POST: Admin/APPLICATION_LEAVE/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_TEACHER,DATESTART,REASON,STATUS,DATEEND,TYPELEAVE")] APPLICATION_LEAVE aPPLICATION_LEAVE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aPPLICATION_LEAVE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_TEACHER = new SelectList(db.TEACHERs, "ID", "NAME", aPPLICATION_LEAVE.ID_TEACHER);
            return View(aPPLICATION_LEAVE);
        }

        // GET: Admin/APPLICATION_LEAVE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APPLICATION_LEAVE aPPLICATION_LEAVE = db.APPLICATION_LEAVE.Find(id);
            if (aPPLICATION_LEAVE == null)
            {
                return HttpNotFound();
            }
            return View(aPPLICATION_LEAVE);
        }

        // POST: Admin/APPLICATION_LEAVE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            APPLICATION_LEAVE aPPLICATION_LEAVE = db.APPLICATION_LEAVE.Find(id);
            db.APPLICATION_LEAVE.Remove(aPPLICATION_LEAVE);
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
