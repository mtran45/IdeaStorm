using System;
using System.Linq;
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
    public class IdeaController : Controller
    {
        private IDbContext db;

        public Func<string> GetUserId; // For testing

        public IdeaController(IDbContext context)
        {
            db = context;
            GetUserId = () => User.Identity.GetUserId();
        }

        // GET: /
        [AllowAnonymous]
        public ViewResult List()
        {
            return View(db.Ideas.OrderByDescending(i => i.CreatedTime));
        }

        public ActionResult Details(int? id)
        {
            Idea idea = db.GetIdeaByID(id);
            if (idea == null)
            {
                return HttpNotFound();
            }
            return View(idea);
        }

        // GET: Idea/Edit/5
        public ActionResult Edit(int id)
        {
            Idea idea = db.GetIdeaByID(id);
            if (idea == null)
            {
                return HttpNotFound();
            }

            if (idea.User.Id != GetUserId())
            {
                TempData["errorMsg"] = $"You are not authorized to edit \"{idea.Title}\".";
                return RedirectToAction("List");
            }

            return View(idea);
        }

        // POST: Idea/Edit/5
        [HttpPost]
        public ActionResult Edit(Idea idea)
        {
            if (ModelState.IsValid)
            {
                db.SaveIdea(idea);
                TempData["message"] = $"\"{idea.Title}\" has been updated";
                return RedirectToAction("List");
            }
            else
            {
                // there is something wrong with the data values
                return View(idea);
            }
        }

        // GET: Idea/Create
        public ViewResult Create()
        {
            return View();
        }

        public ActionResult PromoteSpark(int id)
        {
            Spark spark = db.GetSparkByID(id);
            if (spark == null)
            {
                return HttpNotFound();
            }
            var vm = new CreateIdeaViewModel
            {
                SparkID = id,
                Title = spark.Title
            };
            return View("Create", vm);
        }

        // POST: Idea/Create
        [HttpPost]
        public ActionResult Create(CreateIdeaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var idea = new Idea
                {
                    Title = model.Title,
                    Description = model.Description,
                    Category = model.Category,
                    User = db.GetUserByID(GetUserId()),
                    Spark = db.GetSparkByID(model.SparkID)
            };
                db.SaveIdea(idea);
                TempData["message"] = string.Format($"\"{idea.Title}\" has been added");
                return RedirectToAction("List");
            }

            return View(model);
        }

        // GET: Idea/Delete/5
        public ActionResult Delete(int id)
        {
            Idea idea = db.GetIdeaByID(id);
            if (idea == null)
            {
                return HttpNotFound();
            }

            if (idea.User.Id != GetUserId())
            {
                TempData["errorMsg"] = $"You are not authorized to delete \"{idea.Title}\".";
                return RedirectToAction("List");
            }

            return View(idea);
        }

        // POST: Idea/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteIdea(int id)
        {
            Idea idea = db.GetIdeaByID(id);
            db.DeleteIdea(idea);
            TempData["message"] = string.Format($"\"{idea.Title}\" has been deleted");
            return RedirectToAction("List");
        }

    }
}