using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyTrainer.Models
{
    public class BasicMealPlan
    {
        [Key]
        public int Id { get; set; }
        public double? ProteinIntake { get; set; }
        public double? CarbIntake { get; set; }
        public double? FatIntake { get; set; }
        public List<string> Meal1 { get; set; }
        public List<string> Snack1 { get; set; }
        public List<string> Meal2 { get; set; }
        public List<string> Snack2 { get; set; }
        public List<string> Meal3 { get; set; }
    }
}