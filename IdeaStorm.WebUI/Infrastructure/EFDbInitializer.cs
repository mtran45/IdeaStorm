using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using IdeaStorm.Domain.Concrete;
using IdeaStorm.Domain.Entities;
using Microsoft.AspNet.Identity;

namespace IdeaStorm.WebUI.Infrastructure
{
    public class EFDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            var hasher = new PasswordHasher();
            var user = new User
            {
                UserName = "user",
                Email = "user@email.com",
                PasswordHash = hasher.HashPassword("pass"),
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedTime = DateTime.Parse("2005-09-01")
            };
            context.Users.AddOrUpdate(u => u.UserName, user);

            var users = new List<User>
            {
                user,
                new User {UserName="bill", CreatedTime=DateTime.Parse("2002-08-05")},
                new User {UserName="ben", CreatedTime=DateTime.Parse("2010-02-09")},
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var storm = new Storm()
            {
                User = user,
                Title = "Example Storm"
            };

            var storm2 = new Storm()
            {
                User = user,
                Title = "Brainstorm 19/2/17"
            };

            var storms = new List<Storm>
            {
                storm, storm2
            };
            storms.ForEach(s => context.Storms.Add(s));
            context.SaveChanges();

            var spark = new Spark(user)
            {
                Title = "Storm Spark 1",
                Storm = storm
            };

            var sparks = new List<Spark>
            {
                spark,
                new Spark(user) {Title = "Storm Spark 2", Storm = storm},
                new Spark(user) {Title = "Storm Spark 3", Storm = storm},
                new Spark(user) {Title = "Storm Spark 4", Storm = storm},
                new Spark(user) {Title = "Storm Spark 5", Storm = storm},
                new Spark(user) {Title = "Storm Spark 6", Storm = storm},
                new Spark(user) {Title = "Storm Spark 7", Storm = storm},
                new Spark(user) {Title = "Storm Spark 8", Storm = storm},
                new Spark(user) {Title = "Storm Spark 9", Storm = storm},
                new Spark(user) {Title = "Storm Spark 10", Storm = storm},
                new Spark(user) {Title = "Brainstorm 1", Storm = storm2},
                new Spark(user) {Title = "Brainstorm 2", Storm = storm2},
                new Spark(user) {Title = "Brainstorm 3", Storm = storm2},
                new Spark(user) {Title = "Brainstorm 4", Storm = storm2},
                new Spark(user) {Title = "Brainstorm 5", Storm = storm2},
                new Spark(user) {Title = "Brainstorm 6", Storm = storm2},
                new Spark(user) {Title = "Brainstorm 7", Storm = storm2},
                new Spark(user) {Title = "Brainstorm 8", Storm = storm2},
                new Spark(user) {Title = "Brainstorm 9", Storm = storm2},
                new Spark(user) {Title = "Brainstorm 10", Storm = storm2},
            };
            sparks.ForEach(s => context.Sparks.Add(s));
            context.SaveChanges();

            var promotedIdea = new Idea(user)
            {
                Title = "Storm Idea 1",
                Description = "Promoted from Storm Spark 1",
                Spark = spark
            };

            var ideas = new List<Idea>
            {
                new Idea(user) {Title ="My Great Idea", Description ="The description for the idea here", Category="Misc"},
                new Idea(user) {Title="IdeaStorm", Description="A webapp for brainstorming ideas", Category="Web App"},
                new Idea(users[1]) {Title="J-Reader", Description="A web app for reading japanese ebooks", Category="Web App"},
                new Idea(users[2]) {Title="Jukugo Basket", Description="A virtual replication of the card game", Category="Game"},
                new Idea(user) {Title="My Great Idea 2", Description="The second revision of my great idea", Category="Misc"},
                promotedIdea
            };
            ideas.ForEach(i => context.Ideas.Add(i));
            context.SaveChanges();
        }
    }
}