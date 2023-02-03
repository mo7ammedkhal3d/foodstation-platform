﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models
{
    public class Category
    {
        public int Id { get; set; }

        [DisplayName("اسم العنصر")]
        public string Name { get; set; }


        [DisplayName("الصورة")]
        public string ImgUrl { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}