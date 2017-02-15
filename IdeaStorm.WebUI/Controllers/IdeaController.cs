using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Castle.Core.Internal;
using IdeaStorm.Domain.Abstract;
using IdeaStorm.Domain.Entities;

namespace IdeaStorm.WebUI.Controllers
{
    public class IdeaController : Controller
    {
        private IIdeaRepository repository;

        public Idea FindIdea(int id)
        {
            return repository.Ideas.FirstOrDefault(i => i.IdeaID == id);
        }

        public IdeaController(IIdeaRepository ideaRepository)
        {
            this.repository = ideaRepository;
        }

        public ViewResult List()
        {
            return View(repository.Ideas);
        }

        public ActionResult Edit(int id)
        {
            //Idea idea = repository.FindIdea(id);
            Idea idea = FindIdea(id);
            if (idea == null)
            {
                return HttpNotFound();
            }
            return View(idea);
        }

        [HttpPost]
        public ActionResult Edit(Idea idea)
        {
            if (ModelState.IsValid)
            {
                repository.SaveIdea(idea);
                TempData["message"] = string.Format($"\"{idea.Name}\" has been saved");
                return RedirectToAction("List");
            }
            else
            {
                // there is something wrong with the data values
                return View(idea);
            }
        }

        public ViewResult Create()
        {
            return View(new Idea());
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,Description,Category")] Idea idea)
        {
            if (ModelState.IsValid)
            {
                repository.SaveIdea(idea);
                TempData["message"] = string.Format($"\"{idea.Name}\" has been added");
                return RedirectToAction("List");
            }
            return View(idea);
        }

        public ActionResult Delete(int id)
        {
            Idea idea = FindIdea(id);
            if (idea == null)
            {
                return HttpNotFound();
            }
            return View(idea);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteIdea(int id)
        {
            Idea idea = FindIdea(id);
            repository.DeleteIdea(idea);
            TempData["message"] = string.Format($"\"{idea.Name}\" has been deleted");
            return RedirectToAction("List");
        }

        public ViewResult Brainstorm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Brainstorm(IList<string> ideas)
        {
            int saved = 0;
            foreach (var i in ideas)
            {
                if (i.Trim().IsEmpty()) continue;
                repository.SaveIdea(new Idea(i));
                saved++;
            }
            TempData["message"] = string.Format($"{saved} ideas added");
            return RedirectToAction("List");
        } 
    }
}