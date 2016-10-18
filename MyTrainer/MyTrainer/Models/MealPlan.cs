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
        public string MealPlanType { get; set; }
        public string mealPlanDetails { get; set; }

        [ForeignKey("VegetarianMealPlan")]
        public int? VegetarianId { get; set; }
        public VegetarianMealPlan VegetarianMealPlan { get; set; }
        [ForeignKey("VeganMealPlan")]
        public int? VeganId { get; set; }
        public VeganMealPlan VeganMealPlan { get; set; }
        [ForeignKey("BasicMealPlan")]
        public int? BasicId { get; set; }
        public BasicMealPlan BasicMealPlan { get; set; }

    }
}