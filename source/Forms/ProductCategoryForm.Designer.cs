namespace PersonalFinancialManager.source.Forms
{
    partial class ProductCategoryForm
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
            categoryComboBox = new ComboBox();
            cancelButton = new Button();
            okButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 25);
            label1.Name = "label1";
            label1.Size = new Size(87, 19);
            label1.TabIndex = 0;
            label1.Text = "Категория: ";
            // 
            // categoryComboBox
            // 
            categoryComboBox.FormattingEnabled = true;
            categoryComboBox.Location = new Point(105, 22);
            categoryComboBox.Margin = new Padding(3, 4, 3, 4);
            categoryComboBox.Name = "categoryComboBox";
            categoryComboBox.Size = new Size(373, 27);
            categoryComboBox.TabIndex = 43;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(236, 56);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(118, 33);
            cancelButton.TabIndex = 54;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // okButton
            // 
            okButton.Location = new Point(360, 56);
            okButton.Name = "okButton";
            okButton.Size = new Size(118, 33);
            okButton.TabIndex = 53;
            okButton.Text = "ОК";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // ProductCategoryForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(490, 98);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(categoryComboBox);
            Controls.Add(label1);
            Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(3, 4, 3, 4);
            MaximumSize = new Size(506, 137);
            MinimumSize = new Size(506, 137);
            Name = "ProductCategoryForm";
            Text = "Категория продукта";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox categoryComboBox;
        private Button cancelButton;
        private Button okButton;
    }
}