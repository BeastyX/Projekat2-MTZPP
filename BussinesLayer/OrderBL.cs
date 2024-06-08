using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DataLayer;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Configuration;
using System.Linq.Expressions;
using System.Xml.Linq;
using System.Data.Linq;
using System.Data.Entity;

namespace BussinesLayer
{
    public class OrderBL
    {
        public OrderDOM GetOrderById(int orderId)
        {
            using (var context = new DataClasses1DataContext())
            {
                var order = context.Orders // LINQ upit da se pronadje Order
                    .Include(o => o.Employee)
                    .Include(o => o.Client)
                    .SingleOrDefault(o => o.OrderID == orderId); // po prsoledjenom idu

                return Mapper.MapToDTO(order);
            }
        }

        public void UpdateOrder(OrderDOM order)
        {
            using (var context = new DataClasses1DataContext())
            {
                var entity = context.Orders.SingleOrDefault(o => o.OrderID == order.OrderID); 
                if (entity != null)
                {
                    entity.OrderDate = order.OrderDate;
                    entity.Employee_EmployeeID = order.Employee_EmployeeID;
                    entity.Client_ClientID = order.Client_ClientID;
                    context.SubmitChanges();
                }
            }
        }

        public int InsertOrder(OrderDOM oDOM)
        {
            Order o = Mapper.MapToEntity(oDOM); //mapper prebacuje podatke iz domen objekta u entitet koji radi sa bazom

            using (var context = new DataClasses1DataContext()) //context upravlja sa povezivanjem sa bazom
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

        public List<ItemDOM> GetItemsByOrderId(int orderId)
        {
            using (var context = new DataClasses1DataContext())
            {
                var items = context.Items
                    .Where(i => i.Order_OrderID == orderId)
                    .Select(i => new ItemDOM //Transformise podatkse iz baze podataka u ItemDOM objekte
                    {
                        ItemID = i.ItemID,
                        Quantity = i.Quantity,
                        ItemPrice = i.ItemPrice,
                        Order_OrderID = i.Order_OrderID,
                        Product_ProductID = i.Product_ProductID
                    })
                    .ToList();

                return items;
            }
        }

        public void UpdateItems(List<ItemDOM> items)
        {
            using (var context = new DataClasses1DataContext())
            {
                foreach (var itemDOM in items)
                {
                    var entity = context.Items.SingleOrDefault(i => i.ItemID == itemDOM.ItemID);
                    if (entity != null)
                    {
                        entity.Quantity = itemDOM.Quantity;
                        entity.ItemPrice = itemDOM.ItemPrice;
                        entity.Order_OrderID = itemDOM.Order_OrderID;
                        entity.Product_ProductID = itemDOM.Product_ProductID;
                    }
                }

                context.SubmitChanges();
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

        // Izvestaj o porudzbinama na osnovu opcionalnih filtera za zaposlenog, klijenta i proizvod.
        //Dapper biblioteka - koristi se za izvrsavanje stored procedure
        private string connectionString = ConfigurationManager.ConnectionStrings["MicrosoftProjekat2"].ConnectionString;

        public List<OrderReportDOM> GetOrderReport(int? employeeId = null, int? clientId = null, int? productId = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                //Kreiram i popunjavam parametre
                var parameters = new DynamicParameters();
                parameters.Add("employeeId", employeeId, DbType.Int32);
                parameters.Add("clientId", clientId, DbType.Int32);
                parameters.Add("productId", productId, DbType.Int32);

                // Stored procedure ✔️ Mapiranje rezultata ✔️
                return connection.Query<OrderReportDOM>("OrderReport", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}
