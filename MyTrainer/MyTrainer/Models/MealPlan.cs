using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyTrainer.Models
{
    public class MealPlan
    {
        [Key]
        public int Id { get; set; }
        public string mealPlanDetails { get; set; }
        public double? CalorieIntake { get; set; }
        public double? ProteinIntake { get; set; }
        public double? CarbIntake { get; set; }
        public double? FatIntake { get; set; }
        public double? CaloriesAdded { get; set; }
        public double? ProteinAdded { get; set; }
        public double? CarbsAdded { get; set; }
        public double? FatAdded { get; set; }

    }
}