using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models.Repositories
{
    public class DiningTypeRepository : ICloudRestaurantRepository<DiningType>
    {
        private readonly ApplicationDbContext db;
        public DiningTypeRepository(ApplicationDbContext _db)
        {
            db= _db;    
        }
        public void Add(DiningType newDiningType)
        {
            db.DiningTypes.Add(newDiningType);
            db.SaveChanges();
        }

        public void Delete(int Id)
        {
            db.DiningTypes.Remove(Find(Id));
            db.SaveChanges();
        }

        public DiningType Find(int? Id)
        {
            return db.DiningTypes.FirstOrDefault(x => x.Id == Id);
        }

        public IList<DiningType> List()
        {
            return db.DiningTypes.ToList();
        }

        public void Update(DiningType modifyDiningType)
        {
            db.Entry(modifyDiningType).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}