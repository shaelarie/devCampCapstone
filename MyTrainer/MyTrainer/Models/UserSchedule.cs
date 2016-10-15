using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyTrainer.Models
{
    [Serializable]
    public class UserSchedule
    {
        [Key]
        public int Id { get; set; }
        public string eventId { get; set; }
        public string title { get; set; }
        public string eventDescription { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string background { get; set; }
        public bool editable { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public string data;
        public List<UserSchedule> ScheduleList;
        public UserSchedule()
        {
            this.Id = Id;
            this.title = title;
            this.eventDescription = eventDescription;
            this.startDate = startDate;
            this.endDate = endDate;
        }
    }
}