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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //this.Close();
            //Form4 newForm = new Form4();//Открытие регистрации
            //newForm.Show();
        }
        private Form previousForm; // Переменная для хранения предыдущей формы

        public void SetPreviousForm(Form form)
        {
            previousForm = form;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (previousForm != null)
            {
                previousForm.Show(); // Отображаем предыдущую форму
                this.Close(); // Закрываем текущую форму
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Close();
            //Form5 newForm = new Form5();
            //newForm.Show();
        }


    }
}
