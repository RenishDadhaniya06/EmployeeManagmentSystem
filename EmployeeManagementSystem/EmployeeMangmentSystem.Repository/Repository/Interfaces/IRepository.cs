
namespace EmployeeMangmentSystem.Repository.Repository.Interfaces
{
    #region Using
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    #endregion

    /// <summary>
    /// IRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        T GetById(object Id);

        /// <summary>
        /// Inserts the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        T Insert(T obj);

        /// <summary>
        /// Inserts the range.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        List<T> InsertRange(List<T> obj);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        void Delete(object Id);

        T Update(T obj);
        /// <summary>
        /// Saves this instance.
        /// </summary>
        void Save();

        /// <summary>
        /// Determines whether this instance contains the object.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified entity]; otherwise, <c>false</c>.
        /// </returns>
        bool Contains(T entity);

        /// <summary>
        /// Deletes the where.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        void DeleteWhere(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Finds the by.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            params Expression<Func<T, object>>[] includes);
    }
}
