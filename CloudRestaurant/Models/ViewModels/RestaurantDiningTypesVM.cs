using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models.ViewModels
{
    public class RestaurantDiningTypesVM 
    {
        [Required]
        [DisplayName("اسم المطعم")]
        public string Name { get; set; }

        [DisplayName("وصف المطعم")]
        public string Description { get; set; }

        [DisplayName("صورة المطعم")]
        public string ImgUrl { get; set; }

        [Required]
        [DisplayName("المنطقة")]
        public int RegionId { get; set; }

        public IEnumerable<DiningType>  diningTypes  { get; set; }
    }
}