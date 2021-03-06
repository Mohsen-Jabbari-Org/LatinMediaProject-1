using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class StartModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        public string Id { get; set; }

        [Display(Name = "Join Url")]
        public string Url { get; set; }
        public int MaxUsers { get; set; }
        public int Duration { get; set; }
        public string ClassName { get; set; }
        public int ServerId { get; set; }
    }
}
