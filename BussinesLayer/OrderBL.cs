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

        private string connectionString = ConfigurationManager.ConnectionStrings["MicrosoftProjekat2"].ConnectionString;

        public List<OrderReportDOM> GetOrderReport(int? employeeId = null, int? clientId = null, int? productId = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("employeeId", employeeId, DbType.Int32);
                parameters.Add("clientId", clientId, DbType.Int32);
                parameters.Add("productId", productId, DbType.Int32);

                return connection.Query<OrderReportDOM>("GetOrderReport", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}
