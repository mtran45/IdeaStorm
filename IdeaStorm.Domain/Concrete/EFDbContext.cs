using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using IdeaStorm.Domain.Abstract;
using IdeaStorm.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdeaStorm.Domain.Concrete
{
    public class EFDbContext : IdentityDbContext<User>, IDbContext
    {
        public DbSet<Idea> Ideas { get; set; }
        public DbSet<Storm> Storms { get; set; }
        public DbSet<Spark> Sparks { get; set; }
        // Users DbSet defined in base class

        public EFDbContext() : base("name=EFDbContext") { }

        public static EFDbContext Create()
        {
            return new EFDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new IdentityUserLoginConfiguration());
            modelBuilder.Configurations.Add(new IdentityUserRoleConfiguration());

            modelBuilder.Entity<Spark>()
                        .HasOptional(s => s.Idea)
                        .WithOptionalPrincipal(i => i.Spark);
        }

        // Ideas

        public void SaveIdea(Idea idea)
        {
            idea.UpdatedTime = DateTime.Now;
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

        // Sparks

        public void SaveSpark(Spark spark)
        {
            spark.UpdatedTime = DateTime.Now;
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

        public Spark GetSparkByID(int? id)
        {
            return Sparks.Find(id);
        }

        // Storms

        public void SaveStorm(Storm storm)
        {
            storm.UpdatedTime = DateTime.Now;
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

        // Users

        public User GetUserByID(string userID)
        {
            return Users.Find(userID);
        }
    }

    #region Identity
    public class IdentityUserLoginConfiguration : EntityTypeConfiguration<IdentityUserLogin>
    {

        public IdentityUserLoginConfiguration()
        {
            HasKey(iul => iul.UserId);
        }

    }

    public class IdentityUserRoleConfiguration : EntityTypeConfiguration<IdentityUserRole>
    {

        public IdentityUserRoleConfiguration()
        {
            HasKey(iur => iur.RoleId);
        }

    }
    #endregion
}
