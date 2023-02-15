using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models
{
    public class Request
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public DateTime DateTime { get; set; }
        public virtual Item Item { get; set; }
    }
}