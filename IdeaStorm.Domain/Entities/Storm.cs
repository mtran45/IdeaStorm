using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdeaStorm.Domain.Entities
{
    public class Storm
    {
        public int StormID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        // Navigation properties
        public virtual ICollection<Spark> Sparks { get; set; }
        public virtual User User { get; set; }

        public Storm()
        {
            CreatedTime = DateTime.UtcNow;
            UpdatedTime = DateTime.UtcNow;
        }
    }
}
