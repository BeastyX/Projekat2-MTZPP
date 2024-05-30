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
        }
    }
}
