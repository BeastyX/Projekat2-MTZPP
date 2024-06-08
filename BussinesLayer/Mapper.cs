using DataLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer
{
    // Poenta Mappera je da napravi separaciju izmedju sloja pristupa podacima i sloja poslovne logike
    public static class Mapper
    {
        public static EmployeeDOM MapToDTO(Employee e)
        {
            EmployeeDOM emp = new EmployeeDOM();

            emp.EmployeeID = e.EmployeeID;
            emp.EmployeeName = e.EmployeeName;

            return emp;
        }

        public static Employee MapToEntity(EmployeeDOM e)
        {
            Employee emp = new Employee();

            emp.EmployeeName = e.EmployeeName;

            return emp;
        }

        public static ClientDOM MapToDTO(Client c)
        {
            ClientDOM cDTP = new ClientDOM();

            cDTP.ClientID = c.ClientID;
            cDTP.ClientName = c.ClientName;

            return cDTP;
        }

        public static Order MapToEntity(OrderDOM oDTP)
        {
            Order o = new Order();

            o.OrderID = oDTP.OrderID;
            o.OrderDate = oDTP.OrderDate;
            o.Employee_EmployeeID = oDTP.Employee_EmployeeID;
            o.Client_ClientID = oDTP.Client_ClientID;

            return o;
        }

        public static OrderDOM MapToDTO(Order o)
        {
            OrderDOM oDTP = new OrderDOM();

            oDTP.OrderID = o.OrderID;
            oDTP.OrderDate = o.OrderDate;
            oDTP.Employee_EmployeeID = o.Employee_EmployeeID;
            oDTP.Client_ClientID = o.Client_ClientID;

            return oDTP;
        }

        public static OrderDOM MapToDTOO(Order order)
        {
            return new OrderDOM
            {
                OrderID = order.OrderID,
                OrderDate = order.OrderDate,
                Employee_EmployeeID = order.Employee_EmployeeID,
                Client_ClientID = order.Client_ClientID
            };
        }

        public static ProductDOM MapToDTO(Product p)
        {
            ProductDOM pDTP = new ProductDOM();

            pDTP.ProductID = p.ProductID;
            pDTP.ProductName = p.ProductName;
            pDTP.ProductPrice = p.ProductPrice;

            return pDTP;
        }

        public static Item MapToEntity(ItemDOM oDTP)
        {
            Item od = new Item();

            od.ItemID = oDTP.ItemID;
            od.Quantity = oDTP.Quantity;
            od.ItemPrice = oDTP.ItemPrice;
            od.Order_OrderID = oDTP.Order_OrderID;
            od.Product_ProductID = oDTP.Product_ProductID;

            return od;
        }

        public static ItemDOM MapToDTO(Item i)
        {
            ItemDOM itemDOM = new ItemDOM
            {
                ItemID = i.ItemID,
                Quantity = i.Quantity,
                ItemPrice = i.ItemPrice,
                Order_OrderID = i.Order_OrderID,
                Product_ProductID = i.Product_ProductID
            };
            return itemDOM;
        }

        // New mapping method for OrderDOM to Order
        public static OrderDOM MapToDTO2(Order o)
        {
            OrderDOM oDTP = new OrderDOM
            {
                OrderID = o.OrderID,
                OrderDate = o.OrderDate,
                Employee_EmployeeID = o.Employee_EmployeeID,
                Client_ClientID = o.Client_ClientID,
                Items = o.Items.Select(MapToDTO).ToList()  // Map items
            };
            return oDTP;
        }

        // New mapping method for List<Order> to List<OrderDOM>
        public static List<OrderDOM> MapToDomainList(IEnumerable<Order> orders)
        {
            return orders.Select(MapToDTO2).ToList();
        }

        //Order LIST
        //public static List<OrderDOM> convertToList(ISingleResult<Order> eList)
        //{
        //    List<OrderDOM> l = new List<OrderDOM>();
        //    foreach (Order a in eList)
        //        l.Add(MapToDTO(a));

        //    return l;
        //}
        public static List<EmployeeDOM> convertToList(List<Employee> eList)
        {
            // Konvertovanje liste employees u listu employeeDOM objekata
            List<EmployeeDOM> l = new List<EmployeeDOM>();
            foreach (Employee e in eList)
                l.Add(MapToDTO(e));

            return l;
        }

        public static List<ClientDOM> convertToList(List<Client> eList)
        {
            List<ClientDOM> l = new List<ClientDOM>();
            foreach (Client e in eList)
                l.Add(MapToDTO(e));

            return l;
        }
        public static List<ProductDOM> convertToList(List<Product> eList)
        {
            List<ProductDOM> l = new List<ProductDOM>();
            foreach (Product e in eList)
                l.Add(MapToDTO(e));

            return l;
        }
    }
}
