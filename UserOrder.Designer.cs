namespace Zoomir
{
    partial class UserOrder
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
            this.flowLayoutPanelOrder = new System.Windows.Forms.FlowLayoutPanel();
            this.totalAmountLabel = new System.Windows.Forms.Label();
            this.buttonCreateOrder = new System.Windows.Forms.Button();
            this.pick_upBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowLayoutPanelOrder
            // 
            this.flowLayoutPanelOrder.AutoScroll = true;
            this.flowLayoutPanelOrder.Location = new System.Drawing.Point(12, 29);
            this.flowLayoutPanelOrder.Name = "flowLayoutPanelOrder";
            this.flowLayoutPanelOrder.Size = new System.Drawing.Size(760, 500);
            this.flowLayoutPanelOrder.TabIndex = 0;
            // 
            // totalAmountLabel
            // 
            this.totalAmountLabel.AutoSize = true;
            this.totalAmountLabel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.totalAmountLabel.Location = new System.Drawing.Point(12, 549);
            this.totalAmountLabel.Name = "totalAmountLabel";
            this.totalAmountLabel.Size = new System.Drawing.Size(0, 21);
            this.totalAmountLabel.TabIndex = 3;
            // 
            // buttonCreateOrder
            // 
            this.buttonCreateOrder.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCreateOrder.Location = new System.Drawing.Point(16, 608);
            this.buttonCreateOrder.Name = "buttonCreateOrder";
            this.buttonCreateOrder.Size = new System.Drawing.Size(223, 29);
            this.buttonCreateOrder.TabIndex = 4;
            this.buttonCreateOrder.Text = "Оформить заказ";
            this.buttonCreateOrder.UseVisualStyleBackColor = true;
            this.buttonCreateOrder.Click += new System.EventHandler(this.buttonCreateOrder_Click);
            // 
            // pick_upBox
            // 
            this.pick_upBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pick_upBox.FormattingEnabled = true;
            this.pick_upBox.Location = new System.Drawing.Point(301, 608);
            this.pick_upBox.Name = "pick_upBox";
            this.pick_upBox.Size = new System.Drawing.Size(289, 29);
            this.pick_upBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(432, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "Нажмите ПКМ на название товара, чтобы удалить из корзины";
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(635, 602);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(137, 35);
            this.buttonBack.TabIndex = 7;
            this.buttonBack.Text = "Назад";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // UserOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pick_upBox);
            this.Controls.Add(this.buttonCreateOrder);
            this.Controls.Add(this.totalAmountLabel);
            this.Controls.Add(this.flowLayoutPanelOrder);
            this.MaximumSize = new System.Drawing.Size(800, 700);
            this.MinimumSize = new System.Drawing.Size(800, 700);
            this.Name = "UserOrder";
            this.Text = "Оформление заказа";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelOrder;
        private System.Windows.Forms.Label totalAmountLabel;
        private System.Windows.Forms.Button buttonCreateOrder;
        private System.Windows.Forms.ComboBox pick_upBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonBack;
    }
}