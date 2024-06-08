using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;
using BussinesLayer;
using System.Net;

namespace Projekat2_MTZPP
{
    public partial class ReportForm : Form
    {
        private OrderBL orderBL = new OrderBL();    
        public ReportForm()
        {
            InitializeComponent();
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.SelectionChanged += dgvOrders_SelectionChanged;
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            // Popunjavanje comboBox Employee 💼
            EmployeeBL eBL = new EmployeeBL();
            cmbEmployee.DataSource = eBL.GetEmployees();
            cmbEmployee.DisplayMember = "EmployeeName";
            cmbEmployee.ValueMember = "EmployeeID";

            // Popunjavanje comboBox Client 💁
            ClientBL cBL = new ClientBL();
            cmbClient.DataSource = cBL.GetClient();
            cmbClient.DisplayMember = "ClientName";
            cmbClient.ValueMember = "ClientID";

            // Popunjavanje comboBox Product 📦
            ProductBL pBL = new ProductBL();
            cmbProduct.DataSource = pBL.GetProducts();
            cmbProduct.DisplayMember = "ProductName";
            cmbProduct.ValueMember = "ProductID";
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            // Izvlacim vrednost iz combo-Boxa 🥊 
            int? employeeId = cmbEmployee.SelectedValue as int?;
            int? clientId = cmbClient.SelectedValue as int?;
            int? productId = cmbProduct.SelectedValue as int?;

            List<OrderReportDOM> reportData = orderBL.GetOrderReport(employeeId, clientId, productId);

            dgvOrders.DataSource = reportData;
        }

        private void dgvOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // za svaki slucaj da korisnik ne ubode red 👻
            {
                int orderId = (int)dgvOrders.Rows[e.RowIndex].Cells["OrderID"].Value; // izvlacim order🆔
                EditForm editForm = new EditForm(orderId);
                editForm.ShowDialog();
            }
        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void dgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
