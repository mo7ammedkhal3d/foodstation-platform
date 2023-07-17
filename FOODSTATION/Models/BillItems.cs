using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FOODSTATION.Models
{
    public class BillItems
    {
            public int Id { get; set; }
            public int ItemId { get; set; }
            public int BillId { get; set; }
            public int Quantity { get; set; }

            public virtual Item Item { get; set; }
            public virtual Bill Bill { get; set; }

    }
}