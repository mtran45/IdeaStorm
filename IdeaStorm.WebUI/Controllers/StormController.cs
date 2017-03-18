using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdeaStorm.Domain.Abstract;
using IdeaStorm.Domain.Entities;
using IdeaStorm.WebUI.Helpers;
using IdeaStorm.WebUI.Models;
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
            Storm storm = db.GetStormByID(id);
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
            Storm storm = db.GetStormByID((int)id);
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
            Storm storm = db.GetStormByID(id);
            db.DeleteStorm(storm);
            TempData["message"] = string.Format($"\"{storm.Title}\" has been deleted");
            return RedirectToAction("Index");
        }

        // GET: Storm/Brainstorm
        public ActionResult Brainstorm(int? id)
        {
            Storm storm = (id == null) ? new Storm(): db.GetStormByID(id);
            if (storm == null)
            {
                return HttpNotFound();

            }
            var model = new BrainstormViewModel
            {
                Id = storm.StormID,
                Title = storm.Title ??
                    "Brainstorm " + DateTime.UtcNow.ToString("d/M/yy"),
                Sparks = storm.Sparks?.ToList()
            };
            return View(model);
        }

        // POST: Storm/Brainstorm
        [HttpPost]
        public ActionResult Brainstorm(BrainstormViewModel model)
        {
            Storm storm = db.GetStormByID(model.Id);

            if (storm != null)
            {
                var sparks = storm.Sparks.ToList();

                for (int i = 0; i < model.Sparks.Count; i++)
                {
                    sparks[i].Title = model.Sparks[i].Title;
                    db.SaveSpark(sparks[i]);
                }
            }
            else
            {
                storm = new Storm
                {
                    User = db.GetUserByID(GetUserId())
                };
                foreach (var spark in model.Sparks)
                {
                    spark.Storm = storm;
                    spark.User = db.GetUserByID(GetUserId());
                    db.SaveSpark(spark);
                }
            }
            storm.Title = model.Title;
            db.SaveStorm(storm);
            TempData["message"] = string.Format($"\"{model.Title}\" added");
            return RedirectToAction("Index");
        }
    }
}
