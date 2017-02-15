using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IdeaStorm.Domain.Entities
{
    public class Idea
    {
        [HiddenInput(DisplayValue = false)]
        public int IdeaID { get; set; }

        // Foreign keys
        [HiddenInput(DisplayValue = false)]
        public int UserID { get; set; }

        [HiddenInput(DisplayValue = false)]
        [DisplayName("Created Time")]
        public DateTime CreatedTime { get; set; }

        [HiddenInput(DisplayValue = false)]
        [DisplayName("Updated Time")]
        public DateTime UpdatedTime { get; set; }

        [Required(ErrorMessage = "Please enter an idea name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string Category { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }

        public Idea()
        {
            CreatedTime = DateTime.Now;
            UpdatedTime = DateTime.Now;
            UserID = 1;
        }

        public Idea(string name) : this()
        {
            Name = name;
        }
    }
}
