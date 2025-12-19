namespace PersonalFinancialManager.source.Forms
{
    partial class UserTokenForm
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
            label1 = new Label();
            apiInputTextBox = new TextBox();
            cancelButton = new Button();
            okButton = new Button();
            SuspendLayout();
            // 
            // infoTextBox
            // 
            infoTextBox.BackColor = Color.White;
            infoTextBox.BorderStyle = BorderStyle.None;
            infoTextBox.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            infoTextBox.Location = new Point(12, 12);
            infoTextBox.Name = "infoTextBox";
            infoTextBox.ReadOnly = true;
            infoTextBox.Size = new Size(470, 80);
            infoTextBox.TabIndex = 1;
            infoTextBox.Text = "Такого API ключа не существует. Необходимо:\n1. зарегистрироваться на сайте proverkacheka.com\n2. в личном кабинете получить API ключ.\n";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 12F);
            label1.Location = new Point(12, 101);
            label1.Name = "label1";
            label1.Size = new Size(86, 20);
            label1.TabIndex = 2;
            label1.Text = "API ключ: ";
            // 
            // apiInputTextBox
            // 
            apiInputTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            apiInputTextBox.Location = new Point(104, 98);
            apiInputTextBox.MaxLength = 300;
            apiInputTextBox.Name = "apiInputTextBox";
            apiInputTextBox.Size = new Size(378, 26);
            apiInputTextBox.TabIndex = 3;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(240, 130);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(118, 33);
            cancelButton.TabIndex = 31;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // okButton
            // 
            okButton.Location = new Point(364, 130);
            okButton.Name = "okButton";
            okButton.Size = new Size(118, 33);
            okButton.TabIndex = 30;
            okButton.Text = "ОК";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // UserTokenForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(505, 174);
            ControlBox = false;
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(apiInputTextBox);
            Controls.Add(label1);
            Controls.Add(infoTextBox);
            MaximizeBox = false;
            MaximumSize = new Size(521, 213);
            MinimizeBox = false;
            MinimumSize = new Size(521, 213);
            Name = "UserTokenForm";
            Text = "API ключ";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox infoTextBox;
        private Label label1;
        private TextBox apiInputTextBox;
        private Button cancelButton;
        private Button okButton;
    }
}