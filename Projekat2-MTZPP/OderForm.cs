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
    public partial class OderForm : Form
    {
        OrderDOM oDOM = new OrderDOM();
        List<ItemDOM> itemDOMlist = new List<ItemDOM>();
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
            cmbClient.DataSource = cBL.GetClient();
            cmbClient.DisplayMember = "ClientName";
            cmbClient.ValueMember = "ClientID";

            ProductBL pBL = new ProductBL();
            cmbProduct.DataSource = pBL.GetProducts();
            cmbProduct.DisplayMember = "ProductName";
            cmbProduct.ValueMember = "ProductID";

            lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            cmbProduct.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            btnAddItem.Enabled = false;

            cmbProduct.SelectedIndexChanged += cmbProduct_SelectedIndexChanged;
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedProduct = (ProductDOM)cmbProduct.SelectedItem;
            if (selectedProduct != null)
            {
                textBox1.Text = selectedProduct.ProductPrice.ToString();
            }
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            oDOM.OrderDate = DateTime.Parse(lblDate.Text);
            oDOM.Employee_EmployeeID = (Int32)cmbEmployee.SelectedValue;
            oDOM.Client_ClientID = (Int32)cmbClient.SelectedValue;

            cmbProduct.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            btnAddItem.Enabled = true;
        }
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            ItemDOM iDOM = new ItemDOM();
            iDOM.Product_ProductID = (Int32)cmbProduct.SelectedValue;
            iDOM.ItemPrice = Convert.ToDecimal(textBox1.Text);
            iDOM.Quantity = Convert.ToInt16(textBox2.Text);

            itemDOMlist.Add(iDOM);

            dgvItem.DataSource = null;
            dgvItem.DataSource = itemDOMlist;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OrderBL orderBL = new OrderBL();

            // Insert the order and retrieve the generated OrderID
            int orderId = orderBL.InsertOrder(oDOM);

            // After the order is inserted, the OrderID is generated automatically
            foreach (var item in itemDOMlist)
            {
                item.Order_OrderID = orderId; // Set the OrderID for each item
                orderBL.InsertItem(item);
            }

            MessageBox.Show("Order saved successfully");
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
