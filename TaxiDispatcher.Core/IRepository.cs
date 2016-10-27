using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiDispatcher.Core
{
    public interface IRepository<T> where T : IEntity
    {
        void Add(T entity);
        IEnumerable<T> Get();
        void Update(T entity);
        void Delete(T entity);
    }
}
