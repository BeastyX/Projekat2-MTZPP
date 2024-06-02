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
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            EmployeeBL eBL = new EmployeeBL();
            cmbEmployee.DataSource = eBL.GetEmployees();
            cmbEmployee.DisplayMember = "EmployeeName";
            cmbEmployee.ValueMember = "EmployeeID";

            ClientBL cBL = new ClientBL();
            cmbClient.DataSource = cBL.GetClient();
            cmbClient.DisplayMember = "ClientName";
            cmbClient.ValueMember = "ClientID";

            ProductBL pBL = new ProductBL();
            cmbProduct.DataSource = pBL.GetProducts();
            cmbProduct.DisplayMember = "ProductName";
            cmbProduct.ValueMember = "ProductID";
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int? employeeId = cmbEmployee.SelectedValue as int?;
            int? clientId = cmbClient.SelectedValue as int?;
            int? productId = cmbProduct.SelectedValue as int?;

            List<OrderReportDOM> reportData = orderBL.GetOrderReport(employeeId, clientId, productId);

            dgvOrders.DataSource = reportData;
        }
    }
}
