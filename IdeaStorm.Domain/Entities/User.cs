using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaStorm.Domain.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public DateTime CreatedTime { get; set; }

        // Navigation properties
        public virtual ICollection<Idea> Ideas { get; set; }
    }
}
