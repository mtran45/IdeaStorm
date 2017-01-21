﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaStorm.Domain.Entities
{
    public class Idea
    {
        public int IdeaID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AuthorID { get; set; }
        public string Category { get; set; }
    }
}
