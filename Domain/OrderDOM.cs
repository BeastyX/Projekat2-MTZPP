using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class OrderDOM
    {
        public int OrderID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public int Employee_EmployeeID { get; set; }
        public int Client_ClientID { get; set; }
    }
}
