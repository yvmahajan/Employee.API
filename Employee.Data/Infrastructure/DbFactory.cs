using NHibernate;
using System;
using Employee.Data.DBSession;

namespace Employee.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ISession Init();
    }
    public class DbFactory : Disposable, IDbFactory
    {
        ISession dbContext;

        public ISession Init()
        {
            return dbContext ?? (dbContext = FluentNHibernateHelper.OpenSession().OpenSession());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
