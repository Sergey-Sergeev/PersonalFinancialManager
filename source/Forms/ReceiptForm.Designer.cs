namespace PersonalFinancialManager.source.Forms
{
    partial class ReceiptForm
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
            components = new System.ComponentModel.Container();
            cancelButton = new Button();
            okButton = new Button();
            receiptQRDataLabel = new Label();
            label1 = new Label();
            label2 = new Label();
            addsressComboBox = new ComboBox();
            label3 = new Label();
            dateTimeTextBox = new TextBox();
            cashTextBox = new TextBox();
            label4 = new Label();
            eCashTextBox = new TextBox();
            label5 = new Label();
            totalSumTextBox = new TextBox();
            label6 = new Label();
            productsLisViewtContextMenuStrip = new ContextMenuStrip(components);
            addProductToolStripMenuItem = new ToolStripMenuItem();
            changeProductToolStripMenuItem = new ToolStripMenuItem();
            deleteProductToolStripMenuItem = new ToolStripMenuItem();
            productsListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            productsLisViewtContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(269, 444);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(118, 33);
            cancelButton.TabIndex = 29;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // okButton
            // 
            okButton.Location = new Point(393, 444);
            okButton.Name = "okButton";
            okButton.Size = new Size(118, 33);
            okButton.TabIndex = 28;
            okButton.Text = "ОК";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // receiptQRDataLabel
            // 
            receiptQRDataLabel.AutoSize = true;
            receiptQRDataLabel.Location = new Point(12, 38);
            receiptQRDataLabel.Margin = new Padding(3);
            receiptQRDataLabel.Name = "receiptQRDataLabel";
            receiptQRDataLabel.Size = new Size(103, 19);
            receiptQRDataLabel.TabIndex = 16;
            receiptQRDataLabel.Text = "Данные чека:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calibri", 14F);
            label1.Location = new Point(228, 9);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(39, 23);
            label1.TabIndex = 15;
            label1.Text = "Чек";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 72);
            label2.Margin = new Padding(3);
            label2.Name = "label2";
            label2.Size = new Size(124, 19);
            label2.TabIndex = 30;
            label2.Text = "Адрес магазина:";
            // 
            // addsressComboBox
            // 
            addsressComboBox.FormattingEnabled = true;
            addsressComboBox.Location = new Point(142, 69);
            addsressComboBox.Name = "addsressComboBox";
            addsressComboBox.Size = new Size(369, 27);
            addsressComboBox.TabIndex = 31;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 105);
            label3.Margin = new Padding(3);
            label3.Name = "label3";
            label3.Size = new Size(105, 19);
            label3.TabIndex = 32;
            label3.Text = "Дата и время:";
            // 
            // dateTimeTextBox
            // 
            dateTimeTextBox.Location = new Point(142, 102);
            dateTimeTextBox.Name = "dateTimeTextBox";
            dateTimeTextBox.PlaceholderText = "00.00.0000 00:00:00";
            dateTimeTextBox.Size = new Size(369, 27);
            dateTimeTextBox.TabIndex = 33;
            dateTimeTextBox.Leave += dateTimeTextBox_Leave;
            // 
            // cashTextBox
            // 
            cashTextBox.Location = new Point(142, 135);
            cashTextBox.Name = "cashTextBox";
            cashTextBox.Size = new Size(369, 27);
            cashTextBox.TabIndex = 35;
            cashTextBox.Leave += cashTextBox_Leave;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 138);
            label4.Margin = new Padding(3);
            label4.Name = "label4";
            label4.Size = new Size(100, 19);
            label4.TabIndex = 34;
            label4.Text = "Наличными: ";
            // 
            // eCashTextBox
            // 
            eCashTextBox.Location = new Point(142, 168);
            eCashTextBox.Name = "eCashTextBox";
            eCashTextBox.Size = new Size(369, 27);
            eCashTextBox.TabIndex = 37;
            eCashTextBox.Leave += eCashTextBox_Leave;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 171);
            label5.Margin = new Padding(3);
            label5.Name = "label5";
            label5.Size = new Size(123, 19);
            label5.TabIndex = 36;
            label5.Text = "Безналичными: ";
            // 
            // totalSumTextBox
            // 
            totalSumTextBox.Location = new Point(142, 201);
            totalSumTextBox.Name = "totalSumTextBox";
            totalSumTextBox.ReadOnly = true;
            totalSumTextBox.Size = new Size(369, 27);
            totalSumTextBox.TabIndex = 39;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 204);
            label6.Margin = new Padding(3);
            label6.Name = "label6";
            label6.Size = new Size(109, 19);
            label6.TabIndex = 38;
            label6.Text = "Общая сумма:";
            // 
            // productsLisViewtContextMenuStrip
            // 
            productsLisViewtContextMenuStrip.Items.AddRange(new ToolStripItem[] { addProductToolStripMenuItem, changeProductToolStripMenuItem, deleteProductToolStripMenuItem });
            productsLisViewtContextMenuStrip.Name = "productsTreeViewContextMenuStrip";
            productsLisViewtContextMenuStrip.Size = new Size(129, 70);
            // 
            // addProductToolStripMenuItem
            // 
            addProductToolStripMenuItem.Name = "addProductToolStripMenuItem";
            addProductToolStripMenuItem.Size = new Size(128, 22);
            addProductToolStripMenuItem.Text = "Добавить";
            addProductToolStripMenuItem.Click += addProductToolStripMenuItem_Click;
            // 
            // changeProductToolStripMenuItem
            // 
            changeProductToolStripMenuItem.Name = "changeProductToolStripMenuItem";
            changeProductToolStripMenuItem.Size = new Size(128, 22);
            changeProductToolStripMenuItem.Text = "Изменить";
            changeProductToolStripMenuItem.Click += changeProductToolStripMenuItem_Click;
            // 
            // deleteProductToolStripMenuItem
            // 
            deleteProductToolStripMenuItem.Name = "deleteProductToolStripMenuItem";
            deleteProductToolStripMenuItem.Size = new Size(128, 22);
            deleteProductToolStripMenuItem.Text = "Удалить";
            deleteProductToolStripMenuItem.Click += deleteProductToolStripMenuItem_Click;
            // 
            // productsListView
            // 
            productsListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5 });
            productsListView.ContextMenuStrip = productsLisViewtContextMenuStrip;
            productsListView.FullRowSelect = true;
            productsListView.GridLines = true;
            productsListView.Location = new Point(12, 234);
            productsListView.Name = "productsListView";
            productsListView.Size = new Size(499, 204);
            productsListView.TabIndex = 40;
            productsListView.UseCompatibleStateImageBehavior = false;
            productsListView.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Название";
            columnHeader1.Width = 130;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Цена";
            columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Количество";
            columnHeader3.Width = 95;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Сумма";
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Категория";
            columnHeader5.Width = 120;
            // 
            // ReceiptForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(523, 489);
            Controls.Add(productsListView);
            Controls.Add(totalSumTextBox);
            Controls.Add(label6);
            Controls.Add(eCashTextBox);
            Controls.Add(label5);
            Controls.Add(cashTextBox);
            Controls.Add(label4);
            Controls.Add(dateTimeTextBox);
            Controls.Add(label3);
            Controls.Add(addsressComboBox);
            Controls.Add(label2);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(receiptQRDataLabel);
            Controls.Add(label1);
            Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(3, 4, 3, 4);
            MaximumSize = new Size(539, 528);
            MinimumSize = new Size(539, 528);
            Name = "ReceiptForm";
            Text = "Получение чека через его данные";
            productsLisViewtContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion


        private Button cancelButton;
        private Button okButton;
        private Label receiptQRDataLabel;
        private Label label1;
        private Label label2;
        private ComboBox addsressComboBox;
        private Label label3;
        private TextBox dateTimeTextBox;
        private TextBox cashTextBox;
        private Label label4;
        private TextBox eCashTextBox;
        private Label label5;
        private TextBox totalSumTextBox;
        private Label label6;
        private ContextMenuStrip productsLisViewtContextMenuStrip;
        private ToolStripMenuItem addProductToolStripMenuItem;
        private ToolStripMenuItem changeProductToolStripMenuItem;
        private ToolStripMenuItem deleteProductToolStripMenuItem;
        private ListView productsListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
    }
}