using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FOODSTATION.Models
{
    public class Item
    {
        public Item()
        {
            this.Bills = new HashSet<BillItems>();
        }
        public int Id { get; set; }

        [Required]
        [DisplayName("أسم العنصر")]
        public string Name { get; set; }

        [Required]
        [DisplayName("السعر")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("وقت التحظير")]
        public TimeSpan TimeOfDone { get; set; }

        [Required]
        [DisplayName("نوع الصنف")]
        public int CategoryId { get; set; }


        [DisplayName("الصورة")]
        public string ImgUrl { get; set; }

        [Required]
        [DisplayName("مقدمة من")]
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<BillItems> Bills { get; set; }

    }
}