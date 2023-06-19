﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeacherManager.Models;

namespace TeacherManager.Controllers
{
    public class MAKEUP_LESSONController : Controller
    {
        private TeacherWorkEntities db = new TeacherWorkEntities();

        // GET: MAKEUP_LESSON
        public ActionResult Index()
        {
            var mAKEUP_LESSON = db.MAKEUP_LESSON.Include(m => m.CLASSROOM).Include(m => m.SUBJECT);
            return View(mAKEUP_LESSON.ToList());
        }

        // GET: MAKEUP_LESSON/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MAKEUP_LESSON mAKEUP_LESSON = db.MAKEUP_LESSON.Find(id);
            if (mAKEUP_LESSON == null)
            {
                return HttpNotFound();
            }
            return View(mAKEUP_LESSON);
        }

        // GET: MAKEUP_LESSON/Create
        public ActionResult Create()
        {
            ViewBag.ID_CLASS = new SelectList(db.CLASSROOMs, "ID", "NAME");
            ViewBag.ID_SUBJECT = new SelectList(db.SUBJECTs, "ID", "NAME");
            return View();
        }

        // POST: MAKEUP_LESSON/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_CLASS,ID_SUBJECT,DATE,TIMESTART,TIMEEND,SITUATION")] MAKEUP_LESSON mAKEUP_LESSON)
        {
            if (ModelState.IsValid)
            {
                db.MAKEUP_LESSON.Add(mAKEUP_LESSON);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CLASS = new SelectList(db.CLASSROOMs, "ID", "NAME", mAKEUP_LESSON.ID_CLASS);
            ViewBag.ID_SUBJECT = new SelectList(db.SUBJECTs, "ID", "NAME", mAKEUP_LESSON.ID_SUBJECT);
            return View(mAKEUP_LESSON);
        }

        // GET: MAKEUP_LESSON/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MAKEUP_LESSON mAKEUP_LESSON = db.MAKEUP_LESSON.Find(id);
            if (mAKEUP_LESSON == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CLASS = new SelectList(db.CLASSROOMs, "ID", "NAME", mAKEUP_LESSON.ID_CLASS);
            ViewBag.ID_SUBJECT = new SelectList(db.SUBJECTs, "ID", "NAME", mAKEUP_LESSON.ID_SUBJECT);
            return View(mAKEUP_LESSON);
        }

        // POST: MAKEUP_LESSON/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_CLASS,ID_SUBJECT,DATE,TIMESTART,TIMEEND,SITUATION")] MAKEUP_LESSON mAKEUP_LESSON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mAKEUP_LESSON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CLASS = new SelectList(db.CLASSROOMs, "ID", "NAME", mAKEUP_LESSON.ID_CLASS);
            ViewBag.ID_SUBJECT = new SelectList(db.SUBJECTs, "ID", "NAME", mAKEUP_LESSON.ID_SUBJECT);
            return View(mAKEUP_LESSON);
        }

        // GET: MAKEUP_LESSON/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MAKEUP_LESSON mAKEUP_LESSON = db.MAKEUP_LESSON.Find(id);
            if (mAKEUP_LESSON == null)
            {
                return HttpNotFound();
            }
            return View(mAKEUP_LESSON);
        }

        // POST: MAKEUP_LESSON/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MAKEUP_LESSON mAKEUP_LESSON = db.MAKEUP_LESSON.Find(id);
            db.MAKEUP_LESSON.Remove(mAKEUP_LESSON);
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