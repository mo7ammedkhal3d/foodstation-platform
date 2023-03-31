using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models
{
    public class Country
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("اسم الدولة")]
        public string Name { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
    }
}