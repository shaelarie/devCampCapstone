using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyTrainer.Models
{
    public class MealPlan
    {
        [Key]
        public int Id { get; set; }
        public bool Basic { get; set; }
        public bool Vegetarian { get; set; }
        public bool Vegan { get; set; }
    }
}