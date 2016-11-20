using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Database.Contract
{
    public interface IBaseRepository<T> 
    {
        void Add(T entity); 
        void Delete(T entity);
        void Save();
    }
}
