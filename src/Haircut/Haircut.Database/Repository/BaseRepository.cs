using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Database.Repository
{
    public abstract class BaseRepository<T> where T : class
    {
        protected DatabaseContext _context;

        public BaseRepository()
        {
            _context = new DatabaseContext();
        }

        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }
    }
}
