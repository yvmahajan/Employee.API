using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate;
using FluentNHibernate.Mapping;
using System.Configuration;
using Employee.Entities;

namespace Employee.Data.DBSession
{
    public static class Database
    {
        private static ISessionFactory _sessionFactory;
        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString; }
        }
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = Fluently.Configure()
                        .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(ConnectionString).ShowSql()
                    )
            .Mappings(m => m.FluentMappings
                .Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never())
                .AddFromNamespaceOf<EmployeeInfo>())
            .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, false))
            .BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }
        
        public static FluentMappingsContainer AddFromNamespaceOf<T>(
            this FluentMappingsContainer fmc)
        {
            string ns = typeof(T).Namespace;
            IEnumerable<Type> types = typeof(T).Assembly.GetExportedTypes()
                .Where(t => t.Namespace == ns)
                .Where(x => IsMappingOf<IMappingProvider>(x) ||
                            IsMappingOf<IIndeterminateSubclassMappingProvider>(x) ||
                            IsMappingOf<IExternalComponentMappingProvider>(x) ||
                            IsMappingOf<IFilterDefinition>(x));

            foreach (Type t in types)
            {
                fmc.Add(t);
            }

            return fmc;
        }
        private static bool IsMappingOf<T>(Type type)
        {
            return !type.IsGenericType && typeof(T).IsAssignableFrom(type);
        }
    }
}
