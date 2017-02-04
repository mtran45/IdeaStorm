using System;
using System.Web.Mvc;
using IdeaStorm.Domain.Abstract;
using IdeaStorm.Domain.Entities;
using IdeaStorm.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IdeaStorm.UnitTests
{
    [TestClass]
    public class IdeaTests
    {
        [TestMethod]
        public void Can_Edit_Idea()
        {
            // Arrange - create the mock repo
            Mock<IIdeaRepository> mock = new Mock<IIdeaRepository>();
            mock.Setup(m => m.Ideas).Returns(new Idea[]
            {
                new Idea() {IdeaID = 1, Name = "I1" },
                new Idea() {IdeaID = 2, Name = "I2" },
                new Idea() {IdeaID = 3, Name = "I3" },
            });

            // Arrange - create the controller
            IdeaController target = new IdeaController(mock.Object);

            // Act
            Idea i1 = target.Edit(1).ViewData.Model as Idea;
            Idea i2 = target.Edit(2).ViewData.Model as Idea;
            Idea i3 = target.Edit(3).ViewData.Model as Idea;

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
                new Idea() {IdeaID = 1, Name = "I1" },
                new Idea() {IdeaID = 2, Name = "I2" },
                new Idea() {IdeaID = 3, Name = "I3" },
            });

            // Arrange - create the controller
            IdeaController target = new IdeaController(mock.Object);

            // Act
            Idea result = (Idea) target.Edit(4).ViewData.Model;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Arrange - create mock repo
            Mock<IIdeaRepository> mock = new Mock<IIdeaRepository>();
            // Arrange - create the controller
            IdeaController target = new IdeaController(mock.Object);
            // Arrange - create an idea
            Idea idea = new Idea() {Name = "Test"};

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
            IdeaController target = new IdeaController(mock.Object);
            // Arrange - create an idea
            Idea idea = new Idea() { Name = "Test" };
            // Arrange - add an error to the model state
            target.ModelState.AddModelError("error", "error");

            // Act - try to save the idea
            ActionResult result = target.Edit(idea);

            // Assert - check that the repo was not called
            mock.Verify(m => m.SaveIdea(It.IsAny<Idea>()), Times.Never);
            // Assert - check the method result type
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
