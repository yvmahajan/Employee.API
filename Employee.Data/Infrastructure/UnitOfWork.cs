using NHibernate;
using System;

namespace Employee.Data.Infrastructure
{
    public class UnitOfWork : Disposable, IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private ITransaction _transaction;
        private ISession dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public ISession DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }
        public void Evict<T>(T obj)
        {
            dbContext.Evict(obj);
        }
        public void BeginTransaction()
        {
            _transaction = DbContext.BeginTransaction();
        }
        public void Commit()
        {
            try
            {
                // commit transaction if there is one active
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Commit();
            }
            catch
            {
                // rollback if there was an exception
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();

                throw;
            }
            finally
            {
                DbContext.Dispose();
            }
        }
        public void Rollback()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        void Evict<T>(T obj);
    }
}