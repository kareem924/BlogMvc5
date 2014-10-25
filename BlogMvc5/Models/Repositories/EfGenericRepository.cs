using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BlogMvc5.Models.Repositories
{
    public class EfGenericRepository<T> : IGenericRepository<T> where T : class
    {
        DbContext _db;
        IDbSet<T> entity;
        public EfGenericRepository(DbContext db)
        {
            this._db = db;
            entity = this._db.Set<T>();
        }
        public IQueryable<T> List(Expression<Func<T,bool>> filter =null )
        {
            if (filter !=null)
            {
                return entity.Where(filter);
            }
            return entity;
        }

        public T Find(int id)
        {
            return entity.Find(id);
        }

        public void Add(T entityToAdd)
        {
            entity.Add(entityToAdd);
        }

        public void Edit(int id, T entitToUpdate)
        {
            entity.Attach(entitToUpdate);
            _db.Entry<T>(entitToUpdate).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var selectedEntity = entity.Find(id);
            _db.Entry<T>(selectedEntity).State = EntityState.Deleted;
        }

      
    }
}