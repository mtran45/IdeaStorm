﻿using System.Collections.Generic;
using IdeaStorm.Domain.Entities;

namespace IdeaStorm.Domain.Abstract
{
    public interface ISparkRepository
    {
        IEnumerable<Spark> Sparks { get; }

        void SaveSpark(Spark spark);
    }
}