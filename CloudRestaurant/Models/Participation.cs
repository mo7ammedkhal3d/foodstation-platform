using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models
{
    public class Participation
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("نوع الإشتراك")]
        public string Name { get; set; }
        public virtual ICollection<RestaurantParticipation> RestaurantParticipations { get; set; }
        public virtual ICollection<ParticipationFeatures> ParticipationFeatures { get; set; }
    }
}