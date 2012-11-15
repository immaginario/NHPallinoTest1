using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHPallinoTest1.Model
{
    public class ShopOrder
    {
        public virtual int Id { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual ICollection<ShopOrderItem> ShopOrderItems { get; set; }
        public virtual Shop Shop { get; set; }

        public ShopOrder()
        {
            this.Created = DateTime.Now;
            this.ShopOrderItems = new List<ShopOrderItem>();
        }

        public virtual void AddItem(ShopOrderItem shopOrderItem)
        {
            this.ShopOrderItems.Add(shopOrderItem);
            shopOrderItem.ShopOrder = this;
        }

        public virtual void AddItem(string productName, decimal price, int quantity)
        {
            if (price < 0)
                throw new ArgumentException("Price can't be negative");
            var shopOrderItem = new ShopOrderItem { ProductName = productName, Price = price, Quantity = quantity };

            this.AddItem(shopOrderItem);
        }
    }
}
