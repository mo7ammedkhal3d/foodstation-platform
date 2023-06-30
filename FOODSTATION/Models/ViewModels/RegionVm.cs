using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOODSTATION.Models.ViewModels
{
    public class RegionVm
    {
        public string Name { get; set; }

        public int CountryId { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }
    }
}