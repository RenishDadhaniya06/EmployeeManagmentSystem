using EmployeeMangmentSystem.Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EmployeeMangmentSystem.Repository.Repository.Classes
{
    class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private RepositoryContext Entities;
        protected BaseRepository(RepositoryContext context)
        {
            Entities = context as RepositoryContext;
        }

        protected virtual IDbSet<T> Dbset => Entities.Set<T>();

        public virtual Type EntityType => typeof(T);

        public virtual T GetByIds(object[] keys)
        {
            return Dbset.Find(keys);
        }

        public virtual T GetById(object key)
        {
            return GetByIds(new object[] { key });
        }

        public virtual IQueryable<T> GetAll()
        {
            return Dbset;

        }

        public virtual IQueryable<T> GetAll(bool asNoTracking)
        {
            if (asNoTracking)
            {
                return Dbset.AsNoTracking();
            }

            return Dbset;
        }

        public virtual IQueryable<T> GetAll(bool asNoTracking, bool ignoreInstanceAuthorizationRules)
        {
            return GetAll(asNoTracking);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return Dbset.Where(predicate);
        }

        public virtual T Add(T obj)
        {
            return Entities.Set<T>().Add(obj);
        }

        public virtual T Create()
        {
            return Entities.Set<T>().Create();
        }

        public IEnumerable<T> Add(IEnumerable<T> entities)
        {
            return Entities.Set<T>().AddRange(entities);
        }

        public virtual void Attach(T obj, bool setToChanged)
        {
            if (setToChanged)
            {
                Entities.Entry(obj).State = EntityState.Modified;
            }
            else
            {
                Entities.Set<T>().Attach(obj);
            }
        }

        public virtual void Delete(T item)
        {
            if (item != null)
            {
                Entities.Set<T>().Remove(item);
            }
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            Entities.Set<T>().RemoveRange(entities);
        }

        public virtual void Delete(object key)
        {
            var entity = GetById(key);

            if (entity != null) Delete(entity);
        }

        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            var entities = Dbset.Where(predicate);
            RemoveRange(entities);
        }

        public bool Contains(T entity)
        {
            return Dbset.FirstOrDefault(t => t == entity) != null;
        }

        public int Count => Dbset.Count();
    }
}
