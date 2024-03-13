namespace Zoomir
{
    partial class ProductsList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanelProducts = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelPagination = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.showOrdersBtn = new System.Windows.Forms.Button();
            this.showOrder = new System.Windows.Forms.Button();
            this.productEdit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowLayoutPanelProducts
            // 
            this.flowLayoutPanelProducts.Location = new System.Drawing.Point(12, 44);
            this.flowLayoutPanelProducts.Name = "flowLayoutPanelProducts";
            this.flowLayoutPanelProducts.Size = new System.Drawing.Size(760, 500);
            this.flowLayoutPanelProducts.TabIndex = 0;
            // 
            // flowLayoutPanelPagination
            // 
            this.flowLayoutPanelPagination.Location = new System.Drawing.Point(472, 550);
            this.flowLayoutPanelPagination.Name = "flowLayoutPanelPagination";
            this.flowLayoutPanelPagination.Size = new System.Drawing.Size(300, 60);
            this.flowLayoutPanelPagination.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(431, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Нажмите ПКМ на название товара, чтобы добавить в корзину";
            // 
            // showOrdersBtn
            // 
            this.showOrdersBtn.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.showOrdersBtn.Location = new System.Drawing.Point(445, 3);
            this.showOrdersBtn.Name = "showOrdersBtn";
            this.showOrdersBtn.Size = new System.Drawing.Size(100, 38);
            this.showOrdersBtn.TabIndex = 3;
            this.showOrdersBtn.Text = "Просмотреть заказы";
            this.showOrdersBtn.UseVisualStyleBackColor = true;
            this.showOrdersBtn.Click += new System.EventHandler(this.showOrdersBtn_Click);
            // 
            // showOrder
            // 
            this.showOrder.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.showOrder.Location = new System.Drawing.Point(562, 3);
            this.showOrder.Name = "showOrder";
            this.showOrder.Size = new System.Drawing.Size(100, 38);
            this.showOrder.TabIndex = 4;
            this.showOrder.Text = "Показать заказ";
            this.showOrder.UseVisualStyleBackColor = true;
            this.showOrder.Click += new System.EventHandler(this.showOrder_Click);
            // 
            // productEdit
            // 
            this.productEdit.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.productEdit.Location = new System.Drawing.Point(672, 3);
            this.productEdit.Name = "productEdit";
            this.productEdit.Size = new System.Drawing.Size(100, 38);
            this.productEdit.TabIndex = 5;
            this.productEdit.Text = "Работа с продуктами";
            this.productEdit.UseVisualStyleBackColor = true;
            this.productEdit.Click += new System.EventHandler(this.productEdit_Click);
            // 
            // ProductsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.productEdit);
            this.Controls.Add(this.showOrder);
            this.Controls.Add(this.showOrdersBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanelPagination);
            this.Controls.Add(this.flowLayoutPanelProducts);
            this.MaximumSize = new System.Drawing.Size(800, 700);
            this.MinimumSize = new System.Drawing.Size(800, 700);
            this.Name = "ProductsList";
            this.Text = "Список продуктов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProducts;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPagination;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button showOrdersBtn;
        private System.Windows.Forms.Button showOrder;
        private System.Windows.Forms.Button productEdit;
    }
}