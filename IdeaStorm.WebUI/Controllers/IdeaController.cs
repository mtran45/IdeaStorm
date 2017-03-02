using System.Linq;
using System.Web.Mvc;
using IdeaStorm.Domain.Abstract;
using IdeaStorm.Domain.Entities;

namespace IdeaStorm.WebUI.Controllers
{
    public class IdeaController : Controller
    {
        private IIdeaSparkRepository ideaSparkRepo;

        public IdeaController(IIdeaSparkRepository ideaSparkRepo)
        {
            this.ideaSparkRepo = ideaSparkRepo;
        }

        public Idea FindIdea(int id)
        {
            return ideaSparkRepo.Ideas.FirstOrDefault(i => i.IdeaID == id);
        }

        // GET: /
        public ViewResult List()
        {
            return View(ideaSparkRepo.Ideas.OrderByDescending(i => i.CreatedTime));
        }

        // GET: Idea/Edit/5
        public ActionResult Edit(int id)
        {
            //Idea idea = ideaSparkRepo.FindIdea(id);
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
                ideaSparkRepo.SaveIdea(idea);
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

        public ActionResult PromoteSpark(int id)
        {
            Spark spark = ideaSparkRepo.Sparks.FirstOrDefault(s => s.SparkID == id);
            if (spark == null)
            {
                return HttpNotFound();
            }
            var idea = new Idea
            {
                Spark = spark,
                Title = spark.Title,
                User = spark.User
            };
            return View("Create", idea);
        }

        // POST: Idea/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Title,Description,Category,Spark")] Idea idea,
            int? SparkID)
        {
            Spark spark = ideaSparkRepo.Sparks.FirstOrDefault(s => s.SparkID == SparkID);
            idea.Spark = spark;
            if (ModelState.IsValid)
            {
                ideaSparkRepo.SaveIdea(idea);
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
            ideaSparkRepo.DeleteIdea(idea);
            TempData["message"] = string.Format($"\"{idea.Title}\" has been deleted");
            return RedirectToAction("List");
        }

    }
}