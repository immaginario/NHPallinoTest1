using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Testing;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHPallinoTest1.Model;
using NUnit.Framework;


namespace NHPallinoTest1.Test
{
    [TestFixture]
    public class MappingTests
    {
        [Test]
        public void CanCorrectlyMapCustomer()
        {
            using (ISession session = NHHelper.GetInMemorySession())
            {
                new PersistenceSpecification<Customer>(session, new CustomEqualityComparer())
                    .CheckProperty(c => c.Id, 1)
                    .CheckProperty(c => c.Address, "via Roma, 1")
                    .CheckProperty(c => c.Created, new DateTime(2012,1,2))
                    .CheckProperty(c => c.Name, "Mario")
                    .CheckProperty(c => c.Surname, "Rossi")
                    .VerifyTheMappings();
            }
        }
    }


    public class CustomEqualityComparer : IEqualityComparer
    {
        bool IEqualityComparer.Equals(object x, object y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }
            if (x is DateTime && y is DateTime)
            {
                return ((DateTime)x).ToString() == ((DateTime)y).ToString();
            }
            if (x is DateTime? && y is DateTime?)
            {
                if (((DateTime?)x).HasValue && ((DateTime?)y).HasValue)
                    return ((DateTime?)x).Value == ((DateTime?)y).Value;
                if (!((DateTime?)x).HasValue && !((DateTime?)y).HasValue)
                    return true;
                return false;
            }

            return x.Equals(y);
        }

        public int GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
