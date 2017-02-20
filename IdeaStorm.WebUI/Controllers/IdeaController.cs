using System.Linq;
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

        public Idea FindIdea(int id)
        {
            return repository.Ideas.FirstOrDefault(i => i.IdeaID == id);
        }

        // GET: /
        public ViewResult List()
        {
            return View(repository.Ideas);
        }

        // GET: Idea/Edit/5
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

        // POST: Idea/Edit/5
        [HttpPost]
        public ActionResult Edit(Idea idea)
        {
            if (ModelState.IsValid)
            {
                repository.SaveIdea(idea);
                TempData["message"] = string.Format($"\"{idea.Title}\" has been updated");
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
            return View(new Idea());
        }

        // POST: Idea/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Title,Description,Category")] Idea idea)
        {
            if (ModelState.IsValid)
            {
                repository.SaveIdea(idea);
                TempData["message"] = string.Format($"\"{idea.Title}\" has been added");
                return RedirectToAction("List");
            }
            return View(idea);
        }

        // GET: Idea/Delete/5
        public ActionResult Delete(int id)
        {
            Idea idea = FindIdea(id);
            if (idea == null)
            {
                return HttpNotFound();
            }
            return View(idea);
        }

        // POST: Idea/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteIdea(int id)
        {
            Idea idea = FindIdea(id);
            repository.DeleteIdea(idea);
            TempData["message"] = string.Format($"\"{idea.Title}\" has been deleted");
            return RedirectToAction("List");
        }
    }
}