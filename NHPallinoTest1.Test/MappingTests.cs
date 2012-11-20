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
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Transform;
using NHPallinoTest1.Model;
using NUnit.Framework;
using SharpTestsEx;


namespace NHPallinoTest1.Test
{
    [TestFixture]
    public class MappingTests
    {

        private ISessionFactory sessFactory;
        private Configuration config;


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

        private void AddDummyData(ISession session)
        {
            var shopName = "Negozio di Pallino";
            using (ITransaction trans = session.BeginTransaction())
            {
                var shop = new Shop { Name = shopName };

                var customer = new Customer
                {
                    Name = "Mario",
                    Surname = "Rossi",
                    Address = "via Roma, 2"
                };
                try
                {
                    session.Save(customer);
                    session.Save(shop);
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
            using (ITransaction trans = session.BeginTransaction())
            {
                var shop = session.Get<Shop>(1);

                var customer = session.Get<Customer>(1);

                var order = new ShopOrder();
                order.AddItem("Fiesta", 2.30M, 1);
                order.AddItem("Delice", 2.30M, 1);

                shop.AddShopOrder(order, customer);

                try
                {
                    session.Save(shop);
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        [Test]
        public void CanAddNewOrder()
        {
            var shopName = "Negozio di Pallino";
            using (ISession session = NHHelper.GetInMemorySession())
            {
                AddDummyData(session);

                string expectedName = string.Empty;
                var shopOnDb = session.Get<Shop>(1);
                expectedName = shopOnDb.Name;

                shopName.Should().Be.EqualTo(expectedName);
            }
        }

        [Test]
        public void CanQueryTodaysOrders()
        {
            var config = NHHelper.CreateConfiguration();
            var factory = config.BuildSessionFactory();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            using (ISession session = factory.OpenSession())
            {
                AddDummyData(session);
            }
            using (ISession session = factory.OpenSession())
            {
                using (ITransaction trans = session.BeginTransaction())
                {
                    var todaysOrdersQuery = session.QueryOver<ShopOrder>()
                        .Where(x => x.Created >= DateTime.Today)
                        .And(x => x.Created < DateTime.Today.AddDays(1))
                        .JoinQueryOver<ShopOrderItem>(x => x.ShopOrderItems)
                        .TransformUsing(new DistinctRootEntityResultTransformer())
                        .Take(1000);
                    var todaysOrders = todaysOrdersQuery.List<ShopOrder>();

                    todaysOrders.Should().Have.Count.EqualTo(1);
                    var todayOrder = todaysOrders[0];

                    todayOrder.ShopOrderItems.Should().Have.Count.EqualTo(2);
                    trans.Commit();
                }
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
