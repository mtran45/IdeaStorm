using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.WebPages;
using IdeaStorm.Domain.Abstract;
using IdeaStorm.Domain.Concrete;
using IdeaStorm.Domain.Entities;

namespace IdeaStorm.WebUI.Controllers
{
    public class StormController : Controller
    {
        private IStormRepository stormRepo;
        private IIdeaRepository ideaRepo;
        //private EFDbContext db = new EFDbContext();

        public StormController(IStormRepository stormRepo, IIdeaRepository ideaRepo)
        {
            this.stormRepo = stormRepo;
            this.ideaRepo = ideaRepo;
        }

        public Storm FindStorm(int id)
        {
            return stormRepo.Storms.FirstOrDefault(s => s.StormID == id);
        }

        // GET: Storm
        public ActionResult Index()
        {
            // var storms = db.Storms.Include(s => s.User);
            return View(stormRepo.Storms);
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

        // GET: Storm/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Storm/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StormID,UserID,Title,CreatedTime,UpdatedTime")] Storm storm)
        {
            if (ModelState.IsValid)
            {
                stormRepo.SaveStorm(storm);
                TempData["message"] = string.Format($"\"{storm.Title}\" has been updated");
                return RedirectToAction("Index");
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
        public ActionResult DeleteConfirmed(int id)
        {
            Storm storm = FindStorm(id);
            stormRepo.DeleteStorm(storm);
//            db.Storms.Remove(storm);
//            db.SaveChanges();
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
        public ActionResult Brainstorm(IList<string> ideaTitles)
        {
            Storm storm = new Storm();
            storm.Title = "Brainstorm " + DateTime.Today.ToShortDateString();
            foreach (var title in ideaTitles)
            {
                if (title.Trim().IsEmpty()) continue;
                Idea idea = new Idea(title);
                idea.Storm = storm;
                ideaRepo.SaveIdea(idea);
            }
            if (storm.Ideas.Count > 0) stormRepo.SaveStorm(storm);
            TempData["message"] = string.Format($"{storm.Ideas.Count} ideas added");
            return RedirectToAction("Index");
        }
    }
}
