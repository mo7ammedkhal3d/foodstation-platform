using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models.ViewModels
{
    public class RestaurantVM
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgUrl { get; set; }

        public int RegionId { get; set; }

        public string Region { get; set; }
    }
}