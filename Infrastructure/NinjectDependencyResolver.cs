using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdeaStorm.Domain.Abstract;
using IdeaStorm.Domain.Entities;
using Moq;
using Ninject;

namespace IdeaStorm.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            Mock<IIdeaRepository> mock = new Mock<IIdeaRepository>();
            mock.Setup(m => m.Ideas).Returns(new List<Idea>
            {
                new Idea {Name = "Idea Storm", Description = "App for brainstorming ideas"},
                new Idea {Name = "J-Reader", Description = "A ebook reader for Japanese books"}
            });

            kernel.Bind<IIdeaRepository>().ToConstant(mock.Object);
        }
    }
}