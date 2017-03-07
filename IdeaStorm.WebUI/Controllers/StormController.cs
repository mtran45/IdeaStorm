﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using IdeaStorm.Domain.Abstract;
using IdeaStorm.Domain.Entities;
using IdeaStorm.WebUI.Helpers;

namespace IdeaStorm.WebUI.Controllers
{
    public class StormController : Controller
    {
        private IStormRepository stormRepo;
        private ISparkRepository sparkRepo;
        private IIdeaRepository ideaRepo;
        private IUserRepository userRepo;

        public StormController(IStormRepository stormRepo, ISparkRepository sparkRepo, IIdeaRepository ideaRepo, IUserRepository userRepo)
        {
            this.stormRepo = stormRepo;
            this.sparkRepo = sparkRepo;
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
        public ActionResult Brainstorm(string stormTitle, IList<string> sparks)
        {
            var filteredTitles = sparks.Where(it => !string.IsNullOrWhiteSpace(it)).ToList();
            Storm storm = new Storm();
            storm.Title = stormTitle;
            foreach (var title in filteredTitles)
            {
                Spark spark = new Spark(title);
                spark.Storm = storm;
                sparkRepo.SaveSpark(spark);
            }
            if (filteredTitles.Any()) stormRepo.SaveStorm(storm);
            TempData["message"] = string.Format($"{filteredTitles.Count} sparks added");
            return RedirectToAction("Index");
        }
    }
}
