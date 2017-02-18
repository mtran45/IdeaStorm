using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdeaStorm.Domain.Concrete;
using IdeaStorm.Domain.Entities;

namespace IdeaStorm.WebUI.Controllers
{
    public class StormController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: Storm
        public ActionResult Index()
        {
            var storms = db.Storms.Include(s => s.User);
            return View(storms.ToList());
        }

        // GET: Storm/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Storm storm = db.Storms.Find(id);
            if (storm == null)
            {
                return HttpNotFound();
            }
            return View(storm);
        }

        // GET: Storm/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username");
            return View();
        }

        // POST: Storm/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StormID,UserID,Title,CreatedTime,UpdatedTime")] Storm storm)
        {
            if (ModelState.IsValid)
            {
                db.Storms.Add(storm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", storm.UserID);
            return View(storm);
        }

        // GET: Storm/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Storm storm = db.Storms.Find(id);
            if (storm == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", storm.UserID);
            return View(storm);
        }

        // POST: Storm/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StormID,UserID,Title,CreatedTime,UpdatedTime")] Storm storm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", storm.UserID);
            return View(storm);
        }

        // GET: Storm/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Storm storm = db.Storms.Find(id);
            if (storm == null)
            {
                return HttpNotFound();
            }
            return View(storm);
        }

        // POST: Storm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Storm storm = db.Storms.Find(id);
            db.Storms.Remove(storm);
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
