using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace Employee.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        // Marks an entity as new
        void Add(T entity);
        // Marks an entity as modified
        void Update(T entity);
        // Marks an entity to be removed
        void Delete(T entity);
        void Delete<K>(K id);
        void Delete(Expression<Func<T, bool>> where);
        // Get an entity by int id
        T GetById<K>(K id);
        // Get an entity using delegate
        T Get(Expression<Func<T, bool>> where);
        // Gets all entities of type T
        IQueryable<T> GetAll();
        // Gets entities using delegate
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        IEnumerable<T> ExecuteSQLQuery(string SQLQuery);
        IList<T> ExecuteStoredProcedure(string SPCommond, Dictionary<string, string> Parameters);
    }
}
