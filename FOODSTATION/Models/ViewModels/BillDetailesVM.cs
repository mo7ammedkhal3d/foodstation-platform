using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOODSTATION.Models.ViewModels
{
    public class BillDetailesVM
    {
        public string UserName { get; set; }

        public string UserPhoneNumber { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public string DiningType { get; set; }

        public ICollection<ItemDetailesVM> ItemDetails { get; set; }
    }
}