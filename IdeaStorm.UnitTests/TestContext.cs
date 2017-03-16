using System;
using System.Data.Entity;
using System.Linq;
using IdeaStorm.Domain.Abstract;
using IdeaStorm.Domain.Entities;

namespace IdeaStorm.UnitTests
{
    public class TestContext : IDbContext
    {
        public TestContext()
        {
            this.Ideas = new TestDbSet<Idea>();
            this.Storms = new TestDbSet<Storm>();
            this.Sparks = new TestDbSet<Spark>();
            this.Users = new TestDbSet<User>();
        }

        public DbSet<Idea> Ideas { get; set; }
        public DbSet<Storm> Storms { get; set; }
        public DbSet<Spark> Sparks { get; set; }
        public IDbSet<User> Users { get; set; }

        public int SaveChangesCount { get; private set; }

        public int SaveChanges()
        {
            this.SaveChangesCount++;
            return 1;
        }

        public void SaveIdea(Idea idea)
        {
            idea.UpdatedTime = DateTime.UtcNow;
            if (idea.IdeaID == 0)
            {
                Ideas.Add(idea);
            }
            else
            {
                Idea dbEntry = Ideas.Find(idea.IdeaID);
                if (dbEntry != null)
                {
                    dbEntry.Title = idea.Title;
                    dbEntry.Description = idea.Description;
                    dbEntry.Category = idea.Category;
                    dbEntry.UpdatedTime = idea.UpdatedTime;
                }
            }
            SaveChanges();
        }

        public void DeleteIdea(Idea idea)
        {
            Ideas.Remove(idea);
            SaveChanges();
        }

        public Spark GetSparkByID(int? id)
        {
            return Sparks.FirstOrDefault(s => s.SparkID == id);
        }

        public void SaveSpark(Spark spark)
        {
            spark.UpdatedTime = DateTime.UtcNow;
            if (spark.SparkID == 0)
            {
                Sparks.Add(spark);
            }
            else
            {
                Spark dbEntry = Sparks.Find(spark.SparkID);
                if (dbEntry != null)
                {
                    dbEntry.Title = spark.Title;
                    dbEntry.UpdatedTime = spark.UpdatedTime;
                }
            }
            SaveChanges();
        }

        public void SaveStorm(Storm storm)
        {
            storm.UpdatedTime = DateTime.UtcNow;
            if (storm.StormID == 0)
            {
                Storms.Add(storm);
            }
            else
            {
                Storm dbEntry = Storms.Find(storm.StormID);
                if (dbEntry != null)
                {
                    dbEntry.Title = storm.Title;
                    dbEntry.Description = storm.Description;
                    dbEntry.UpdatedTime = storm.UpdatedTime;
                }
            }
            SaveChanges();
        }

        public void DeleteStorm(Storm storm)
        {
            Sparks.RemoveRange(storm.Sparks);
            Storms.Remove(storm);
            SaveChanges();
        }

        public User GetUserByID(string userID)
        {
            return Users.FirstOrDefault(u => u.Id == userID);
        }
    }
}
