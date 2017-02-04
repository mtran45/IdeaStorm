﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdeaStorm.Domain.Abstract;
using IdeaStorm.Domain.Entities;

namespace IdeaStorm.Domain.Concrete
{
    public class EFIdeaRepository : IIdeaRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Idea> Ideas
        {
            get { return context.Ideas; }
        }

        public void SaveIdea(Idea idea)
        {
            if (idea.IdeaID == 0)
            {
                context.Ideas.Add(idea);
            }
            else
            {
                Idea dbEntry = context.Ideas.Find(idea.IdeaID);
                if (dbEntry != null)
                {
                    dbEntry.Name = idea.Name;
                    dbEntry.Description = idea.Description;
                    dbEntry.Category = idea.Category;
                }
                context.SaveChanges();
            }
        }
    }
}
