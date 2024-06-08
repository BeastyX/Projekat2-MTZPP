using DataLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer
{
    public class ReportBL
    {
        //LINQ upit za dobavljanje filtriranih podataka iz base 🌅
        public List<OrderDOM> GetFilteredOrders(int? employeeId, int? clientId, int? productId)
        {
            using (var context = new DataClasses1DataContext())
            {
                var query = context.Orders.AsQueryable(); //AsQueryable - omogucava da se na upit primene dodatni LINK Uslovi 😨

                if (employeeId.HasValue) // != NULL ☁️
                {
                    query = query.Where(o => o.Employee_EmployeeID == employeeId.Value);
                }

                if (clientId.HasValue)
                {
                    query = query.Where(o => o.Client_ClientID == clientId.Value);
                }

                if (productId.HasValue)
                {
                    query = query.Where(o => o.Items.Any(i => i.Product_ProductID == productId.Value));
                }

                var orders = query.ToList(); // Vraca listu porudzbina 🌬️
                return Mapper.MapToDomainList(orders);
            }
        }
    }
}
