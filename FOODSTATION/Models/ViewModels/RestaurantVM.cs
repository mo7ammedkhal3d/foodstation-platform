using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOODSTATION.Models.ViewModels
{
    public class RestaurantVM
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgUrl { get; set; }

        public int RegionId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public int Participation { get; set; }

        public List<int> diningTypeIds { get; set; }

        public string Region { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public  ICollection<DiningType> DiningTypes { get; set; }
    }
}