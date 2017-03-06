﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdeaStorm.Domain.Abstract;
using IdeaStorm.Domain.Entities;

namespace IdeaStorm.Domain.Concrete
{
    public class EFStormRepository : IStormRepository
    {
        private EFDbContext context;

        public EFStormRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Storm> Storms
        {
            get { return context.Storms;  }
        }

        public void SaveStorm(Storm storm)
        {
            storm.UpdatedTime = DateTime.Now;
            if (storm.StormID == 0)
            {
                context.Storms.Add(storm);
            }
            else
            {
                Storm dbEntry = context.Storms.Find(storm.StormID);
                if (dbEntry != null)
                {
                    dbEntry.Title = storm.Title;
                    dbEntry.Description = storm.Description;
                    dbEntry.UpdatedTime = storm.UpdatedTime;
                }
            }
            context.SaveChanges();
        }

        public void DeleteStorm(Storm storm)
        {
            context.Sparks.RemoveRange(storm.Sparks);
            context.Storms.Remove(storm);
            context.SaveChanges();
        }
    }
}
