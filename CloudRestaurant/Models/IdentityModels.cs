﻿using System.Data.Entity;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CloudRestaurant.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<CloudRestaurant.Models.Restaurant> Restaurants { get; set; }

        public System.Data.Entity.DbSet<CloudRestaurant.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<CloudRestaurant.Models.Item> Items { get; set; }

        public System.Data.Entity.DbSet<CloudRestaurant.Models.Request> Requests { get; set; }

        public System.Data.Entity.DbSet<CloudRestaurant.Models.Region> Regions { get; set; }

        public System.Data.Entity.DbSet<CloudRestaurant.Models.Country> Countries { get; set; }

        public System.Data.Entity.DbSet<CloudRestaurant.Models.DiningType> DiningTypes { get; set; }
    }
}