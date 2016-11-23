using Haircut.Database.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haircut.Database.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        protected DatabaseContext _context;

        public BaseRepository()
        {
            _context = new DatabaseContext();
        }

        public virtual void Add(T entity) 
        {       
                 
            _context.Set(typeof(T)).Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _context.Set(typeof(T)).Remove(entity);
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Update<T1>(T1 entity) where T1 : class
        {
            //_context.Entry
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual T GetById(int id) 
        {
            return (T)_context.Set(typeof(T)).Find(id);
        }
    }
}
