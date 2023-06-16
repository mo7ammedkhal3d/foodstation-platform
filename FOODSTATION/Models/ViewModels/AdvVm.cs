using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOODSTATION.Models.ViewModels
{
    public class AdvertisementResponceViewModel
    {
        public bool Seccess { get; set; }

        public string Message { get; set; }

        public int Remaing { get; set; }
    }

    public class EditAdvertisementViewModel
    {
        public string Description { get; set; }

        public int RestaurantId { get; set; }

        public string ImgUrl { get; set; }
    }

}