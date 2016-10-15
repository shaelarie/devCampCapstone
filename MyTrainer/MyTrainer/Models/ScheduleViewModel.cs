using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTrainer.Models
{
    public class ScheduleViewModel
    {
        public int UserId { get; set; }
        public IEnumerable<UserSchedule> Schedules { get; set; }
        
    }
}