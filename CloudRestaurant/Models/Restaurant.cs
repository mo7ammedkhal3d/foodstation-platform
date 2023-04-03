﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("اسم المطعم")]
        public string Name { get; set; }

        [DisplayName("وصف المطعم")]
        public string Description { get; set; }

        [DisplayName("صورة المطعم")]
        public string ImgUrl { get; set; }

        [Required]
        [DisplayName("المنطقة")]
        public string RegionId { get; set; }

        public virtual Region Region { get; set; }

        public virtual ICollection<RestaurantDiningTypes> RestaurantDiningTypes { get; set; }

        public virtual ICollection<RestaurantParticipation> RestaurantParticipations { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        
    }
}