using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHPallinoTest1.Model;

namespace NHPallinoTest1.NHMappings
{
    public class ShopMapping: ClassMap<Shop>
    {
        public ShopMapping()
        {
            Table("Shops");
            Id(x => x.Id);
            Map(x => x.Name).Not.Nullable().Length(255);
            HasMany(x => x.ShopOrders).Inverse().Cascade.All();
        }
    }
}
