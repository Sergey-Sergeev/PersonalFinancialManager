namespace PersonalFinancialManager.source.Forms
{
    partial class GetNewConditionForm
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
            attributeComboBox = new ComboBox();
            operatorComboBox = new ComboBox();
            valueTextBox = new TextBox();
            label1 = new Label();
            okButton = new Button();
            entityTextBox = new TextBox();
            SuspendLayout();
            // 
            // attributeComboBox
            // 
            attributeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            attributeComboBox.FormattingEnabled = true;
            attributeComboBox.Location = new Point(130, 12);
            attributeComboBox.Name = "attributeComboBox";
            attributeComboBox.Size = new Size(368, 27);
            attributeComboBox.TabIndex = 3;
            attributeComboBox.TextChanged += attributeComboBox_TextChanged;
            // 
            // operatorComboBox
            // 
            operatorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            operatorComboBox.Font = new Font("Calibri", 12F, FontStyle.Bold);
            operatorComboBox.FormattingEnabled = true;
            operatorComboBox.Location = new Point(504, 12);
            operatorComboBox.Name = "operatorComboBox";
            operatorComboBox.Size = new Size(49, 27);
            operatorComboBox.TabIndex = 5;
            // 
            // valueTextBox
            // 
            valueTextBox.Location = new Point(559, 12);
            valueTextBox.Name = "valueTextBox";
            valueTextBox.Size = new Size(353, 27);
            valueTextBox.TabIndex = 7;
            valueTextBox.Leave += valueTextBox_Leave;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calibri", 16F);
            label1.Location = new Point(109, 10);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(18, 27);
            label1.TabIndex = 8;
            label1.Text = ":";
            // 
            // okButton
            // 
            okButton.Location = new Point(918, 8);
            okButton.Name = "okButton";
            okButton.Size = new Size(61, 33);
            okButton.TabIndex = 29;
            okButton.Text = "ОК";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // entityTextBox
            // 
            entityTextBox.Location = new Point(6, 12);
            entityTextBox.Name = "entityTextBox";
            entityTextBox.ReadOnly = true;
            entityTextBox.Size = new Size(100, 27);
            entityTextBox.TabIndex = 30;
            // 
            // GetNewConditionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(989, 52);
            Controls.Add(entityTextBox);
            Controls.Add(okButton);
            Controls.Add(label1);
            Controls.Add(valueTextBox);
            Controls.Add(operatorComboBox);
            Controls.Add(attributeComboBox);
            Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(3, 4, 3, 4);
            MaximumSize = new Size(1005, 91);
            MinimumSize = new Size(1005, 91);
            Name = "GetNewConditionForm";
            Text = "Новое условие";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox attributeComboBox;
        private ComboBox operatorComboBox;
        private TextBox valueTextBox;
        private Label label1;
        private Button okButton;
        private TextBox entityTextBox;
    }
}