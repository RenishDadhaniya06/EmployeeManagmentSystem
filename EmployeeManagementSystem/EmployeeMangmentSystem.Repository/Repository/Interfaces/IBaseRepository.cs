using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EmployeeMangmentSystem.Repository.Repository.Interfaces
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(bool asNoTracking);
        IQueryable<T> GetAll(bool asNoTracking, bool ignoreInstanceAuthorizationRules);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        IEnumerable<T> Add(IEnumerable<T> entities);
        void Attach(T entity, bool setToChanged);
        void Delete(T entity);
        bool Contains(T entity);
        int Count { get; }
        T GetByIds(object[] keys);
        T GetById(object key);
        void Delete(object key);
        void RemoveRange(IEnumerable<T> entities);
        T Create();
        void DeleteWhere(Expression<Func<T, bool>> predicate);
    }
}
