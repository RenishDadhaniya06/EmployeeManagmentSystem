
namespace EmployeeMangmentSystem.Repository.Repository.Classes
{
    #region Using
    using EmployeeMangmentSystem.Repository.Repository.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    #endregion


    /// <summary>
    /// BaseRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="EmployeeMangmentSystem.Repository.Repository.Interfaces.IBaseRepository{T}" />
    class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        /// <summary>
        /// The entities
        /// </summary>
        private RepositoryContext Entities;


        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected BaseRepository(RepositoryContext context)
        {
            Entities = context as RepositoryContext;
        }

        /// <summary>
        /// Gets the dbset.
        /// </summary>
        /// <value>
        /// The dbset.
        /// </value>
        protected virtual IDbSet<T> Dbset => Entities.Set<T>();

        /// <summary>
        /// Gets the type of the entity.
        /// </summary>
        /// <value>
        /// The type of the entity.
        /// </value>
        public virtual Type EntityType => typeof(T);

        /// <summary>
        /// Gets the by ids.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns></returns>
        public virtual T GetByIds(object[] keys)
        {
            return Dbset.Find(keys);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public virtual T GetById(object key)
        {
            return GetByIds(new object[] { key });
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll()
        {
            return Dbset;

        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="asNoTracking">if set to <c>true</c> [as no tracking].</param>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll(bool asNoTracking)
        {
            if (asNoTracking)
            {
                return Dbset.AsNoTracking();
            }

            return Dbset;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="asNoTracking">if set to <c>true</c> [as no tracking].</param>
        /// <param name="ignoreInstanceAuthorizationRules">if set to <c>true</c> [ignore instance authorization rules].</param>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll(bool asNoTracking, bool ignoreInstanceAuthorizationRules)
        {
            return GetAll(asNoTracking);
        }

        /// <summary>
        /// Finds the by.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return Dbset.Where(predicate);
        }

        /// <summary>
        /// Adds the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public virtual T Add(T obj)
        {
            return Entities.Set<T>().Add(obj);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public virtual T Create()
        {
            return Entities.Set<T>().Create();
        }

        /// <summary>
        /// Adds the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public IEnumerable<T> Add(IEnumerable<T> entities)
        {
            return Entities.Set<T>().AddRange(entities);
        }

        /// <summary>
        /// Attaches the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="setToChanged">if set to <c>true</c> [set to changed].</param>
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

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public virtual void Delete(T item)
        {
            if (item != null)
            {
                Entities.Set<T>().Remove(item);
            }
        }

        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            Entities.Set<T>().RemoveRange(entities);
        }

        /// <summary>
        /// Deletes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public virtual void Delete(object key)
        {
            var entity = GetById(key);

            if (entity != null) Delete(entity);
        }

        /// <summary>
        /// Deletes the where.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            var entities = Dbset.Where(predicate);
            RemoveRange(entities);
        }

        /// <summary>
        /// Determines whether this instance contains the object.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified entity]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(T entity)
        {
            return Dbset.FirstOrDefault(t => t == entity) != null;
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count => Dbset.Count();
    }
}
