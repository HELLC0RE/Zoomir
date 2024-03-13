using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zoomir
{
    public class DbMethods
    {
        
        public static List<Product> GetAllProducts()
        {
            string connectionString = "Server=localhost;Port=5432;Database=ZoomirDB;User Id=postgres;Password=qwerty123";
        List<Product> allProducts = new List<Product>();          

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM products";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                Title = reader.GetString(1),
                                Cost = reader.GetDecimal(2),
                                Discount = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3),
                                Quantity = reader.GetInt32(4),
                            };
                            allProducts.Add(product);
                        }
                    }
                }
            }

            return allProducts;
        }
        private int GetLatestOrderId()
        {
            int orderId = -1; // Значение по умолчанию, если не удалось получить ID заказа
            string connectionString = "Server=localhost;Port=5432;Database=ZoomirDB;User Id=postgres;Password=qwerty123";
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    string query = "SELECT MAX(id) FROM order_;"; // Получаем максимальный ID заказа
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    connection.Open();
                    orderId = Convert.ToInt32(command.ExecuteScalar()); // Получаем максимальный ID
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении номера заказа: " + ex.Message);
            }

            return orderId;
        }
    }
}
