using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void ButtonImport_Click(object sender, EventArgs e)
        {
            FormImport ImportNew = new FormImport();
            this.Hide();
            ImportNew.ShowDialog();
            this.Show();
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            FormSearch SearchNew = new FormSearch();
            this.Hide();
            SearchNew.ShowDialog();
            this.Show();
        }
    }
}
