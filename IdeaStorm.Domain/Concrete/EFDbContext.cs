using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdeaStorm.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdeaStorm.Domain.Concrete
{
    public class EFDbContext : IdentityDbContext<User>
    {
        public DbSet<Idea> Ideas { get; set; }
        public DbSet<Storm> Storms { get; set; }
        public DbSet<Spark> Sparks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Spark>()
                        .HasOptional(s => s.Idea)
                        .WithOptionalPrincipal(i => i.Spark);
        }
    }
}
