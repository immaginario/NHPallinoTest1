using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHPallinoTest1.Model;

namespace NHPallinoTest1.NHMappings
{
    public class CustomerMapping: ClassMap<Customer>
    {
        public CustomerMapping()
        {
            Table("Customers");
            Id(x => x.Id);
            Map(x => x.Name).Not.Nullable().Length(255);
            Map(x => x.Surname).Not.Nullable().Length(255);
            Map(x => x.Address).Not.Nullable().Length(255);
            Map(x => x.Created).Not.Nullable();
            HasMany(x => x.ShopOrders).Inverse().Cascade.All();
        }
    }
}
