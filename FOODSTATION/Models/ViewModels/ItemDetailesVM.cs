using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOODSTATION.Models.ViewModels
{
    public class ItemDetailesVM
    {
        public string ItemName { get; set; }

        public string RestaurantName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}