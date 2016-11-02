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
        [Display(Name ="Age")]
        public int? age { get; set; }
        public string Username { get; set; }
        [Display(Name = "weight in pounds")]
        public int? Weight { get; set; }
        [Display(Name = "height in feet")]
        public int? HeightFt { get; set; }
        [Display(Name = "height in inches")]
        public int? HeightIn { get; set; }
        public double? TDEE { get; set; }
        public double? BMR { get; set; }
        public int? WorkoutAmount { get; set; }
        public double? DailyCalorieIntake { get; set; }
        public double? BMI { get; set; }
        public double? ProteinIntake { get; set; }
        public double? FatIntake { get; set; }
        public double? CarbIntake { get; set; }
        public string Gender { get; set; }
        [ForeignKey("ApplicationUsers")]
        public string LoginId { get; set; }
        public ApplicationUser ApplicationUsers { get; set; }
        [ForeignKey("Goals")]
        public int GoalId { get; set; }
        public Goals Goals { get; set; }
        [ForeignKey("MealPlan")]
        public int MealPlanId { get; set; }
        public MealPlan MealPlan { get; set; }
        public static object Identity { get; internal set; }
    }

}