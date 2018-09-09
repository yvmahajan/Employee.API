using Employee.Data.DBSession;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Employee.Data.Infrastructure
{
    public class RepositoryBase<T> : IDisposable where T : class 
    {
        #region Properties
        protected ISession _session = null;
        protected ITransaction _transaction = null;

        public RepositoryBase()
        {
            _session = Database.OpenSession();
        }
        public RepositoryBase(ISession session)
        {
            _session = session;
        }
        #endregion

        #region Transaction and Session Management Methods
        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }
        public void CommitTransaction()
        {
            // _transaction will be replaced with a new transaction            // by NHibernate, but we will close to keep a consistent state.
            _transaction.Commit();
            CloseTransaction();
        }
        public void RollbackTransaction()
        {
            // _session must be closed and disposed after a transaction            // rollback to keep a consistent state.
            _transaction.Rollback();
            CloseTransaction();
            CloseSession();
        }
        private void CloseTransaction()
        {
            _transaction.Dispose();
            _transaction = null;
        }
        private void CloseSession()
        {
            _session.Close();
            _session.Dispose();
            _session = null;
        }
        #endregion

        #region IRepository Members
        public virtual void Add(T entity)
        {
            _session.Save(entity);
        }
        public virtual void Update(T entity)
        {
            _session.Update(entity);
        }
        public virtual void Delete(T entity)
        {
            _session.Delete(entity);
        }
        public virtual void Delete<TV>(TV id)
        {
            _session.Delete(_session.Load<T>(id));
        }
        public virtual void Delete(Expression<Func<T, bool>> restriction)
        {
            IEnumerable<T> objects = _session.QueryOver<T>().Where(restriction).List();
            foreach (T obj in objects)
                _session.Delete(obj);
        }
        public virtual T GetById<K>(K id)
        {
            return _session.Get<T>(id);
        }
        public virtual IQueryable<T> GetAll()
        {
            return _session.Query<T>();
        }
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> restriction)
        {
            return _session.QueryOver<T>().Where(restriction).List();
        }
        public virtual T Get(Expression<Func<T, bool>> restriction)
        {
            return _session.QueryOver<T>().Where(restriction).SingleOrDefault<T>();
        }
        public IEnumerable<T> ExecuteSQLQuery(string SQLQuery)
        {
            IEnumerable<T> records = _session.CreateSQLQuery(SQLQuery)
                               .AddEntity(typeof(T)).List<T>();
            return records;
        }
        public IList<T> ExecuteStoredProcedure(string SPCommond, Dictionary<string, string> Parameters)
        {
            ISQLQuery spSQLQuery = _session.CreateSQLQuery(SPCommond);
            IQuery spQuery = spSQLQuery.SetResultTransformer(Transformers.AliasToBean<T>());
            foreach (var key in Parameters)
            {
                spQuery = spQuery.SetParameter(key.Key, key.Value);
            }
            IList<T> records = spQuery.List<T>();

            return records;
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            if (_transaction != null)
            {
                // Commit transaction by default, unless user explicitly rolls it back.
                // To rollback transaction by default, unless user explicitly commits,                // comment out the line below.
                CommitTransaction();
            }
            if (_session != null)
            {
                _session.Flush(); // commit session transactions
                CloseSession();
            }
        }
        #endregion
    }
}
