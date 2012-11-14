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
    }
}
