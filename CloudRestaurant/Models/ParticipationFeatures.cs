using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models
{
    public class ParticipationFeatures
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public int FarticipationId { get; set; }
        public virtual Feature Feature { get; set; }
        public virtual Participation Participation { get; set; }
    }
}