using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gubar
{
    public partial class MasterForm : Form
    {
        private Form previousForm;

        public MasterForm()
        {
            InitializeComponent();
        }

        public void SetPreviousForm(Form form)
        {
            previousForm = form;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (previousForm != null)
            {
                previousForm.Show();
                this.Close();
            }
        }
    }
}
