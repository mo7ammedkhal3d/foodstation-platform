using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOODSTATION.Models
{
    public class Advertisement
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string ImgUrl { get; set; }

        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}