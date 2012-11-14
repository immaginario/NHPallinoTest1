using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHPallinoTest1.Model;

namespace NHPallinoTest1.NHMappings
{
    public class ShopOrderItemMapping: ClassMap<ShopOrderItem>
    {
        public ShopOrderItemMapping()
        {
            Table("ShopOrderItems");
            Id(x => x.Id);
            Map(x => x.Price).Not.Nullable();
            Map(x => x.ProductName).Not.Nullable().Length(255);
            Map(x => x.Quantity).Not.Nullable();
            References(x => x.ShopOrder).Not.Nullable()
                .ForeignKey("FK_ShopOrderItems_ShopOrders");
        }
    }
}
