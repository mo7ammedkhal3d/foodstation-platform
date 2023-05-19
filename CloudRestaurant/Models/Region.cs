using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FOODSTATION.Models
{
    public class Region
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("اسم المنطقة")]
        public string Name { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}