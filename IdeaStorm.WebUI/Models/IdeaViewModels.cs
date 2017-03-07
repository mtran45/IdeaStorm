using System.ComponentModel.DataAnnotations;
using IdeaStorm.Domain.Entities;

namespace IdeaStorm.WebUI.Models
{

    public class CreateIdeaViewModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        public string Category { get; set; }

        public int? SparkID { get; set; }
    }

}