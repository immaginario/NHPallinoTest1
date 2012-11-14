using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHPallinoTest1.Model
{
    public class Shop
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ICollection<ShopOrder> ShopOrders { get; set; }

        public Shop()
        {
            this.ShopOrders = new List<ShopOrder>();
        }

        public virtual void AddShopOrder(ShopOrder shopOrder, Customer customer)
        {
            shopOrder.Shop = this;
            shopOrder.Customer = customer;
            customer.ShopOrders.Add(shopOrder);
            this.ShopOrders.Add(shopOrder);
        }
    }
}
