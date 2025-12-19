namespace PersonalFinancialManager.source.Forms
{
    public partial class QRDataForm
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
            cancelButton = new Button();
            okButton = new Button();
            nComboBox = new ComboBox();
            label7 = new Label();
            tTextBox = new TextBox();
            label6 = new Label();
            sTextBox = new TextBox();
            label5 = new Label();
            fpTextBox = new TextBox();
            label4 = new Label();
            iTextBox = new TextBox();
            label3 = new Label();
            fnTextBox = new TextBox();
            label2 = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(264, 292);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(118, 33);
            cancelButton.TabIndex = 29;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // okButton
            // 
            okButton.Location = new Point(388, 292);
            okButton.Name = "okButton";
            okButton.Size = new Size(118, 33);
            okButton.TabIndex = 28;
            okButton.Text = "ОК";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
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
            nComboBox.Location = new Point(220, 241);
            nComboBox.Name = "nComboBox";
            nComboBox.Size = new Size(286, 27);
            nComboBox.TabIndex = 27;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(14, 244);
            label7.Name = "label7";
            label7.Size = new Size(127, 19);
            label7.TabIndex = 26;
            label7.Text = "Приход/возврат: ";
            // 
            // tTextBox
            // 
            tTextBox.Location = new Point(220, 208);
            tTextBox.Name = "tTextBox";
            tTextBox.PlaceholderText = "00.00.0000 00:00:00";
            tTextBox.Size = new Size(286, 27);
            tTextBox.TabIndex = 25;
            tTextBox.Leave += tTextBox_Leave;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(14, 211);
            label6.Name = "label6";
            label6.Size = new Size(109, 19);
            label6.TabIndex = 24;
            label6.Text = "Дата и время: ";
            // 
            // sTextBox
            // 
            sTextBox.Location = new Point(220, 170);
            sTextBox.Name = "sTextBox";
            sTextBox.PlaceholderText = "0,0";
            sTextBox.Size = new Size(286, 27);
            sTextBox.TabIndex = 23;
            sTextBox.Leave += sTextBox_Leave;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 173);
            label5.Name = "label5";
            label5.Size = new Size(116, 19);
            label5.TabIndex = 22;
            label5.Text = "Полная сумма: ";
            // 
            // fpTextBox
            // 
            fpTextBox.Location = new Point(220, 132);
            fpTextBox.Name = "fpTextBox";
            fpTextBox.PlaceholderText = "000000000";
            fpTextBox.Size = new Size(286, 27);
            fpTextBox.TabIndex = 21;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 135);
            label4.Name = "label4";
            label4.Size = new Size(164, 19);
            label4.TabIndex = 20;
            label4.Text = "Фискальный знак ФП: ";
            // 
            // iTextBox
            // 
            iTextBox.Location = new Point(220, 94);
            iTextBox.Name = "iTextBox";
            iTextBox.PlaceholderText = "00000";
            iTextBox.Size = new Size(286, 27);
            iTextBox.TabIndex = 19;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 97);
            label3.Name = "label3";
            label3.Size = new Size(121, 19);
            label3.TabIndex = 18;
            label3.Text = "Номер чека ФД: ";
            // 
            // fnTextBox
            // 
            fnTextBox.Location = new Point(220, 56);
            fnTextBox.Name = "fnTextBox";
            fnTextBox.PlaceholderText = "0000000000000000";
            fnTextBox.Size = new Size(286, 27);
            fnTextBox.TabIndex = 17;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 59);
            label2.Name = "label2";
            label2.Size = new Size(87, 19);
            label2.TabIndex = 16;
            label2.Text = "Номер ФН: ";
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
            // GetFromUserQRDataForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(523, 337);
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
            Margin = new Padding(3, 4, 3, 4);
            MaximumSize = new Size(539, 376);
            MinimumSize = new Size(539, 376);
            Name = "GetFromUserQRDataForm";
            Text = "Получение чека через его данные";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button cancelButton;
        private Button okButton;
        private ComboBox nComboBox;
        private Label label7;
        private TextBox tTextBox;
        private Label label6;
        private TextBox sTextBox;
        private Label label5;
        private TextBox fpTextBox;
        private Label label4;
        private TextBox iTextBox;
        private Label label3;
        private TextBox fnTextBox;
        private Label label2;
        private Label label1;
    }
}