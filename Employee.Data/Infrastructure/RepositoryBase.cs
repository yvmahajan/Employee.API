using NHibernate;
using NHibernate.Linq;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Employee.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        #region Properties
        private ISession dataContext;
        protected IDbFactory DbFactory
        {
            get;
            private set;
        }
        protected ISession DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }
        #endregion
        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }
        #region Implementation
        public virtual void Add(T entity)
        {
            DbContext.Save(entity);
        }
        public virtual void Update(T entity)
        {
            DbContext.Update(entity);
        }
        public virtual void Delete(T entity)
        {
            DbContext.Delete(entity);
        }
        public virtual void Delete<TV>(TV id)
        {
            DbContext.Delete(DbContext.Load<T>(id));
        }
        public virtual void Delete(Expression<Func<T, bool>> restriction)
        {
            IEnumerable<T> objects = DbContext.QueryOver<T>().Where(restriction).List();
            foreach (T obj in objects)
                DbContext.Delete(obj);
        }
        public virtual T GetById<K>(K id)
        {
            return DbContext.Get<T>(id);
        }
        public virtual IQueryable<T> GetAll()
        {
            return DbContext.Query<T>();
        }
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> restriction)
        {
            return DbContext.QueryOver<T>().Where(restriction).List();
        }
        public virtual T Get(Expression<Func<T, bool>> restriction)
        {
            return DbContext.QueryOver<T>().Where(restriction).SingleOrDefault<T>();
        }
        #endregion

        public IEnumerable<T> ExecuteSQLQuery(string SQLQuery)
        {
            IEnumerable<T> records = DbContext.CreateSQLQuery(SQLQuery)
                               .AddEntity(typeof(T)).List<T>();
            return records;
        }
        public IList<T> ExecuteStoredProcedure(string SPCommond, Dictionary<string, string> Parameters)
        {
            ISQLQuery spSQLQuery = DbContext.CreateSQLQuery(SPCommond);
            IQuery spQuery = spSQLQuery.SetResultTransformer(Transformers.AliasToBean<T>());
            foreach (var key in Parameters)
            {
                spQuery = spQuery.SetParameter(key.Key, key.Value);
            }
            IList<T> records = spQuery.List<T>();

            return records;
        }
    }
}
