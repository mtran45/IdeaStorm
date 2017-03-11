using System.Collections.Generic;
using System.Linq;
using IdeaStorm.Domain.Entities;
using IdeaStorm.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdeaStorm.UnitTests
{
    [TestClass]
    public class StormTests
    {
        [TestMethod]
        public void Can_Bulk_Create_Sparks_With_Brainstorm()
        {
            // Arrange - create a list of spark names
            List<string> ideaTitles = new List<string>(new[] { "I1", "I2", "I3" });

            // Arrange - create the mock repos
            var context = new TestContext();

            // Arrange - create the controller
            StormController target = new StormController(context)
            {
                GetUserId = () => "UserId"
            };

            // Act
            target.Brainstorm("Storm Title", ideaTitles);

            // Assert - ensure that storm is created with correct title
            Assert.AreEqual(1, context.Storms.Count());
            Assert.AreEqual("Storm Title", context.Storms.Single().Title);

            // Assert - ensure that sparks are created
            Assert.AreEqual(3, context.Sparks.Count());
            Assert.IsTrue(context.Sparks.Any(i => i.Title == "I1"));
            Assert.IsTrue(context.Sparks.Any(i => i.Title == "I2"));
            Assert.IsTrue(context.Sparks.Any(i => i.Title == "I3"));
        }

        [TestMethod]
        public void Can_Delete_Storm()
        {
            // Arrange - create Sparks to be associated with the storm
            var spark1 = new Spark { SparkID = 1, Title = "Sp1" };
            var spark2 = new Spark { SparkID = 2, Title = "Sp2" };
            var spark3 = new Spark { SparkID = 3, Title = "Sp3" };
            var sparks = new List<Spark> { spark1, spark2, spark3 };
            // Create a spark not associated with the storm
            var spark4 = new Spark { SparkID = 4, Title = "Sp4" };

            // Arrange - create an Storm
            Storm storm = new Storm { StormID = 2, Title = "Test Storm", Sparks = sparks};

            // Arrange - create the mock repos
            var context = new TestContext();

            context.Sparks.Add(spark1);
            context.Sparks.Add(spark2);
            context.Sparks.Add(spark3);
            context.Sparks.Add(spark4);

            context.Storms.Add(new Storm {StormID = 1, Title = "S1"});
            context.Storms.Add(storm);
            context.Storms.Add(new Storm {StormID = 3, Title = "S3"});

            // Arrange - create the controller
            StormController stormTarget = new StormController(context);

            // Act
            stormTarget.DeleteStorm(storm.StormID);

            // Assert - ensure that the correct Storm is deleted
            Assert.AreEqual(2, context.Storms.Count());
            Assert.IsFalse(context.Storms.Any(s => s.StormID == 2));

            // Assert - ensure that the associated Sparks are deleted, only spark4 should be left
            Assert.AreEqual(1, context.Sparks.Count());
            Assert.AreEqual(spark4, context.Sparks.Single());
        }
    }
}
