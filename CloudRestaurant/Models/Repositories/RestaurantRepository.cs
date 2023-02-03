using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models.Repositories
{
    public class RestaurantRepository : ICloudRestaurantRepository<Restaurant>
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Add(Restaurant newrestaurant)
        {
            db.Restaurants.Add(newrestaurant);
            db.SaveChanges();   
        }

        public void Delete(int Id)
        {
            var restaurant = Find(Id);

            db.Restaurants.Remove(restaurant);
            db.SaveChanges();
        }

        public Restaurant Find(int? Id)
        {
            var restaurant= db.Restaurants.FirstOrDefault(x => x.Id == Id);

            return restaurant;
        }

        public IList<Restaurant> List()
        {
            var restaurants = db.Restaurants.ToList();

            return restaurants;
        }

        public void Update(Restaurant modifyrestaurant)
        {
            db.Entry(modifyrestaurant).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}