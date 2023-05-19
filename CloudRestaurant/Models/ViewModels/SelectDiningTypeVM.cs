using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOODSTATION.Models.ViewModels
{
    public class SelectDiningTypeVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Select { get; set; }
    }
}