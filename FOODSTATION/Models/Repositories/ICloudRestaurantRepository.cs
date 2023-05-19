using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOODSTATION.Models.Repositories
{
    public interface IFOODSTATIONRepository<TEntity>
    {
        IList<TEntity> List(); 

        TEntity Find(int? Id);  

        void Add(TEntity entity); 

        void Update(TEntity entity); 

        void Delete(int Id);

        void Dispose();

    }
}
