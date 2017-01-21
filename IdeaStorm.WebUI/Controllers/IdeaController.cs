using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdeaStorm.Domain.Abstract;

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
    }
}