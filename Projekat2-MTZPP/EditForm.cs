using BussinesLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekat2_MTZPP
{
    public partial class EditForm : Form
    {
        private int _orderId;
        private OrderBL orderBL = new OrderBL();
        private OrderBL itemBL = new OrderBL();
        private EmployeeBL employeeBL = new EmployeeBL();
        private ClientBL clientBL = new ClientBL();
        private ProductBL productBL = new ProductBL();
        public EditForm(int orderId)
        {
            InitializeComponent();
            _orderId = orderId;
            LoadOrderData();
        }

        private void LoadOrderData()
        {
            // Order detalji 📙
            OrderDOM order = orderBL.GetOrderById(_orderId);

            txtOrderID.Text = order.OrderID.ToString();
            txtOrderID.ReadOnly = true;
            lblDate.Text = order.OrderDate.ToString("d");

            // Popunjavanje comboBox Employee 💼
            var employees = employeeBL.GetEmployees();
            cmbEmployee.DataSource = employees;
            cmbEmployee.DisplayMember = "EmployeeName";
            cmbEmployee.ValueMember = "EmployeeID";
            cmbEmployee.SelectedValue = order.Employee_EmployeeID;

            // Popunjavanje comboBox Client 💁
            var clients = clientBL.GetClient();
            cmbClient.DataSource = clients;
            cmbClient.DisplayMember = "ClientName";
            cmbClient.ValueMember = "ClientID";
            cmbClient.SelectedValue = order.Client_ClientID;

            // Load DataGridView with items
            var items = itemBL.GetItemsByOrderId(_orderId);
            dgvOrderItems.DataSource = items;

            // Set DataGridView columns
            dgvOrderItems.Columns["ItemID"].Visible = false;
            dgvOrderItems.Columns["Order_OrderID"].Visible = false;

            // ProductID kolona
            DataGridViewComboBoxColumn cmbProductColumn = new DataGridViewComboBoxColumn();
            cmbProductColumn.DataPropertyName = "Product_ProductID";
            cmbProductColumn.HeaderText = "Product";
            cmbProductColumn.Name = "cmbProduct";

            // Popunjavanje comboBox Product 📦
            var products = productBL.GetProducts();
            cmbProductColumn.DataSource = products;
            cmbProductColumn.DisplayMember = "ProductName";
            cmbProductColumn.ValueMember = "ProductID";

            dgvOrderItems.Columns.Add(cmbProductColumn);

            dgvOrderItems.Columns["ItemPrice"].ReadOnly = false;
            dgvOrderItems.Columns["Quantity"].ReadOnly = false;
            dgvOrderItems.Columns["TotalPrice"].ReadOnly = true;
        }
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            // Update order details
            OrderDOM order = new OrderDOM
            {
                OrderID = _orderId,
                Employee_EmployeeID = (int)cmbEmployee.SelectedValue,
                Client_ClientID = (int)cmbClient.SelectedValue,
                OrderDate = DateTime.Parse(lblDate.Text)
            };
            orderBL.UpdateOrder(order);

            // Update order items
            List<ItemDOM> items = dgvOrderItems.DataSource as List<ItemDOM>;
            itemBL.UpdateItems(items);

            this.Close();
        }
        private void EditForm_Load(object sender, EventArgs e)
        {

        }

    }
}
