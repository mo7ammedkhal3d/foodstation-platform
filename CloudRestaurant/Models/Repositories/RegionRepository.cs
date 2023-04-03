using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models.Repositories
{
    public class RegionRepository : ICloudRestaurantRepository<Region>
    {
        private readonly ApplicationDbContext db;

        public RegionRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public void Add(Region newRegion)
        {
            db.Regions.Add(newRegion);
            db.SaveChanges();
        }

        public void Delete(int Id)
        {
            var region = Find(Id);

            db.Regions.Remove(region);
            db.SaveChanges();
        }

        public Region Find(int? Id)
        {
            var region = db.Regions.FirstOrDefault(x => x.Id == Id);

            return region;  
        }

        public IList<Region> List()
        {
            return db.Regions.ToList(); 
        }

        public void Update(Region modifyRegion)
        {
            db.Entry(modifyRegion).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}