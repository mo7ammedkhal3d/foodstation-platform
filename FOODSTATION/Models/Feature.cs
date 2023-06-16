using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FOODSTATION.Models
{
    public class Feature
    {
        public Feature()
        {
            this.Participations = new HashSet<Participation>();
        }

        public int Id { get; set; }

        [Required]
        [DisplayName("الميزة")]
        public string  Name{ get; set; }

        [Required]
        [DisplayName("العدد")]
        public int Numb { get; set; }

        public virtual ICollection<Participation> Participations{ get; set;}
    }
}