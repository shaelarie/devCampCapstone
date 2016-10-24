using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyTrainer.Models
{
    public class Meal2
    {
        [Key]
        public int Id { get; set; }
        public string FoodItem { get; set; }
        public double? servingSize { get; set; }
        public double? calories { get; set; }
        public double? protein { get; set; }
        public double? fat { get; set; }
        public double? carbs { get; set; }
        [ForeignKey("MealPlan")]
        public int MealPlanId { get; set; }
        public MealPlan MealPlan { get; set; }
    }
}