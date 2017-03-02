using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdeaStorm.Domain.Abstract;
using IdeaStorm.Domain.Entities;

namespace IdeaStorm.Domain.Concrete
{
    public class EFIdeaSparkRepository : IIdeaSparkRepository
    {
        private EFDbContext context = new EFDbContext();

        #region Ideas

        public IEnumerable<Idea> Ideas
        {
            get { return context.Ideas; }
        }

        public void SaveIdea(Idea idea)
        {
            idea.UpdatedTime = DateTime.Now;
            if (idea.IdeaID == 0)
            {
                context.Ideas.Add(idea);
            }
            else
            {
                Idea dbEntry = context.Ideas.Find(idea.IdeaID);
                if (dbEntry != null)
                {
                    dbEntry.Title = idea.Title;
                    dbEntry.Description = idea.Description;
                    dbEntry.Category = idea.Category;
                    dbEntry.UpdatedTime = idea.UpdatedTime;
                }
            }
            context.SaveChanges();
        }

        public void DeleteIdea(Idea idea)
        {
            context.Ideas.Remove(idea);
            context.SaveChanges();
        }

        #endregion

        #region Sparks

        public IEnumerable<Spark> Sparks
        {
            get { return context.Sparks; }
        }

        public void SaveSpark(Spark spark)
        {
            spark.UpdatedTime = DateTime.Now;
            if (spark.SparkID == 0)
            {
                context.Sparks.Add(spark);
            }
            else
            {
                Spark dbEntry = context.Sparks.Find(spark.SparkID);
                if (dbEntry != null)
                {
                    dbEntry.Title = spark.Title;
                    dbEntry.UpdatedTime = spark.UpdatedTime;
                }
            }
            context.SaveChanges();
        }

        #endregion
    }
}

