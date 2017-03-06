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
    public class IdeaController : Controller
    {
        private IIdeaRepository ideaRepo;
        private ISparkRepository sparkRepo;
        private IUserRepository userRepo;

        public IdeaController(IIdeaRepository ideaRepo, ISparkRepository sparkRepo, IUserRepository userRepo)
        {
            this.ideaRepo = ideaRepo;
            this.sparkRepo = sparkRepo;
            this.userRepo = userRepo;
        }

        public Idea FindIdea(int id)
        {
            return ideaRepo.Ideas.FirstOrDefault(i => i.IdeaID == id);
        }

        private AppUserManager _userManager;
        public AppUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: /
        public ViewResult List()
        {
            return View(ideaRepo.Ideas.OrderByDescending(i => i.CreatedTime));
        }

        // GET: Idea/Edit/5
        public ActionResult Edit(int id)
        {
            //Idea idea = ideaRepo.FindIdea(id);
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
                ideaRepo.SaveIdea(idea);
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
            return View();
        }

        public ActionResult PromoteSpark(int id)
        {
            Spark spark = sparkRepo.Sparks.FirstOrDefault(s => s.SparkID == id);
            if (spark == null)
            {
                return HttpNotFound();
            }
            var vm = new CreateIdeaViewModel
            {
                Spark = spark,
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
                var currUser = UserManager.FindById(User.Identity.GetUserId());
                var idea = new Idea
                {
                    Title = model.Title,
                    Description = model.Description,
                    Category = model.Category,
                    User = userRepo.GetUserByID(currUser.Id),
                    Spark = model.Spark
                };
                //Spark spark = ideaRepo.Sparks.FirstOrDefault(s => s.SparkID == SparkID);
                ideaRepo.SaveIdea(idea);
                TempData["message"] = string.Format($"\"{idea.Title}\" has been added");
                return RedirectToAction("List");
            }

            return View(model);
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
            ideaRepo.DeleteIdea(idea);
            TempData["message"] = string.Format($"\"{idea.Title}\" has been deleted");
            return RedirectToAction("List");
        }

    }
}