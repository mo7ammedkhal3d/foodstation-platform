using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models.Repositories
{
    public class ItemRepository : ICloudRestaurantRepository<Item>
    {
        private readonly ApplicationDbContext db;

        public ItemRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public void Add(Item newItem)
        {
            db.Items.Add(newItem);
            db.SaveChanges();
        }

        public void Delete(int Id)
        {
            db.Items.Remove(Find(Id));
            db.SaveChanges();
        }

        public Item Find(int? Id)
        {
            var item = db.Items.FirstOrDefault(x => x.Id == Id);    
            return item;               
        }

        public IList<Item> List()
        {
            return db.Items.ToList();
        }

        public void Update(Item modifyItem)
        {
            db.Entry(modifyItem).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}