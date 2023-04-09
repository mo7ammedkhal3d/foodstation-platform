using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models
{
    public class Restaurant
    {
        public Restaurant()
        {
            this.DiningTypes = new HashSet<DiningType>();
            this.Participations = new HashSet<Participation>();
        }

        public int Id { get; set; }

        [Required]
        [DisplayName("اسم المطعم")]
        public string Name { get; set; }

        [DisplayName("وصف المطعم")]
        public string Description { get; set; }

        [DisplayName("صورة المطعم")]
        public string ImgUrl { get; set; }

        [Required]
        [DisplayName("المنطقة")]
        public int RegionId { get; set; }

        public virtual Region Region { get; set; }

        public virtual ICollection<DiningType> DiningTypes { get; set; }

        public virtual ICollection<Participation> Participations { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        
    }
}