using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHPallinoTest1.Model;
using NHPallinoTest1.NHMappings;

namespace NHPallinoTest1.Test
{
    public class NHHelper
    {
        private static readonly string dbFile
            = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "test.sqllite"
                );

        private static void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            if (File.Exists(dbFile)) 
                File.Delete(dbFile);

            new SchemaExport(config)
                .Create(false, true);
        }

        public static Configuration CreateConfiguration()
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile(dbFile).ShowSql())
                .Mappings(x => NHHelper.BuildMappings(x))
                .ExposeConfiguration(BuildSchema)
                .BuildConfiguration();
        }

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
