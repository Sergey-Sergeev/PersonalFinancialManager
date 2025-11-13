namespace PersonalFinancialManager.source.Forms
{
    partial class SetCategoriesForeachProductForm
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
            listOfProductsListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            cancelButton = new Button();
            okButton = new Button();
            setAutoButton = new Button();
            SuspendLayout();
            // 
            // listOfProductsListView
            // 
            listOfProductsListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            listOfProductsListView.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            listOfProductsListView.FullRowSelect = true;
            listOfProductsListView.GridLines = true;
            listOfProductsListView.Location = new Point(14, 15);
            listOfProductsListView.Margin = new Padding(3, 4, 3, 4);
            listOfProductsListView.Name = "listOfProductsListView";
            listOfProductsListView.Size = new Size(508, 532);
            listOfProductsListView.TabIndex = 39;
            listOfProductsListView.UseCompatibleStateImageBehavior = false;
            listOfProductsListView.View = View.Details;
            listOfProductsListView.DoubleClick += listOfProductsListView_DoubleClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Продукт";
            columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Категория";
            columnHeader2.Width = 200;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(280, 554);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(118, 33);
            cancelButton.TabIndex = 44;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // okButton
            // 
            okButton.Location = new Point(404, 554);
            okButton.Name = "okButton";
            okButton.Size = new Size(118, 33);
            okButton.TabIndex = 43;
            okButton.Text = "ОК";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // setAutoButton
            // 
            setAutoButton.Location = new Point(14, 554);
            setAutoButton.Name = "setAutoButton";
            setAutoButton.Size = new Size(260, 33);
            setAutoButton.TabIndex = 45;
            setAutoButton.Text = "Определить автоматически";
            setAutoButton.UseVisualStyleBackColor = true;
            setAutoButton.Click += setAutoButton_Click;
            // 
            // SetCategoriesForeachProductForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 595);
            Controls.Add(setAutoButton);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(listOfProductsListView);
            Font = new Font("Calibri", 12F);
            Margin = new Padding(3, 4, 3, 4);
            MaximumSize = new Size(550, 634);
            MinimumSize = new Size(550, 634);
            Name = "SetCategoriesForeachProductForm";
            Text = "Выберите категории для каждого продукта";
            ResumeLayout(false);
        }

        #endregion

        private ListView listOfProductsListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Button cancelButton;
        private Button okButton;
        private Button setAutoButton;
    }
}