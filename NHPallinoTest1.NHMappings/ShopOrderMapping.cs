using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHPallinoTest1.Model;

namespace NHPallinoTest1.NHMappings
{
    public class ShopOrderMapping: ClassMap<ShopOrder>
    {
        public ShopOrderMapping()
        {
            Table("ShopOrders");
            Id(x => x.Id);
            Map(x => x.Created).Not.Nullable();
            References(x => x.Shop).Not.Nullable()
                .ForeignKey("FK_ShopOrders_Shops");
            References(x => x.Customer).Not.Nullable()
                .ForeignKey("FK_ShopOrders_Customers");
            HasMany(x => x.ShopOrderItems).Inverse().Cascade.All();
        }
    }
}
