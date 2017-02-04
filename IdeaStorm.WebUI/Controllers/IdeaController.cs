using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        public ViewResult Edit(int ideaId)
        {
            Idea idea = repository.Ideas.FirstOrDefault(i => i.IdeaID == ideaId);
            return View(idea);
        }

        [HttpPost]
        public ActionResult Edit(Idea idea)
        {
            if (ModelState.IsValid)
            {
                repository.SaveIdea(idea);
                TempData["message"] = string.Format($"{idea.Name} has been saved");
                return RedirectToAction("List");
            }
            else
            {
                // there is something wrong with the data values
                return View(idea);
            }
        }
    }
}