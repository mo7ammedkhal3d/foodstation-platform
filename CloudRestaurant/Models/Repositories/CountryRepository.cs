using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FOODSTATION.Models.Repositories
{
    public class CountryRepository : IFOODSTATIONRepository<Country>
    {
        private readonly ApplicationDbContext db;

        public CountryRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public void Add(Country NewCountry)
        {
            db.Countries.Add(NewCountry);
            db.SaveChanges();
        }

        public void Delete(int Id)
        {
            db.Countries.Remove(Find(Id));
            db.SaveChanges();
        }

        public Country Find(int? Id)
        {
            return db.Countries.FirstOrDefault(c => c.Id == Id);    
        }

        public IList<Country> List()
        {
            return db.Countries.ToList();   
        }

        public void Update(Country modifyCountry)
        {
            db.Entry(modifyCountry).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}