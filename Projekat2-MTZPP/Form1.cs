using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinesLayer;
using Domain;

namespace Projekat2_MTZPP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            button1.FlatAppearance.MouseOverBackColor = button1.BackColor;
            button1.BackColorChanged += (s, e) => {
                button1.FlatAppearance.MouseOverBackColor = button1.BackColor;
            };
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            OderForm fo = new OderForm();
            fo.ShowDialog();
        }

        private void BtnReport_Click(object sender, EventArgs e)
        {
            ReportForm rp = new ReportForm();
            rp.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
