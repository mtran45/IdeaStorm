using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace IdeaStorm.Domain.Entities
{
    public class Idea
    {
        [HiddenInput(DisplayValue = false)]
        public int IdeaID { get; set; }

        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        [Required(ErrorMessage = "Please enter an idea title")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string Category { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
        public virtual Spark Spark { get; set; }

        public Idea()
        {
            CreatedTime = DateTime.UtcNow;
            UpdatedTime = DateTime.UtcNow;
        }

        public Idea(User user) : this()
        {
            User = user;
        }

        public Idea(User user, string title) : this(user)
        {
            Title = title;
        }
    }
}
