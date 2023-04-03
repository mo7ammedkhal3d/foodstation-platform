﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models
{
    public class Feature
    {
        public int Id { get; set; }
        [Required]
        [DisplayName(" وصف الميزة")]
        public string  Description{ get; set; }
        public virtual ICollection<ParticipationFeatures> ParticipationFeatures { get; set;}
    }
}