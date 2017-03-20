using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using IdeaStorm.Domain.Concrete;
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
            user = new User()
            {
                Id = "1",
                UserName = "user"
            };
        }

        [TestMethod]
        public void Can_Edit_Idea()
        {
            // Arrange - create the mock repo and its data
            var context = new TestContext();

            context.Ideas.Add(new Idea(user) {IdeaID = 1, Title = "I1"});
            context.Ideas.Add(new Idea(user) {IdeaID = 2, Title = "I2"});
            context.Ideas.Add(new Idea(user) {IdeaID = 3, Title = "I3"});

            // Arrange - create the controller
            IdeaController target = new IdeaController(context)
            {
                GetUserId = () => user.Id
            };

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
            // Arrange - create the mock repo and its data
            var context = new TestContext();

            context.Ideas.Add(new Idea(user) { IdeaID = 1, Title = "I1" });
            context.Ideas.Add(new Idea(user) { IdeaID = 2, Title = "I2" });
            context.Ideas.Add(new Idea(user) { IdeaID = 3, Title = "I3" });

            // Arrange - create the controller
            IdeaController target = new IdeaController(context);

            // Act
            ActionResult result = target.Edit(4);

            // Assert
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Arrange - create mock repo
            var context = new TestContext();
            // Arrange - create the controller
            IdeaController target = new IdeaController(context);
            // Arrange - create an idea
            Idea idea = new Idea(user) {Title = "Test"};

            // Act - try to save the idea
            ActionResult result = target.Edit(idea);

            // Assert - check that the idea was saved
            Assert.AreEqual(1, context.Ideas.Count());
            Assert.AreEqual(idea.Title, context.Ideas.Single().Title);
            Assert.AreEqual(user, context.Ideas.Single().User);
            // Assert - check the method result type
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Arrange - create mock repo
            var context = new TestContext();
            // Arrange - create the controller
            IdeaController target = new IdeaController(context);
            // Arrange - create an idea
            Idea idea = new Idea(user) { Title = "Test" };
            // Arrange - add an error to the model state
            target.ModelState.AddModelError("error", "error");

            // Act - try to save the idea
            ActionResult result = target.Edit(idea);

            // Assert - check that the repo was not called
            Assert.AreEqual(0, context.Ideas.Count());
            // Assert - check the method result type
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Create_Idea()
        {
            // Arrange - create an Idea
            Idea idea = new Idea(user) { IdeaID = 2, Title = "Test" };

            // Arrange - create the mock repo
            var context = new TestContext();
            context.Users.Add(user);

            // Arrange - create the controller
            IdeaController target = new IdeaController(context)
            {
                GetUserId = () => user.Id
            };

            var model = new CreateIdeaViewModel {Title = idea.Title};

            // Act
            target.Create(model);

            // Assert - ensure that the repository create method was called with correct Idea
            Assert.AreEqual(1, context.Ideas.Count());
            Assert.AreEqual(idea.Title, context.Ideas.Single().Title);
            Assert.AreEqual(user, context.Ideas.Single().User);
        }

        [TestMethod]
        public void Can_Delete_Idea()
        {
            // Arrange - create an Idea
            Idea idea = new Idea(user) {IdeaID = 2, Title = "Test"};

            // Arrange - create the mock repo
            var context = new TestContext();

            context.Ideas.Add(new Idea(user) { IdeaID = 1, Title = "I1" });
            context.Ideas.Add(idea);
            context.Ideas.Add(new Idea(user) { IdeaID = 3, Title = "I3" });

            // Arrange - create the controller
            IdeaController target = new IdeaController(context);

            // Act
            target.DeleteIdea(idea.IdeaID);

            // Assert - ensure that an idea was deleted and it was the correct one
            Assert.AreEqual(2, context.Ideas.Count());
            Assert.IsFalse(context.Ideas.Any(i => i.IdeaID == 2));
        }

        [TestMethod]
        public void Can_Promote_Spark_To_Idea()
        {
            // Arrange - create a Spark
            Spark spark = new Spark(user) {SparkID = 1, Title = "Test Spark"};

            // Arrange - create the mock repo
            var context = new TestContext();
            context.Sparks.Add(spark);
            context.Users.Add(user);

            // Arrange - create the controller
            IdeaController target = new IdeaController(context)
            {
                GetUserId = () => user.Id
            };

            var model = new CreateIdeaViewModel { Title = spark.Title, SparkID = spark.SparkID };

            // Act
            target.Create(model);

            // Assert - ensure idea was created with the same title and associated spark
            Assert.AreEqual(1, context.Ideas.Count());
            Assert.AreEqual(spark.Title, context.Ideas.Single().Title);
            Assert.AreEqual(spark, context.Ideas.Single().Spark);
            Assert.AreEqual(user, context.Ideas.Single().User);
        }
    }
}
