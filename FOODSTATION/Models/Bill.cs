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
            this.Items = new HashSet<BillItems>();
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public int DiningTypeId { get; set; }
        public string Location { get; set; }

        public virtual DiningType DiningType { get; set; }
        public virtual ICollection<BillItems> Items { get; set; }
        public virtual ApplicationUser User { get; set; }


    }
}