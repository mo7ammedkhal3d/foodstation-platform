using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models
{
    public class RestaurantDiningTypes
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int DiningTypeId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual DiningType DiningType { get; set; }
    }
}