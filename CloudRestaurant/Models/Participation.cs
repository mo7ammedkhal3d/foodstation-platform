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
        public Participation()
        {
            this.Restaurants = new HashSet<Restaurant>();
            this.Features = new HashSet<Feature>();
        }

        public int Id { get; set; }

        [Required]
        [DisplayName("نوع الإشتراك")]
        public string Name { get; set; }

        public virtual ICollection<Restaurant> Restaurants { get; set; }

        public virtual ICollection<Feature> Features { get; set; }
    }
}