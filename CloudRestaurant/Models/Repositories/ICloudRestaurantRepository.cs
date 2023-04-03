using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudRestaurant.Models.Repositories
{
    public interface ICloudRestaurantRepository<TEntity>
    {
        IList<TEntity> List(); 

        TEntity Find(int? Id);  

        void Add(TEntity entity); 

        void Update(TEntity entity); 

        void Delete(int Id);

        void Dispose();

    }
}
