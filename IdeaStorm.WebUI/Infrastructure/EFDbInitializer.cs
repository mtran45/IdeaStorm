using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdeaStorm.Domain.Concrete;
using IdeaStorm.Domain.Entities;

namespace IdeaStorm.WebUI.Infrastructure
{
    public class EFDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            var users = new List<User>
            {
                new User {Username="default_user", CreatedTime=DateTime.Parse("2005-09-01")},
                new User {Username="bill", CreatedTime=DateTime.Parse("2002-08-05")},
                new User {Username="ben", CreatedTime=DateTime.Parse("2010-02-09")},
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var storms = new List<Storm>
            {
                new Storm {UserID=1, Title="Example Storm"},
                new Storm {UserID=1, Title="Brainstorm 19/2"},
            };
            storms.ForEach(s => context.Storms.Add(s));
            context.SaveChanges();

            var ideas = new List<Idea>
            {
                new Idea {UserID=1, Title ="My Great Idea", Description ="The description for the idea here", Category="Misc"},
                new Idea {UserID=1, Title="IdeaStorm", Description="A webapp for brainstorming ideas", Category="Web App"},
                new Idea {UserID=2, Title="J-Reader", Description="A web app for reading japanese ebooks", Category="Web App"},
                new Idea {UserID=3, Title="Jukugo Basket", Description="A virtual replication of the card game", Category="Game"},
                new Idea {UserID=1, Title="My Great Idea 2", Description="The second revision of my great idea", Category="Misc"},
                new Idea {UserID=1, Title="Storm Idea 1", StormID=1},
                new Idea {UserID=1, Title="Storm Idea 2", StormID=1},
                new Idea {UserID=1, Title="Storm Idea 3", StormID=1},
                new Idea {UserID=1, Title="Storm Idea 4", StormID=1},
                new Idea {UserID=1, Title="Storm Idea 5", StormID=1},
                new Idea {UserID=1, Title="Storm Idea 6", StormID=1},
                new Idea {UserID=1, Title="Storm Idea 7", StormID=1},
                new Idea {UserID=1, Title="Storm Idea 8", StormID=1},
                new Idea {UserID=1, Title="Storm Idea 9", StormID=1},
                new Idea {UserID=1, Title="Storm Idea 10", StormID=1},
                new Idea {UserID=1, Title="Brainstorm 1", StormID=1},
                new Idea {UserID=1, Title="Brainstorm 2", StormID=1},
                new Idea {UserID=1, Title="Brainstorm 3", StormID=1},
            };
            ideas.ForEach(i => context.Ideas.Add(i));
            context.SaveChanges();
        }
    }
}