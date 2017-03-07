using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using IdeaStorm.Domain.Abstract;
using IdeaStorm.Domain.Entities;
using IdeaStorm.WebUI.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IdeaStorm.UnitTests
{
    [TestClass]
    public class StormTests
    {
        [TestMethod]
        public void Can_Bulk_Create_Sparks_With_Brainstorm()
        {
            // Arrange - create a list of spark names
            string[] arr = { "I1", "I2", "I3" };
            List<string> ideaTitles = new List<string>(arr);

            // Arrange - create the mock repos
            Mock<IStormRepository> stormMock = new Mock<IStormRepository>();
            Mock<ISparkRepository> sparkMock = new Mock<ISparkRepository>();
            Mock<IUserRepository> userMock = new Mock<IUserRepository>();

            // Arrange - create the controller
            StormController target = new StormController(stormMock.Object, sparkMock.Object,
                                                         userMock.Object)
            {
                GetUserId = () => "UserId"
            };

            // Act
            target.Brainstorm("Storm Title", ideaTitles);

            // Assert - ensure that ideas are created with correct titles
            sparkMock.Verify(m => m.SaveSpark(It.Is<Spark>(i => i.Title == "I1")));
            sparkMock.Verify(m => m.SaveSpark(It.Is<Spark>(i => i.Title == "I2")));
            sparkMock.Verify(m => m.SaveSpark(It.Is<Spark>(i => i.Title == "I3")));

            // Assert - ensure that storm is created with correct title
            stormMock.Verify(m => m.SaveStorm(It.Is<Storm>(s => s.Title == "Storm Title")));
        }

        [TestMethod]
        public void Can_Delete_Storm()
        {
            // Arrange - create an Storm
            Storm storm = new Storm { StormID = 2, Title = "Test Storm" };

            // Arrange - create the mock repos
            Mock<IStormRepository> stormMock = new Mock<IStormRepository>();
            stormMock.Setup(m => m.Storms).Returns(new Storm[]
            {
                new Storm { StormID = 1, Title = "S1" },
                storm,
                new Storm { StormID = 3, Title = "S2" }
            });

            // Arrange - create the controller
            StormController stormTarget = new StormController(stormMock.Object, null, null);

            // Act
            stormTarget.DeleteStorm(storm.StormID);

            // Assert - ensure that the repository delete method was called with correct Storm
            stormMock.Verify(m => m.DeleteStorm(storm));
        }
    }
}
