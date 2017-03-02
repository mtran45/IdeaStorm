using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdeaStorm.Domain.Entities;

namespace IdeaStorm.Domain.Abstract
{
    public interface IIdeaSparkRepository
    {
        IEnumerable<Idea> Ideas { get; }
        IEnumerable<Spark> Sparks { get; }

        void SaveIdea(Idea idea);
        void DeleteIdea(Idea idea);

        void SaveSpark(Spark spark);
    }
}
