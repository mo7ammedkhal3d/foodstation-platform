using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models
{
    public class DiningType
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("النوع")]
        public string Name { get; set; }
        public virtual ICollection<RestaurantDiningTypes> RestaurantDiningTypes  { get; set; }
    }
}