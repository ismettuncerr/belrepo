using Bel.DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private beldatabaseEntities dbContext;
        private IDbSet<T> entities;
        public GenericRepository()
        {
            dbContext = new beldatabaseEntities();
            entities = dbContext.Set<T>();

        }
        public void Add(T entity)
        {
            entities.Add(entity);
        }

        public void Delete(int id)
        {
            var result = entities.Find(id);
            entities.Remove(result);
        }

        //public void Edit(T entity)
        //{
        //    dbContext.Entry(entity).State = EntityState.Modified;
        //}

        public T Get(int id)
        {
            return entities.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities;
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
