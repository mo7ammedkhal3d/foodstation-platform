using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FOODSTATION.Models.Repositories
{
    public class RestaurantRepository : IFOODSTATIONRepository<Restaurant>
    {
        private readonly ApplicationDbContext db;

        public RestaurantRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

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
            return db.Restaurants.ToList();
        }

        public void Update(Restaurant modifyRestaurant)
        {
            db.Entry(modifyRestaurant).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}