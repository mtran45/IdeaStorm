using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IdeaStorm.Domain.Entities
{
    public class Idea
    {
        [HiddenInput(DisplayValue = false)]
        public int IdeaID { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedTime { get; set; } = DateTime.Now;

        [HiddenInput(DisplayValue = false)]
        public DateTime UpdatedTime { get; set; } = DateTime.Now;

        [HiddenInput(DisplayValue = false)]
        public int AuthorID { get; set; } = 0;

        [Required(ErrorMessage = "Please enter an idea name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string Category { get; set; }

        public Idea() {}

        public Idea(string name)
        {
            this.Name = name;
        }
    }
}
