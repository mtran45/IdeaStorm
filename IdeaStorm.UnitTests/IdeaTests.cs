using System.Collections.Generic;
using System.Web.Mvc;
using IdeaStorm.Domain.Abstract;
using IdeaStorm.Domain.Entities;
using IdeaStorm.WebUI.Controllers;
using IdeaStorm.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IdeaStorm.UnitTests
{
    [TestClass]
    public class IdeaTests
    {
        private User user;

        [TestInitialize()]
        public void Initialize()
        {
            user = new User();
        }

        [TestMethod]
        public void Can_Edit_Idea()
        {
            // Arrange - create the mock repo
            Mock<IIdeaRepository> mock = new Mock<IIdeaRepository>();
            mock.Setup(m => m.Ideas).Returns(new Idea[]
            {
                new Idea(user) {IdeaID = 1, Title = "I1" },
                new Idea(user) {IdeaID = 2, Title = "I2" },
                new Idea(user) {IdeaID = 3, Title = "I3" },
            });

            // Arrange - create the controller
            IdeaController target = new IdeaController(mock.Object, null);

            // Act
            Idea i1 = ((ViewResult)target.Edit(1)).ViewData.Model as Idea;
            Idea i2 = ((ViewResult)target.Edit(2)).ViewData.Model as Idea;
            Idea i3 = ((ViewResult)target.Edit(3)).ViewData.Model as Idea;

            // Assert
            Assert.AreEqual(1, i1.IdeaID);
            Assert.AreEqual(2, i2.IdeaID);
            Assert.AreEqual(3, i3.IdeaID);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Idea()
        {
            Mock<IIdeaRepository> mock = new Mock<IIdeaRepository>();
            mock.Setup(m => m.Ideas).Returns(new Idea[]
            {
                new Idea(user) {IdeaID = 1, Title = "I1" },
                new Idea(user) {IdeaID = 2, Title = "I2" },
                new Idea(user) {IdeaID = 3, Title = "I3" },
            });

            // Arrange - create the controller
            IdeaController target = new IdeaController(mock.Object, null);

            // Act
            ActionResult result = target.Edit(4);

            // Assert
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Arrange - create mock repo
            Mock<IIdeaRepository> mock = new Mock<IIdeaRepository>();
            // Arrange - create the controller
            IdeaController target = new IdeaController(mock.Object, null);
            // Arrange - create an idea
            Idea idea = new Idea(user) {Title = "Test"};

            // Act - try to save the idea
            ActionResult result = target.Edit(idea);

            // Assert - check that the repo was called
            mock.Verify(m => m.SaveIdea(idea));
            // Assert - check the method result type
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Arrange - create mock repo
            Mock<IIdeaRepository> mock = new Mock<IIdeaRepository>();
            // Arrange - create the controller
            IdeaController target = new IdeaController(mock.Object, null);
            // Arrange - create an idea
            Idea idea = new Idea(user) { Title = "Test" };
            // Arrange - add an error to the model state
            target.ModelState.AddModelError("error", "error");

            // Act - try to save the idea
            ActionResult result = target.Edit(idea);

            // Assert - check that the repo was not called
            mock.Verify(m => m.SaveIdea(It.IsAny<Idea>()), Times.Never);
            // Assert - check the method result type
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Create_Idea()
        {
            // Arrange - create an Idea
            Idea idea = new Idea(user) { IdeaID = 2, Title = "Test" };

            // Arrange - create the mock repo
            Mock<IIdeaRepository> mock = new Mock<IIdeaRepository>();

            // Arrange - create the controller
            IdeaController target = new IdeaController(mock.Object, null);

            var vm = new CreateIdeaViewModel() {Title = idea.Title};

            // Act
            target.Create(vm);

            // Assert - ensure that the repository create method was called with correct Idea
            mock.Verify(m => m.SaveIdea(It.Is<Idea>(i => i.Title == idea.Title)));
        }

        [TestMethod]
        public void Can_Delete_Idea()
        {
            // Arrange - create an Idea
            Idea idea = new Idea(user) {IdeaID = 2, Title = "Test"};

            // Arrange - create the mock repo
            Mock<IIdeaRepository> mock = new Mock<IIdeaRepository>();
            mock.Setup(m => m.Ideas).Returns(new Idea[]
            {
                new Idea(user) {IdeaID = 1, Title = "I1" },
                idea,
                new Idea(user) {IdeaID = 3, Title = "I3" },
            });

            // Arrange - create the controller
            IdeaController target = new IdeaController(mock.Object, null);

            // Act
            target.DeleteIdea(idea.IdeaID);

            // Assert - ensure that the repository delete method was called with correct Idea
            mock.Verify(m => m.DeleteIdea(idea));
        }
    }
}
