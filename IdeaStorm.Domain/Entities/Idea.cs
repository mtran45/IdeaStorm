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
        public DateTime CreatedTime { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime UpdatedTime { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int AuthorID { get; set; }

        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string Category { get; set; }
    }
}
