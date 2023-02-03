using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models.Repositories
{
    public class CategoryRepository : ICloudRestaurantRepository<Category>
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Add(Category newcategory)
        {
            db.Categories.Add(newcategory);
            db.SaveChanges();
        }

        public void Delete(int Id)
        {
            var category = Find(Id);

            db.Categories.Remove(category);
            db.SaveChanges();
        }

        public Category Find(int? Id)
        {
            var category = db.Categories.FirstOrDefault(x => x.Id == Id);

            return category;
        }

        public IList<Category> List()
        {
            var Categories = db.Categories.ToList();

            return Categories;
        }

        public void Update(Category modifycategory)
        {
            db.Entry(modifycategory).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}