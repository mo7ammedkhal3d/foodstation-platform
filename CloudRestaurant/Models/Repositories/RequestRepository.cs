using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CloudRestaurant.Models.Repositories
{
    public class RequestRepository : ICloudRestaurantRepository<Request>
    {
        private readonly ApplicationDbContext db;

        public RequestRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void Add(Request request)
        {
            db.Requests.Add(request);
            db.SaveChanges();
        }

        public void Delete(int Id)
        {
            var request = Find(Id);
            db.Requests.Remove(request);
        }

        public Request Find(int? Id)
        {
           var request= db.Requests.FirstOrDefault(x => x.Id == Id);
            return request;
        }

        public IList<Request> List()
        {
            var requests = db.Requests.ToList();
            return requests;
        }

        public void Update(Request modifyRequest)
        {
            db.Entry(modifyRequest).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}