using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdeaStorm.Domain.Entities;

namespace IdeaStorm.WebUI.Models
{
    public class BrainstormViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Spark> Sparks { get; set; }
    }
}