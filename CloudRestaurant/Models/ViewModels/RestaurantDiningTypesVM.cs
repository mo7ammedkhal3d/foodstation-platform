using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models.ViewModels
{
    public class RestaurantDiningTypesVM 
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgUrl { get; set; }

        public int RegionId { get; set; }

        public virtual Region Region { get; set; }

        public IEnumerable<DiningType>  diningTypes  { get; set; }
    }
}