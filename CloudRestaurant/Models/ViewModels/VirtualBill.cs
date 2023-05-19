using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOODSTATION.Models.ViewModels
{
    public class VirtualBill
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public int ItemQuantity { get; set; }
    }
}