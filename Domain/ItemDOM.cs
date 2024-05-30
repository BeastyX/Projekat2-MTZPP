using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ItemDOM
    {
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }
        public int Order_OrderID { get; set; }
        public int Product_ProductID { get; set; }
    }
}
