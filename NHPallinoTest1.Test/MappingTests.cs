using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;


namespace NHPallinoTest1.Test
{
    [TestFixture]
    public class MappingTests
    {
        private static void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            var path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                @"script.sql");

            new SchemaExport(config)
                .SetOutputFile(path)
                .Create(true, false);
        }

        [Test]
        public void CanExportSchemaForMSSql()
        {
            Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString("sgdsfgdfgfd").ShowSql())
                .Mappings(x => NHHelper.BuildMappings(x))
                .ExposeConfiguration(BuildSchema)
                .BuildConfiguration();
        }
    }
}
