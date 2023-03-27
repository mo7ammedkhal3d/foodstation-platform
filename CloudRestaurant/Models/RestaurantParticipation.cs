using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models
{
    public class RestaurantParticipation
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int ParticipationId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual Participation Participation { get; set; }
    }
}