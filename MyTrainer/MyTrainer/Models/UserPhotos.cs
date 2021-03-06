﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyTrainer.Models
{
    public class UserPhotos
    {
        [Key]
        public int Id { get; set; }
        public string PictureDescription { get; set; }
        public DateTime? DateTaken { get; set; }
        public string Picture { get; set; }
        public string FileName { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}