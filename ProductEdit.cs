using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zoomir
{
    public partial class ProductEdit : Form
    {
        public ProductEdit()
        {
            InitializeComponent();
            LoadProductsData();
        }
        string connectionString = "Server=localhost;Port=5432;Database=ZoomirDB;User Id=postgres;Password=qwerty123";

        private void LoadProductsData()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = @"SELECT * from products";
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection);
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Products");
                    dataGridProducts.DataSource = dataSet.Tables["Products"];
                    dataGridProducts.Columns[0].HeaderText = "Номер продукта";
                    dataGridProducts.Columns[1].HeaderText = "Название";
                    dataGridProducts.Columns[2].HeaderText = "Стоимость";
                    dataGridProducts.Columns[3].HeaderText = "Скидка (руб.)";
                    dataGridProducts.Columns[4].HeaderText = "Количество (шт.)";
                    dataGridProducts.Columns[0].ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных из базы данных: " + ex.Message);
            }
        }
       

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddNewProduct();
            MessageBox.Show("Продукт успешно добавлен.");
            LoadProductsData();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridProducts.SelectedRows.Count > 0)
                {
                    int selectedIndex = dataGridProducts.SelectedRows[0].Index;

                    int productId = Convert.ToInt32(dataGridProducts.Rows[selectedIndex].Cells[0].Value);

                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();
                        string sqlDeleteQuery = "DELETE FROM products WHERE id = @productId";

                        using (NpgsqlCommand command = new NpgsqlCommand(sqlDeleteQuery, connection))
                        {
                            command.Parameters.AddWithValue("@productId", productId);

                            command.ExecuteNonQuery();
                        }
                    }

                    dataGridProducts.Rows.RemoveAt(selectedIndex);

                    MessageBox.Show("Продукт успешно удален.");
                    LoadProductsData();
                }
                else
                {
                    MessageBox.Show("Выберите продукт для удаления.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении продукта: " + ex.Message);
            }
        }
        private void AddNewProduct()
        {
            try
            {
                int lastIndex = dataGridProducts.Rows.Count - 2;
                string title = dataGridProducts.Rows[lastIndex].Cells[1].Value.ToString();
                decimal cost = Convert.ToDecimal(dataGridProducts.Rows[lastIndex].Cells[2].Value);
                decimal discount = Convert.ToDecimal(dataGridProducts.Rows[lastIndex].Cells[3].Value);
                int quantity = Convert.ToInt32(dataGridProducts.Rows[lastIndex].Cells[4].Value);

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlInsertQuery = @"INSERT INTO products (title, cost, discount, quantity)
                                      VALUES (@title, @cost, @discount, @quantity)";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlInsertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@title", title);
                        command.Parameters.AddWithValue("@cost", cost);
                        command.Parameters.AddWithValue("@discount", discount);
                        command.Parameters.AddWithValue("@quantity", quantity);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении нового продукта: " + ex.Message);
            }
        }

        private void dataGridProducts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Проверяем, что индекс строки не является индексом последней строки
                if (e.RowIndex != dataGridProducts.Rows.Count - 2)
                {
                    DataGridViewRow editedRow = dataGridProducts.Rows[e.RowIndex];
                    int productId = Convert.ToInt32(editedRow.Cells[0].Value);
                    string newTitle = editedRow.Cells[1].Value != DBNull.Value ? Convert.ToString(editedRow.Cells[1].Value) : null;
                    decimal newCost = editedRow.Cells[2].Value != DBNull.Value ? Convert.ToDecimal(editedRow.Cells[2].Value) : 0m;
                    decimal newDiscount = editedRow.Cells[3].Value != DBNull.Value ? Convert.ToDecimal(editedRow.Cells[3].Value) : 0m;
                    int newQuantity = editedRow.Cells[4].Value != DBNull.Value ? Convert.ToInt32(editedRow.Cells[4].Value) : 0;

                    // Обновляем запись в базе данных
                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();
                        string sqlUpdateQuery = @"UPDATE products 
                                      SET 
                                        title = @title,
                                        cost = @cost,
                                        discount = @discount,
                                        quantity = @quantity
                                      WHERE id = @productId";

                        NpgsqlCommand command = new NpgsqlCommand(sqlUpdateQuery, connection);
                        command.Parameters.AddWithValue("@title", newTitle);
                        command.Parameters.AddWithValue("@cost", newCost);
                        command.Parameters.AddWithValue("@discount", newDiscount);
                        command.Parameters.AddWithValue("@quantity", newQuantity);
                        command.Parameters.AddWithValue("@productId", productId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении данных: " + ex.Message);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }   
}
