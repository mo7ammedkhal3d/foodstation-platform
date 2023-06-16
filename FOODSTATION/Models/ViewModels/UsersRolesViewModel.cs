using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOODSTATION.Models.ViewModels
{
    public class UsersRolesViewModel
    {
        public List<ApplicationUser> Users { set; get; }
        public List<IdentityRole> Roles { set; get; }
    }
}