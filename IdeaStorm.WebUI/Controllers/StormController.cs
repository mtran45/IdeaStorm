﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdeaStorm.Domain.Abstract;
using IdeaStorm.Domain.Entities;
using IdeaStorm.WebUI.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace IdeaStorm.WebUI.Controllers
{
    [Authorize]
    public class StormController : Controller
    {
        private IDbContext db;

        public Func<string> GetUserId; // For testing

        public StormController(IDbContext context)
        {
            db = context;

            GetUserId = () => User.Identity.GetUserId();
        }

        public Storm FindStorm(int id)
        {
            return db.Storms.FirstOrDefault(s => s.StormID == id);
        }

        // GET: Storm
        public ActionResult Index()
        {
            string userId = GetUserId();
            List<Storm> storms = db.Storms.Where(s => s.User.Id == userId).ToList();
            return View(storms);
        }

        // GET: Storm/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Storm storm = FindStorm((int)id);
            if (storm == null)
            {
                return HttpNotFound();
            }
            return View(storm);
        }

        // GET: Storm/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Storm storm = FindStorm((int)id);
            if (storm == null)
            {
                return HttpNotFound();
            }
            return View(storm);
        }

        // POST: Storm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStorm(int id)
        {
            Storm storm = FindStorm(id);
            db.DeleteStorm(storm);
            TempData["message"] = string.Format($"\"{storm.Title}\" has been deleted");
            return RedirectToAction("Index");
        }

        // GET: Storm/Brainstorm
        public ViewResult Brainstorm()
        {
            return View();
        }

        // POST: Storm/Brainstorm
        [HttpPost]
        public ActionResult Brainstorm(string stormTitle, IList<string> sparks)
        {
            var filteredTitles = sparks.Where(it => !string.IsNullOrWhiteSpace(it)).ToList();
            Storm storm = new Storm
            {
                User = db.GetUserByID(GetUserId()),
                Title = stormTitle

            };
            foreach (var title in filteredTitles)
            {
                Spark spark = new Spark(title);
                spark.Storm = storm;
                db.SaveSpark(spark);
            }
            if (filteredTitles.Any()) db.SaveStorm(storm);
            TempData["message"] = string.Format($"{filteredTitles.Count} sparks added");
            return RedirectToAction("Index");
        }
    }
}
