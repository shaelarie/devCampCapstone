using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyTrainer.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        [Display(Name = "weight in pounds")]
        public int? Weight { get; set; }
        [Display(Name = "height in feet")]
        public int? HeightFt { get; set; }
        [Display(Name = "height in inches")]
        public int? HeightIn { get; set; }
        [ForeignKey("ApplicationUsers")]
        public string LoginId { get; set; }
        public ApplicationUser ApplicationUsers { get; set; }
    }

}