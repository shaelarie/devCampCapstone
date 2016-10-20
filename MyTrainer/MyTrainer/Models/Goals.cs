using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyTrainer.Models
{
    public class Goals
    {
        [Key]
        public int Id { get; set; }
        public string UserGoal { get; set; }
        public ICollection<Workouts> UsersWorkouts { get; set; }
    }
}