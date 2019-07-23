using System.Collections.Generic;
using System.Linq;

namespace EmployeeMangmentSystem.Repository.Repository.Interfaces
{

    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object Id);
        T Insert(T obj);
        List<T> InsertRange(List<T> obj);
        void Delete(object Id);
        T Update(T obj);
        void Save();
    }

}
