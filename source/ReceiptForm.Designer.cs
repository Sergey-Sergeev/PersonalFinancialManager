namespace PersonalFinancialManager.source
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
            label1 = new Label();
            label2 = new Label();
            fnTextBox = new TextBox();
            iTextBox = new TextBox();
            label3 = new Label();
            fpTextBox = new TextBox();
            label4 = new Label();
            sTextBox = new TextBox();
            label5 = new Label();
            tTextBox = new TextBox();
            label6 = new Label();
            label7 = new Label();
            nComboBox = new ComboBox();
            okButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calibri", 14F);
            label1.Location = new Point(226, 9);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(49, 29);
            label1.TabIndex = 0;
            label1.Text = "Чек";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 59);
            label2.Name = "label2";
            label2.Size = new Size(109, 24);
            label2.TabIndex = 1;
            label2.Text = "Номер ФН: ";
            // 
            // fnTextBox
            // 
            fnTextBox.Location = new Point(218, 56);
            fnTextBox.Name = "fnTextBox";
            fnTextBox.PlaceholderText = "0000000000000000";
            fnTextBox.Size = new Size(286, 32);
            fnTextBox.TabIndex = 2;
            // 
            // iTextBox
            // 
            iTextBox.Location = new Point(218, 94);
            iTextBox.Name = "iTextBox";
            iTextBox.PlaceholderText = "00000";
            iTextBox.Size = new Size(286, 32);
            iTextBox.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 97);
            label3.Name = "label3";
            label3.Size = new Size(152, 24);
            label3.TabIndex = 3;
            label3.Text = "Номер чека ФД: ";
            // 
            // fpTextBox
            // 
            fpTextBox.Location = new Point(218, 132);
            fpTextBox.Name = "fpTextBox";
            fpTextBox.PlaceholderText = "000000000";
            fpTextBox.Size = new Size(286, 32);
            fpTextBox.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 135);
            label4.Name = "label4";
            label4.Size = new Size(200, 24);
            label4.TabIndex = 5;
            label4.Text = "Фискальный знак ФП: ";
            // 
            // sTextBox
            // 
            sTextBox.Location = new Point(218, 170);
            sTextBox.Name = "sTextBox";
            sTextBox.PlaceholderText = "0.0";
            sTextBox.Size = new Size(286, 32);
            sTextBox.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 173);
            label5.Name = "label5";
            label5.Size = new Size(143, 24);
            label5.TabIndex = 7;
            label5.Text = "Полная сумма: ";
            // 
            // tTextBox
            // 
            tTextBox.Location = new Point(218, 208);
            tTextBox.Name = "tTextBox";
            tTextBox.PlaceholderText = "00.00.0000 00.00.00";
            tTextBox.Size = new Size(286, 32);
            tTextBox.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 211);
            label6.Name = "label6";
            label6.Size = new Size(136, 24);
            label6.TabIndex = 9;
            label6.Text = "Дата и время: ";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 252);
            label7.Name = "label7";
            label7.Size = new Size(160, 24);
            label7.TabIndex = 11;
            label7.Text = "Приход/возврат: ";
            // 
            // nComboBox
            // 
            nComboBox.BackColor = Color.White;
            nComboBox.DisplayMember = "0";
            nComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            nComboBox.FlatStyle = FlatStyle.System;
            nComboBox.ForeColor = SystemColors.InfoText;
            nComboBox.FormattingEnabled = true;
            nComboBox.ImeMode = ImeMode.NoControl;
            nComboBox.Location = new Point(218, 249);
            nComboBox.Name = "nComboBox";
            nComboBox.Size = new Size(286, 32);
            nComboBox.TabIndex = 12;
            // 
            // okButton
            // 
            okButton.Location = new Point(386, 563);
            okButton.Name = "okButton";
            okButton.Size = new Size(118, 33);
            okButton.TabIndex = 13;
            okButton.Text = "ОК";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(262, 563);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(118, 33);
            cancelButton.TabIndex = 14;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // ReceiptForm
            // 
            AutoScaleDimensions = new SizeF(10F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(516, 608);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(nComboBox);
            Controls.Add(label7);
            Controls.Add(tTextBox);
            Controls.Add(label6);
            Controls.Add(sTextBox);
            Controls.Add(label5);
            Controls.Add(fpTextBox);
            Controls.Add(label4);
            Controls.Add(iTextBox);
            Controls.Add(label3);
            Controls.Add(fnTextBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4);
            Name = "ReceiptForm";
            Text = "Чек";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox fnTextBox;
        private TextBox iTextBox;
        private Label label3;
        private TextBox fpTextBox;
        private Label label4;
        private TextBox sTextBox;
        private Label label5;
        private TextBox tTextBox;
        private Label label6;
        private Label label7;
        public ComboBox nComboBox;
        private Button okButton;
        private Button cancelButton;
    }
}