using System.Data.Entity;
using IdeaStorm.Domain.Entities;

namespace IdeaStorm.Domain.Abstract
{
    public interface IDbContext
    {
        DbSet<Idea> Ideas { get; }
        DbSet<Storm> Storms { get; }
        DbSet<Spark> Sparks { get; }
        IDbSet<User> Users { get; }

        int SaveChanges();

        void SaveIdea(Idea idea);
        void DeleteIdea(Idea idea);
        Idea GetIdeaByID(int? id);

        Spark GetSparkByID(int? id);
        void SaveSpark(Spark spark);

        void SaveStorm(Storm storm);
        void DeleteStorm(Storm storm);
        Storm GetStormByID(int? id);

        User GetUserByID(string userID);
    }
}
