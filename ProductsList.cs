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
    public partial class ProductsList : Form
    {
        public string role;
        public string Role
        {
            get { return role; }
            set { role = value; }
        }
        public int OrderId { get; set; } = 0;
        private DbMethods dbMethods;
        private const int itemsPerPage = 5;
        private int currentPage = 1;
        private int offset;
        private int limit;
        private int totalPages;
        private Dictionary<Product, int> selectedProductsCount = new Dictionary<Product, int>();
        private List<Product> selectedProducts = new List<Product>();

        public ProductsList()
        {
            DbMethods dbMethods = new DbMethods();
            InitializeComponent();
            DisplayPage(currentPage);
            this.Load += ProductsList_Loaded;
            showOrdersBtn.Visible = false;
            productEdit.Visible = false;
            showOrder.Visible = false;
        }

        private void ProductsList_Loaded(object sender, EventArgs e)
        {
            if (role == "Администратор" || role == "Менеджер")
            {
                showOrdersBtn.Visible = true;
            }
            if (role == "Администратор")
            {
                productEdit.Visible = true;
            }
            if (OrderId != 0 && role != "Администратор")
            {
                showOrder.Visible = true;
            }
        }

        private void DisplayProduct(Product product)
        {
            Panel productPanel = new Panel
            {
                Height = 90,
                Width = flowLayoutPanelProducts.Width - 5,
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
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem addItemToOrderMenuItem = new ToolStripMenuItem("Добавить в заказ");
            addItemToOrderMenuItem.Click += (sender, e) =>
            {
                if (!selectedProducts.Contains(product))
                {
                    selectedProducts.Add(product);
                }

                if (selectedProductsCount.ContainsKey(product))
                {
                    selectedProductsCount[product]++;
                }
                else
                {
                    selectedProductsCount.Add(product, 1);
                }

                showOrder.Visible = true;
            };
            contextMenu.Items.Add(addItemToOrderMenuItem);

            titleLabel.ContextMenuStrip = contextMenu;

            titleLabel.MouseClick += (sender, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    contextMenu.Show(titleLabel, e.Location);
                }
            };
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
                Text = $"Количество: \n {product.Quantity}",
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

            flowLayoutPanelProducts.Controls.Add(productPanel);
        }

        private void DisplayPagination(int count)
        {
            totalPages = (int)Math.Ceiling((double)count / itemsPerPage);

            flowLayoutPanelPagination.RightToLeft = RightToLeft.Yes;

            AddPageButton("<", currentPage + 1);

            for (int i = totalPages; i != 0; i--)
            {
                Label pageButton = new Label
                {
                    Text = i.ToString(),
                    Width = 20,
                    Height = 20,
                    Margin = new Padding(5),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Cursor = Cursors.Hand
                };

                if (i == currentPage)
                {
                    pageButton.Font = new System.Drawing.Font(pageButton.Font, FontStyle.Bold | FontStyle.Underline);
                }

                pageButton.Click += (sender, e) =>
                {
                    currentPage = int.Parse(((Label)sender).Text);
                    DisplayPage(currentPage);
                };

                flowLayoutPanelPagination.Controls.Add(pageButton);
            }

            AddPageButton(">", currentPage - 1);
        }

        private void AddPageButton(string buttonText, int targetPage)
        {
            Label pageButton = new Label
            {
                Text = buttonText,
                Width = 15,
                Height = 15,
                Margin = new Padding(5),
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };

            pageButton.Click += (sender, e) =>
            {
                if (targetPage > 0 && targetPage <= totalPages)
                {
                    currentPage = targetPage;
                    DisplayPage(currentPage);
                }
            };

            flowLayoutPanelPagination.Controls.Add(pageButton);
        }
        private void DisplayPage(int page)
        {
            flowLayoutPanelProducts.Controls.Clear();
            flowLayoutPanelPagination.Controls.Clear();

            offset = (page - 1) * itemsPerPage;
            limit = itemsPerPage;

            var filteredProducts = DbMethods.GetAllProducts();         
            int resultCount = filteredProducts.Count;
            filteredProducts = filteredProducts.Skip(offset).Take(limit).ToList();

            for (int i = 0; i < filteredProducts.Count; i++)
            {
                DisplayProduct(filteredProducts[i]);
            }

            DisplayPagination(resultCount);
        }

        private void showOrder_Click(object sender, EventArgs e)
        {
            UserOrder userOrderForm = new UserOrder();
            userOrderForm.AddProductsToLayoutPanel(selectedProducts, selectedProductsCount);
            userOrderForm.ShowDialog();
        }

        private void showOrdersBtn_Click(object sender, EventArgs e)
        {
            OrdersList ordersList = new OrdersList();
            ordersList.ShowDialog();
        }

        private void productEdit_Click(object sender, EventArgs e)
        {
            ProductEdit productEdit = new ProductEdit();
            productEdit.ShowDialog();
        }
    }
}
