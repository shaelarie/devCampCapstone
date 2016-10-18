using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyTrainer.Models
{
    public class Workouts
    {
        [Key]
        public int Id { get; set; }
        public string WorkoutName { get; set; }
        public string Description { get; set; }
        public int NumberOfDays { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        public double Distance { get; set; }
        public double CaloriesBurned { get; set; }
        public ICollection<Goals> UsersGoal { get; set; }
    }
}