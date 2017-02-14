using System;
using System.Collections.Generic;
using System.Linq;
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

        public IdeaController(IIdeaRepository ideaRepository)
        {
            this.repository = ideaRepository;
        }

        public ViewResult List()
        {
            return View(repository.Ideas);
        }

        public ViewResult Edit(int id)
        {
            Idea idea = repository.Ideas.FirstOrDefault(i => i.IdeaID == id);
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