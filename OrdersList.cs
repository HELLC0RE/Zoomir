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
    public partial class OrdersList : Form
    {
        string connectionString = "Server=localhost;Port=5432;Database=ZoomirDB;User Id=postgres;Password=qwerty123";
        public OrdersList()
        {
            InitializeComponent();
            LoadOrdersData();
        }

        private void LoadOrdersData()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = @"SELECT 
                                    o.id,
                                    p.address AS point_address,
                                    s.title AS state_title,
                                    o.create_date,
                                    o.code,
                                    o.total_amount,
                                    o.total_discount
                                FROM 
                                    order_ o
                                JOIN 
                                    pick_up_point p ON o.point_id = p.id
                                JOIN 
                                    state s ON o.state_id = s.id";
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection);
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Orders");
                    dataGridOrders.DataSource = dataSet.Tables["Orders"];
                    dataGridOrders.Columns[0].HeaderText = "Номер заказа";
                    dataGridOrders.Columns[1].HeaderText = "Пункт выдачи";
                    dataGridOrders.Columns[2].HeaderText = "Статус заказа";
                    dataGridOrders.Columns[3].HeaderText = "Дата создания";
                    dataGridOrders.Columns[4].HeaderText = "Код получения";
                    dataGridOrders.Columns[5].HeaderText = "Общая сумма (руб)";
                    dataGridOrders.Columns[6].HeaderText = "Общая скидка (руб)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных из базы данных: " + ex.Message);
            }
        }

        private void dataGridOrders_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Получаем измененную строку
                DataGridViewRow editedRow = dataGridOrders.Rows[e.RowIndex];
                int orderId = Convert.ToInt32(editedRow.Cells[0].Value); 

                // Получаем новые значения из ячеек
                string newPointAddress = editedRow.Cells[1].Value.ToString();
                string newStateTitle = editedRow.Cells[2].Value.ToString();
                DateTime newCreateDate = Convert.ToDateTime(editedRow.Cells[3].Value);
                int newOrderCode = Convert.ToInt32(editedRow.Cells[4].Value);
                decimal newTotalAmount = Convert.ToDecimal(editedRow.Cells[5].Value);
                decimal newTotalDiscount = Convert.ToDecimal(editedRow.Cells[6].Value);

                // Обновляем запись в базе данных
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlUpdateQuery = @"UPDATE order_ 
                                          SET 
                                            point_id = (SELECT id FROM pick_up_point WHERE address = @PointAddress),
                                            state_id = (SELECT id FROM state WHERE title = @StateTitle),
                                            create_date = @CreateDate,
                                            code = @OrderCode,
                                            total_amount = @TotalAmount,
                                            total_discount = @TotalDiscount
                                          WHERE id = @OrderId";

                    NpgsqlCommand command = new NpgsqlCommand(sqlUpdateQuery, connection);
                    command.Parameters.AddWithValue("@PointAddress", newPointAddress);
                    command.Parameters.AddWithValue("@StateTitle", newStateTitle);
                    command.Parameters.AddWithValue("@CreateDate", newCreateDate);
                    command.Parameters.AddWithValue("@OrderCode", newOrderCode);
                    command.Parameters.AddWithValue("@TotalAmount", newTotalAmount);
                    command.Parameters.AddWithValue("@TotalDiscount", newTotalDiscount);
                    command.Parameters.AddWithValue("@OrderId", orderId);

                    command.ExecuteNonQuery();
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
