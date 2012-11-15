using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHPallinoTest1.Model;
using NHPallinoTest1.NHMappings;

namespace NHPallinoTest1.Test
{
    public class NHHelper
    {
        public static FluentMappingsContainer BuildMappings(MappingConfiguration mappingConfiguration)
        {
            var mapping = mappingConfiguration.FluentMappings
                            .AddFromAssemblyOf<ShopMapping>();
            return mapping;
        }

        private static readonly FluentConfiguration config = Fluently.Configure()
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ShopMapping>())
                .Database(SQLiteConfiguration.Standard.InMemory().ShowSql());
        
        public static ISession GetInMemorySession()
        {
            var sessionFactory = config.BuildSessionFactory();
            var session = sessionFactory.OpenSession();
            new SchemaExport(config.BuildConfiguration()).Execute(false, true, false, session.Connection, null);
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            return session;
        }
    }
}
