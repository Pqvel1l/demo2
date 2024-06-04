using System;
using System.Windows.Forms;

namespace gubar
{
    public partial class OperatorForm : Form
    {
        private Form previousForm;

        public OperatorForm()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OperatorForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "demoDataSet.zayavka". При необходимости она может быть перемещена или удалена.
            this.zayavkaTableAdapter.Fill(this.demoDataSet.zayavka);

        }

    }
}
