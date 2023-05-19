using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOODSTATION.Models.ViewModels
{
    public class ItemVM
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string TimeOfDone { get; set; }

        public string ImgUrl { get; set; }

        public int CategoryId { get; set; }

        public int RestaurantId { get; set; }

        public string Restaurant { get; set; }

        public string Category { get; set; }
    }
}