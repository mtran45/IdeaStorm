using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaStorm.Domain.Entities
{
    public class Spark
    {
        public int SparkID { get; set; }
        public string Title { get; set; }
        public int UserID { get; set; }
        public int StormID { get; set; }
        public int IdeaID { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
        public virtual Storm Storm { get; set; }
        public virtual Idea Idea { get; set; }

        public Spark()
        {
            CreatedTime = DateTime.Now;
            UpdatedTime = DateTime.Now;
            UserID = 1;
        }

        public Spark(string title) : this()
        {
            Title = title;
        }
    }
}
