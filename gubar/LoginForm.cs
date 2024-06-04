using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace gubar
{
    public partial class LoginForm : Form
    {
        private Timer timer;
        private int attemptsLeft = 3;

        public LoginForm()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 60000;
            timer.Tick += TimerTick;
            timer.Enabled = false;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            ResetAttempts();
            timer.Enabled = false;
            MessageBox.Show("Попытки восстановлены. Вы можете попробовать снова.");
            button2.Enabled = true;
        }

        private void ResetAttempts()
        {
            attemptsLeft = 3;
        }

        public void button2Click(object sender, EventArgs e)
        {
            if (attemptsLeft > 0)
            {
                SqlConnection conn = new SqlConnection(@"Data Source=TEXPY\SQLEXPRESS;Initial Catalog=demo;Integrated Security=True;");
                conn.Open();

                string query = "SELECT * FROM users WHERE login=@Login AND password=@Password";

                try
                {
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@Login", text_Login.Text);
                    command.Parameters.AddWithValue("@Password", text_Pass.Text);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            string role = reader["type"].ToString();

                            Form formx;
                            switch (role)
                            {
                                case "Менеджер":
                                    formx = new ManagerForm();
                                    break;
                                case "Мастер":
                                    formx = new MasterForm();
                                    break;
                                case "Оператор":
                                    formx = new OperatorForm();
                                    break;
                                case "Заказчик":
                                    formx = new CustomerForm();
                                    break;
                                default:
                                    MessageBox.Show("Неизвестная должность.");
                                    return;
                            }

                            FormManager.ShowForm(formx);
                        }
                        else
                        {
                            MessageBox.Show($"Неверный логин или пароль. Осталось попыток: {--attemptsLeft}");
                            if (attemptsLeft == 0)
                            {
                                timer.Enabled = true;
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    Console.WriteLine("Ошибка:" + error.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Превышено максимальное количество попыток. Попробуйте через минуту.");
                button2.Enabled = false;
            }
        }

        private void toRegisterButton_Click(object sender, EventArgs e)
        {
            RegisterForm form = new RegisterForm();
            form.Show();
            this.Hide();
        }
    }
}
