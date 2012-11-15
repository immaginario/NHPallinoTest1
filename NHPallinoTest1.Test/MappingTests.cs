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
using SharpTestsEx;


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
                    .CheckProperty(c => c.Created, new DateTime(2012, 1, 2))
                    .CheckProperty(c => c.Name, "Mario")
                    .CheckProperty(c => c.Surname, "Rossi")
                    .VerifyTheMappings();
            }
        }

        [Test]
        public void CanCorrectlyMapShop()
        {
            using (ISession session = NHHelper.GetInMemorySession())
            {
                new PersistenceSpecification<Shop>(session, new CustomEqualityComparer())
                    .CheckProperty(c => c.Id, 1)
                    .CheckProperty(c => c.Name, "Negozio di Padova")
                    .VerifyTheMappings();
            }
        }

        [Test]
        public void CanAddNewOrder()
        {
            var shopName = "Negozio di Pallino";
            using (ISession session = NHHelper.GetInMemorySession())
            {
                using (ITransaction trans = session.BeginTransaction())
                {
                    var shop = new Shop { Name =  shopName};
                    var customer = new Customer { 
                                        Name = "Mario",
                                        Surname = "Rossi",
                                        Address = "via Roma, 2"                                        
                                    };
                    
                    try
                    {
                        session.Save(customer);
                        session.Save(shop);
                        Console.Write(shop.Id);
                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
                string expectedName = string.Empty;
                using (ITransaction trans = session.BeginTransaction())
                {
                    try
                    {
                        var shopOnDb = session.Get<Shop>(1);
                        expectedName = shopOnDb.Name;
                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
                shopName.Should().Be.EqualTo(expectedName);
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
