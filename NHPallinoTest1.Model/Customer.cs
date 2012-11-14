using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHPallinoTest1.Model
{
    public class Customer
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual string Address { get; set; }
        public virtual ICollection<ShopOrder> ShopOrders { get; set; }

        public Customer()
        {
            this.Name = string.Empty;
            this.Surname = string.Empty;
            this.Address = string.Empty;
            this.ShopOrders = new List<ShopOrder>();
            this.Created = DateTime.Now;
        }
    }
}
