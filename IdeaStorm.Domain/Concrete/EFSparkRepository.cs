using System;
using System.Collections.Generic;
using IdeaStorm.Domain.Abstract;
using IdeaStorm.Domain.Entities;

namespace IdeaStorm.Domain.Concrete
{
    public class EFSparkRepository : ISparkRepository
    {
        private EFDbContext context;

        public EFSparkRepository(EFDbContext context)
        {
            this.context = context;
        }

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
    }
}
