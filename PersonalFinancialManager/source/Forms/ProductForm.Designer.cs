namespace PersonalFinancialManager.source.Forms
{
    partial class ProductForm
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
            sumTextBox = new TextBox();
            label6 = new Label();
            quantityTextBox = new TextBox();
            label5 = new Label();
            priceTextBox = new TextBox();
            label4 = new Label();
            nameTextBox = new TextBox();
            label3 = new Label();
            categoryComboBox = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            cancelButton = new Button();
            okButton = new Button();
            SuspendLayout();
            // 
            // sumTextBox
            // 
            sumTextBox.Location = new Point(117, 143);
            sumTextBox.Margin = new Padding(3, 4, 3, 4);
            sumTextBox.Name = "sumTextBox";
            sumTextBox.ReadOnly = true;
            sumTextBox.Size = new Size(458, 27);
            sumTextBox.TabIndex = 50;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 181);
            label6.Margin = new Padding(3, 4, 3, 4);
            label6.Name = "label6";
            label6.Size = new Size(83, 19);
            label6.TabIndex = 49;
            label6.Text = "Категория:";
            // 
            // quantityTextBox
            // 
            quantityTextBox.Location = new Point(117, 108);
            quantityTextBox.Margin = new Padding(3, 4, 3, 4);
            quantityTextBox.Name = "quantityTextBox";
            quantityTextBox.PlaceholderText = "0,0";
            quantityTextBox.Size = new Size(458, 27);
            quantityTextBox.TabIndex = 48;
            quantityTextBox.Leave += quantityTextBox_Leave;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 146);
            label5.Margin = new Padding(3, 4, 3, 4);
            label5.Name = "label5";
            label5.Size = new Size(59, 19);
            label5.TabIndex = 47;
            label5.Text = "Сумма:";
            // 
            // priceTextBox
            // 
            priceTextBox.Location = new Point(117, 73);
            priceTextBox.Margin = new Padding(3, 4, 3, 4);
            priceTextBox.Name = "priceTextBox";
            priceTextBox.PlaceholderText = "0,0";
            priceTextBox.Size = new Size(458, 27);
            priceTextBox.TabIndex = 46;
            priceTextBox.Leave += priceTextBox_Leave;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 111);
            label4.Margin = new Padding(3, 4, 3, 4);
            label4.Name = "label4";
            label4.Size = new Size(96, 19);
            label4.TabIndex = 45;
            label4.Text = "Количество: ";
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(117, 38);
            nameTextBox.Margin = new Padding(3, 4, 3, 4);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(458, 27);
            nameTextBox.TabIndex = 44;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 76);
            label3.Margin = new Padding(3, 4, 3, 4);
            label3.Name = "label3";
            label3.Size = new Size(48, 19);
            label3.TabIndex = 43;
            label3.Text = "Цена:";
            // 
            // categoryComboBox
            // 
            categoryComboBox.FormattingEnabled = true;
            categoryComboBox.Location = new Point(117, 178);
            categoryComboBox.Margin = new Padding(3, 4, 3, 4);
            categoryComboBox.Name = "categoryComboBox";
            categoryComboBox.Size = new Size(458, 27);
            categoryComboBox.TabIndex = 42;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 41);
            label2.Margin = new Padding(3, 4, 3, 4);
            label2.Name = "label2";
            label2.Size = new Size(80, 19);
            label2.TabIndex = 41;
            label2.Text = "Название:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calibri", 14F);
            label1.Location = new Point(257, 9);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(77, 23);
            label1.TabIndex = 40;
            label1.Text = "Продукт";
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(333, 212);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(118, 33);
            cancelButton.TabIndex = 52;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // okButton
            // 
            okButton.Location = new Point(457, 212);
            okButton.Name = "okButton";
            okButton.Size = new Size(118, 33);
            okButton.TabIndex = 51;
            okButton.Text = "ОК";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // ProductForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(587, 255);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(sumTextBox);
            Controls.Add(label6);
            Controls.Add(quantityTextBox);
            Controls.Add(label5);
            Controls.Add(priceTextBox);
            Controls.Add(label4);
            Controls.Add(nameTextBox);
            Controls.Add(label3);
            Controls.Add(categoryComboBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(3, 4, 3, 4);
            MaximumSize = new Size(603, 294);
            MinimumSize = new Size(603, 294);
            Name = "ProductForm";
            Text = "Продукт";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox sumTextBox;
        private Label label6;
        private TextBox quantityTextBox;
        private Label label5;
        private TextBox priceTextBox;
        private Label label4;
        private TextBox nameTextBox;
        private Label label3;
        private ComboBox categoryComboBox;
        private Label label2;
        private Label label1;
        private Button cancelButton;
        private Button okButton;
    }
}