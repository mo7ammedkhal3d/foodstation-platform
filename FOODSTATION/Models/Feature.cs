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
        [DisplayName(" وصف الميزة")]
        public string  Description{ get; set; }

        public virtual ICollection<Participation> Participations{ get; set;}
    }
}