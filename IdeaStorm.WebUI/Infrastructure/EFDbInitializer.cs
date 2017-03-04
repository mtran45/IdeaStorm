using System;
using System.Collections.Generic;
using IdeaStorm.Domain.Concrete;
using IdeaStorm.Domain.Entities;

namespace IdeaStorm.WebUI.Infrastructure
{
    public class EFDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            var user = new User
            {
                Username = "default_user",
                CreatedTime = DateTime.Parse("2005-09-01")
            };

            var users = new List<User>
            {
                user,
                new User {Username="bill", CreatedTime=DateTime.Parse("2002-08-05")},
                new User {Username="ben", CreatedTime=DateTime.Parse("2010-02-09")},
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var storm = new Storm()
            {
                UserID = 1,
                Title = "Example Storm"
            };

            var storm2 = new Storm()
            {
                UserID = 1,
                Title = "Brainstorm 19/2/17"
            };

            var storms = new List<Storm>
            {
                storm, storm2
            };
            storms.ForEach(s => context.Storms.Add(s));
            context.SaveChanges();

            var spark = new Spark()
            {
                User = user,
                Title = "Storm Spark 1",
                Storm = storm
            };

            var sparks = new List<Spark>
            {
                spark,
                new Spark {User = user, Title = "Storm Spark 2", Storm = storm},
                new Spark {User = user, Title = "Storm Spark 3", Storm = storm},
                new Spark {User = user, Title = "Storm Spark 4", Storm = storm},
                new Spark {User = user, Title = "Storm Spark 5", Storm = storm},
                new Spark {User = user, Title = "Storm Spark 6", Storm = storm},
                new Spark {User = user, Title = "Storm Spark 7", Storm = storm},
                new Spark {User = user, Title = "Storm Spark 8", Storm = storm},
                new Spark {User = user, Title = "Storm Spark 9", Storm = storm},
                new Spark {User = user, Title = "Storm Spark 10", Storm = storm},
                new Spark {User = user, Title = "Brainstorm 1", Storm = storm2},
                new Spark {User = user, Title = "Brainstorm 2", Storm = storm2},
                new Spark {User = user, Title = "Brainstorm 3", Storm = storm2},
            };
            sparks.ForEach(s => context.Sparks.Add(s));
            context.SaveChanges();

            var promotedIdea = new Idea()
            {
                UserID = 1,
                Title = "Storm Idea 1",
                Description = "Promoted from Storm Spark 1",
                Spark = spark
            };

            var ideas = new List<Idea>
            {
                new Idea {UserID=1, Title ="My Great Idea", Description ="The description for the idea here", Category="Misc"},
                new Idea {UserID=1, Title="IdeaStorm", Description="A webapp for brainstorming ideas", Category="Web App"},
                new Idea {UserID=2, Title="J-Reader", Description="A web app for reading japanese ebooks", Category="Web App"},
                new Idea {UserID=3, Title="Jukugo Basket", Description="A virtual replication of the card game", Category="Game"},
                new Idea {UserID=1, Title="My Great Idea 2", Description="The second revision of my great idea", Category="Misc"},
                promotedIdea
            };
            ideas.ForEach(i => context.Ideas.Add(i));
            context.SaveChanges();
        }
    }
}