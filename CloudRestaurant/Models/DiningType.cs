using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models
{
    public class DiningType
    {
        public DiningType()
        {
            this.Restaurants = new HashSet<Restaurant>();
        }

        public int Id { get; set; }

        [Required]
        [DisplayName("النوع")]
        public string Name { get; set; }

        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}