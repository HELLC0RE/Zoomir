using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Npgsql;
using Npgsql.Internal;
using NpgsqlTypes;


namespace Zoomir
{
    public partial class UserOrder : Form
    {
        string connectionString = "Server=localhost;Port=5432;Database=ZoomirDB;User Id=postgres;Password=qwerty123";
        private decimal TotalAmount { get; set; }
        private List<Product> products;
        private Dictionary<Product, int> selectedProductsCount;
        private DbMethods dbMethods;
        private CreateTicket ticket;
        public UserOrder()
        {
            ticket = new CreateTicket();
            dbMethods = new DbMethods();
            InitializeComponent();
            LoadPickUpPoints();
        }
        public void UpdateProductsList(List<Product> updatedProducts, Dictionary<Product, int> updatedSelectedProductsCount)
        {
            this.products = updatedProducts;
            this.selectedProductsCount = updatedSelectedProductsCount;
            UpdateLayoutPanel();
        }
        public void AddProductsToLayoutPanel(List<Product> products, Dictionary<Product, int> selectedProductsCount)
        {
            this.products = products;
            this.selectedProductsCount = selectedProductsCount;
            foreach (var product in products)
            {
                Panel productPanel = new Panel
                {
                    Height = 90,
                    Width = flowLayoutPanelOrder.Width - 5,
                    BorderStyle = BorderStyle.FixedSingle,

                };

                PictureBox pictureBox = new PictureBox
                {
                    Image = Properties.Resources.product_icon,
                    SizeMode = PictureBoxSizeMode.CenterImage,
                    Location = new Point(20, 5),
                    Width = 85,
                    Height = 80,
                };
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                Label titleLabel = new Label
                {
                    Text = $"Название: \n{product.Title}",
                    AutoSize = false,
                    TextAlign = ContentAlignment.TopLeft,
                    Location = new Point(120, 20),
                    Width = 150,
                    Height = 150,
                    Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 10)
                };
                titleLabel.MouseClick += HandleRightClickOnProduct;
                Label costLabel = new Label
                {
                    Text = $"Стоимость: \n {product.Cost:C}",
                    Location = new Point(650, 20),
                    Width = 100,
                    Height = 50,
                    Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
                };

                Label discountLabel = new Label
                {
                    Text = $"Скидка: \n {product.Discount:C}",
                    Location = new Point(500, 20),
                    Width = 100,
                    Height = 50,
                    Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
                };

                Label quantityLabel = new Label
                {
                    Text = $"Количество: \n {(selectedProductsCount.ContainsKey(product) ? selectedProductsCount[product] : 0)}",
                    Location = new Point(300, 20),
                    Width = 200,
                    Height = 50,
                    Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
                };



                productPanel.Controls.Add(pictureBox);
                productPanel.Controls.Add(titleLabel);
                productPanel.Controls.Add(costLabel);
                productPanel.Controls.Add(discountLabel);
                productPanel.Controls.Add(quantityLabel);

                flowLayoutPanelOrder.Controls.Add(productPanel);
            }
            TotalAmount = CalculateTotalAmount(products, selectedProductsCount);
            totalAmountLabel.Text = $"Общая сумма заказа: {TotalAmount:C}";
        }

        private decimal CalculateTotalAmount(List<Product> products, Dictionary<Product, int> selectedProductsCount)
        {
            decimal totalAmount = 0;

            foreach (var kvp in selectedProductsCount)
            {
                Product product = kvp.Key;
                int quantity = kvp.Value;
                decimal productPrice = product.Cost * quantity;

                decimal discountedPrice = productPrice - product.Discount;

                totalAmount += discountedPrice;
            }

            return totalAmount;
        }
        private void LoadPickUpPoints()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    string query = "SELECT address FROM pick_up_point";
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    connection.Open();
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        pick_upBox.Items.Add(reader["address"].ToString());
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }
        }       
        private void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            int orderId = 0;
            int code = 0;
            DateTime date = DateTime.MinValue;
            try
            {
                if (pick_upBox.SelectedItem == null)
                {
                    MessageBox.Show("Выберите адрес для формирования заказа.");
                    return;
                }
                string selectedAddress = pick_upBox.SelectedItem.ToString();
                int pointId;
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    string query = "SELECT id FROM pick_up_point WHERE address = @Address";
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Address", selectedAddress);
                    connection.Open();
                    pointId = Convert.ToInt32(command.ExecuteScalar());
                }

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    Dictionary<Product, int> selectedProductsCount = new Dictionary<Product, int>();
                    foreach (var product in products)
                    {
                        int quantity = selectedProductsCount.ContainsKey(product) ? selectedProductsCount[product] : 0;
                        selectedProductsCount[product] = quantity + 1;
                    }
                    (decimal totalAmount, decimal totalDiscount) = CalculateOrderDetails(selectedProductsCount);
                    string query = "INSERT INTO order_ (point_id, state_id, code, create_date, total_amount, total_discount) " +
                        "VALUES (@point_id, @state_id, @code, @create_date, @total_amount, @total_discount) RETURNING id, code, create_date";
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    command.Parameters.AddWithValue("point_id", pointId);
                    command.Parameters.AddWithValue("state_id", 1);
                    command.Parameters.AddWithValue("code", code = GenerateRandomCode());
                    command.Parameters.AddWithValue("create_date", DateTime.Now);
                    command.Parameters.AddWithValue("total_amount", totalAmount);
                    command.Parameters.AddWithValue("total_discount", totalDiscount);
                    connection.Open();
                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        orderId = reader.GetInt32(0);
                        code = reader.GetInt32(1);
                        date = reader.GetDateTime(2);
                    }
                    reader.Close();
                    connection.Close();
                    List<Product> allProducts;
                    allProducts = DbMethods.GetAllProducts();
                    int deliveryTime = CalculateDeliveryTime(allProducts);                  
                    ticket.TicketCreater(orderId,date, products, selectedProductsCount, totalAmount, totalDiscount, deliveryTime, selectedAddress, code);                    
                    UpdateProductsList(products, selectedProductsCount);
                    products.Clear();
                    selectedProductsCount.Clear();
                    selectedProductsCount.Clear();
                    UpdateLayoutPanel();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании заказа: " + ex.Message);
            }
        }
        private int GenerateRandomCode()
        {
            Random random = new Random();
            return random.Next(100, 1000);
        }
        private void UpdateLayoutPanel()
        {
            flowLayoutPanelOrder.Controls.Clear();
            AddProductsToLayoutPanel(products, selectedProductsCount);
        }
        private void HandleRightClickOnProduct(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Label clickedLabel = (Label)sender;
                Product clickedProduct = products.FirstOrDefault(p => p.Title == clickedLabel.Text.Split('\n')[1].Trim());

                if (clickedProduct != null)
                {
                    ContextMenuStrip contextMenu = new ContextMenuStrip();

                    ToolStripMenuItem removeFromOrderMenuItem = new ToolStripMenuItem("Удалить из заказа");
                    removeFromOrderMenuItem.Click += (menuItemSender, menuItemEventArgs) =>
                    {
                        if (selectedProductsCount.ContainsKey(clickedProduct))
                        {
                            if (selectedProductsCount[clickedProduct] == 1)
                            {
                                products.Remove(clickedProduct);
                                selectedProductsCount.Remove(clickedProduct);
                            }
                            else
                            {
                                selectedProductsCount[clickedProduct]--;
                            }
                            UpdateLayoutPanel();
                        }
                    };

                    contextMenu.Items.Add(removeFromOrderMenuItem);

                    contextMenu.Show(clickedLabel, e.Location);
                }
            }
        }
        private int CalculateDeliveryTime(List<Product> allProducts)
        {
            int availableItemsCount = allProducts.Where(p => p.Quantity > 0).Count();
            if (availableItemsCount > 3)
            {
                 return 3;
            }
            else
            {
                return 6;
            }

        }
        private (decimal, decimal) CalculateOrderDetails(Dictionary<Product, int> selectedProductsCount)
        {
            decimal totalAmount = 0;
            decimal totalDiscount = 0;

            foreach (var kvp in selectedProductsCount)
            {
                Product product = kvp.Key;
                int quantity = kvp.Value;
                decimal productPrice = product.Cost * quantity;
                totalAmount += productPrice;
                totalDiscount += product.Discount * quantity;
            }

            return (totalAmount, totalDiscount);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
