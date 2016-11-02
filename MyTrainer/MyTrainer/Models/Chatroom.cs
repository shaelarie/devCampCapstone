using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyTrainer.Models
{
    public class Chatroom
    {
        [Key]
        public int Id { get; set; }
        public string messages { get; set; }
        [ForeignKey("User")]
        public int userId { get; set; }
        public User User { get; set; }
    }
}