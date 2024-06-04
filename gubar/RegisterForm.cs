using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gubar
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            string fio = text_fio.Text;
            string phone = text_phone.Text;
            string login = text_login.Text;
            string password = text_password.Text;

            if (string.IsNullOrWhiteSpace(fio) || string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Все поля должны быть заполнены.");
                return;
            }

            string connectionString = @"Data Source=TEXPY\SQLEXPRESS;Initial Catalog=demo;Integrated Security=True;";
            string query = "INSERT INTO users (fio, phone, login, password, type) VALUES (@Fio, @Phone, @Login, @Password, 'Заказчик')";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Fio", fio);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@Login", login);
                command.Parameters.AddWithValue("@Password", password);

                try
                {
                    conn.Open();
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Регистрация прошла успешно.");
                        LoginForm form = new LoginForm();
                        form.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка регистрации. Попробуйте еще раз.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при подключении к базе данных: " + ex.Message);
                }
            }
        }
    }
}
