﻿using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace gubar
{
    public partial class Form1 : Form
    {
        private Timer timer; // Задаём таймер через класс таймера 
        private int attemptsLeft = 3; // Общее количество попыток

        public Form1()
        {
            InitializeComponent();
            InitializeTimer(); // Инициализация таймера
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 60000; // 1 минута в миллисекундах
            timer.Tick += TimerTick;
            timer.Enabled = false;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            ResetAttempts(); // Восстановление попыток
            timer.Enabled = false; // Выключение таймера, после восстановления попыток
            MessageBox.Show("Попытки восстановлены. Вы можете попробовать снова.");
            button2.Enabled = true;
        }

        private void ResetAttempts() // Восстановление 3 попыток входа
        {
            attemptsLeft = 3;
        }

        private Form previousForm; // Переменная для хранения предыдущей формы

        public void SetPreviousForm(Form form)
        {
            previousForm = form;
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

                            formx.SetPreviousForm(this); // Устанавливаем текущую форму как предыдущую для новой формы
                            formx.Show();
                            this.Hide(); // Скрываем текущую форму
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
    }
}
