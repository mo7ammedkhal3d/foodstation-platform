using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOODSTATION.Models
{
    public class Bill
    {
        public Bill()
        {
            this.Items = new HashSet<Item>();
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        public virtual ApplicationUser User { get; set; }


    }
}