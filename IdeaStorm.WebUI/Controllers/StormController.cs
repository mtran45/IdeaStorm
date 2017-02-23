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
            stormRepo.DeleteStorm(storm);
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
        public ActionResult Brainstorm(string stormTitle, IList<string> ideaTitles)
        {
            var filteredTitles = ideaTitles.Where(it => !string.IsNullOrWhiteSpace(it)).ToList();
            Storm storm = new Storm();
            storm.Title = stormTitle;
            foreach (var title in filteredTitles)
            {
                Idea idea = new Idea(title);
                idea.Storm = storm;
                ideaRepo.SaveIdea(idea);
            }
            if (filteredTitles.Any()) stormRepo.SaveStorm(storm);
            TempData["message"] = string.Format($"{filteredTitles.Count} ideas added");
            return RedirectToAction("Index");
        }
    }
}
