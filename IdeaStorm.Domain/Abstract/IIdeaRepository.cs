using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdeaStorm.Domain.Entities;

namespace IdeaStorm.Domain.Abstract
{
    public interface IIdeaRepository
    {
        IEnumerable<Idea> Ideas { get; }

        void SaveIdea(Idea idea);
        void DeleteIdea(Idea idea);

        Idea FindIdea(int id);
    }
}
