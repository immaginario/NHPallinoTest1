using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHPallinoTest1.Model
{
    public class ShopOrderItem
    {
        public virtual int Id { get; set; }
        public virtual string ProductName { get; set; }
        public virtual decimal Price { get; set; }
        public virtual int Quantity { get; set; }
        public virtual ShopOrder ShopOrder { get; set; }

        public ShopOrderItem()
        {
            this.ProductName = string.Empty;
        }
    }
}
