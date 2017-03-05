using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
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
    }

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
}
