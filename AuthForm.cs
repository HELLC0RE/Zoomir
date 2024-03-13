using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
namespace Zoomir
{
    public partial class AuthForm : Form
    {
        private NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=ZoomirDB;User Id=postgres;Password=qwerty123");
        public AuthForm()
        {
            InitializeComponent();
        }

        private void authBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(loginBox.Text) && !string.IsNullOrWhiteSpace(passwordBox.Text))
            {
                connection.Open();
                var command = new NpgsqlCommand($"SELECT role_id FROM users WHERE login = '{loginBox.Text}' AND password = '{passwordBox.Text}'", connection);
                var role = command.ExecuteScalar();
                connection.Close();
                if (role != null)
                {
                    ProductsList productsList = new ProductsList();
                    if (role.ToString() == "1")
                    {
                        productsList.Role = "Клиент";
                    }
                    else if (role.ToString() == "2")
                    {
                        productsList.Role = "Администратор";
                    }
                    else
                    {
                        productsList.Role = "Менеджер";
                    }

                    productsList.Closed += (s, args) => Close();
                    productsList.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка");
            }
        }
        private void visitorBtn_Click(object sender, EventArgs e)
        {
            ProductsList productsList = new ProductsList();
            productsList.Role = "";
            productsList.Closed += (s, args) => Close();
            productsList.Show();
            Hide();
        }
    }
}

