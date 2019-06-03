using EmployeeMangmentSystem.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeMangmentSystem.Repository.Repository.Interfaces
{

    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object Id);
        T Insert(T obj);
        void Delete(object Id);
        T Update(T obj);
        void Save();
    }

}
