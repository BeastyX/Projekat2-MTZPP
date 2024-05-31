using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DataLayer;

namespace BussinesLayer
{
    public class OrderBL
    {
        public int InsertOrder(OrderDOM oDOM)
        {
            Order o = Mapper.MapToEntity(oDOM);

            using (var context = new DataClasses1DataContext())
            {
                context.Orders.InsertOnSubmit(o);
                context.SubmitChanges();
                return o.OrderID; // Return the generated OrderID
            }
        }

        public void DeleteOrder(int orderId)
        {
            using (var context = new DataClasses1DataContext())
            {
                var order = context.Orders.SingleOrDefault(o => o.OrderID == orderId);
                if (order != null)
                {
                    context.Orders.DeleteOnSubmit(order);
                    context.SubmitChanges();
                }
            }
        }

        // Method to insert a new item
        public void InsertItem(ItemDOM iDOM)
        {
            Item i = Mapper.MapToEntity(iDOM);

            using (var context = new DataClasses1DataContext())
            {
                context.Items.InsertOnSubmit(i);
                context.SubmitChanges();
            }
        }

        // Method to delete an item
        public void DeleteItem(int itemId)
        {
            using (var context = new DataClasses1DataContext())
            {
                var item = context.Items.SingleOrDefault(i => i.ItemID == itemId);
                if (item != null)
                {
                    context.Items.DeleteOnSubmit(item);
                    context.SubmitChanges();
                }
            }
        }
    }
}
