namespace PersonalFinancialManager.source.Forms
{
    partial class StringQRDataForm
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
            infoTextBox = new RichTextBox();
            qrStringDataTextBox = new TextBox();
            cancelButton = new Button();
            oKButton = new Button();
            SuspendLayout();
            // 
            // infoTextBox
            // 
            infoTextBox.BackColor = Color.WhiteSmoke;
            infoTextBox.BorderStyle = BorderStyle.None;
            infoTextBox.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            infoTextBox.Location = new Point(14, 15);
            infoTextBox.Margin = new Padding(3, 4, 3, 4);
            infoTextBox.Name = "infoTextBox";
            infoTextBox.ReadOnly = true;
            infoTextBox.ScrollBars = RichTextBoxScrollBars.None;
            infoTextBox.Size = new Size(556, 25);
            infoTextBox.TabIndex = 2;
            infoTextBox.Text = "Введите строку содержащую данные чека:\n";
            // 
            // qrStringDataTextBox
            // 
            qrStringDataTextBox.Font = new Font("Calibri", 12F);
            qrStringDataTextBox.Location = new Point(12, 48);
            qrStringDataTextBox.Margin = new Padding(3, 4, 3, 4);
            qrStringDataTextBox.MaxLength = 300;
            qrStringDataTextBox.Name = "qrStringDataTextBox";
            qrStringDataTextBox.PlaceholderText = "t=20251002T1530&s=1234.56&fn=9281000100001234&i=12345&fp=678901234&n=1";
            qrStringDataTextBox.Size = new Size(556, 27);
            qrStringDataTextBox.TabIndex = 4;
            // 
            // cancelButton
            // 
            cancelButton.Font = new Font("Calibri", 12F);
            cancelButton.Location = new Point(274, 83);
            cancelButton.Margin = new Padding(3, 4, 3, 4);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(145, 29);
            cancelButton.TabIndex = 5;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // oKButton
            // 
            oKButton.Font = new Font("Calibri", 12F);
            oKButton.Location = new Point(425, 83);
            oKButton.Margin = new Padding(3, 4, 3, 4);
            oKButton.Name = "oKButton";
            oKButton.Size = new Size(145, 29);
            oKButton.TabIndex = 6;
            oKButton.Text = "ОК";
            oKButton.UseVisualStyleBackColor = true;
            oKButton.Click += oKButton_Click;
            // 
            // GetQRDataStringForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 123);
            Controls.Add(oKButton);
            Controls.Add(cancelButton);
            Controls.Add(qrStringDataTextBox);
            Controls.Add(infoTextBox);
            Font = new Font("Calibri", 12F);
            Margin = new Padding(3, 4, 3, 4);
            MaximumSize = new Size(600, 162);
            MinimumSize = new Size(600, 162);
            Name = "GetQRDataStringForm";
            Text = "Запрос чека через строку данных";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox infoTextBox;
        private TextBox qrStringDataTextBox;
        private Button cancelButton;
        private Button oKButton;
    }
}