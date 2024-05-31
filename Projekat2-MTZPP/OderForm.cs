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

namespace Projekat2_MTZPP
{
    public partial class OderForm : Form
    {
        public OderForm()
        {
            InitializeComponent();
        }

        private void OderForm_Load(object sender, EventArgs e)
        {
            EmployeeBL eBL = new EmployeeBL();
            cmbEmployee.DataSource = eBL.GetEmployees();
            cmbEmployee.DisplayMember = "EmployeeName";
            cmbEmployee.ValueMember = "EmployeeID";

            ClientBL cBL = new ClientBL();
            cmbCustomer.DataSource = cBL.GetCustomers();
            cmbCustomer.DisplayMember = "ClientName";
            cmbCustomer.ValueMember = "ClientID";

            ProductBL pBL = new ProductBL();
            cmbProduct.DataSource = pBL.GetProducts();
            cmbProduct.DisplayMember = "ProductName";
            cmbProduct.ValueMember = "ProductID";

            lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            cmbProduct.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            btnAddItem.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
