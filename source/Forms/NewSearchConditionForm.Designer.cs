namespace PersonalFinancialManager.source.Forms
{
    partial class NewSearchConditionForm
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
            okButton = new Button();
            conditionsTreeView = new TreeView();
            conditionsTreeViewContextMenuStrip = new ContextMenuStrip(components);
            changeToolStripMenuItem = new ToolStripMenuItem();
            addConditionANDToolStripMenuItem = new ToolStripMenuItem();
            addConditionORToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            cancelButton = new Button();
            entityComboBox = new ComboBox();
            label1 = new Label();
            clearTreeConditionsButton = new Button();
            conditionsTreeViewContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // okButton
            // 
            okButton.Location = new Point(803, 391);
            okButton.Margin = new Padding(3, 4, 3, 4);
            okButton.Name = "okButton";
            okButton.Size = new Size(122, 37);
            okButton.TabIndex = 30;
            okButton.Text = "ОК";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // conditionsTreeView
            // 
            conditionsTreeView.ContextMenuStrip = conditionsTreeViewContextMenuStrip;
            conditionsTreeView.Location = new Point(12, 13);
            conditionsTreeView.Margin = new Padding(3, 4, 3, 4);
            conditionsTreeView.Name = "conditionsTreeView";
            conditionsTreeView.Size = new Size(913, 370);
            conditionsTreeView.TabIndex = 31;
            // 
            // conditionsTreeViewContextMenuStrip
            // 
            conditionsTreeViewContextMenuStrip.Items.AddRange(new ToolStripItem[] { changeToolStripMenuItem, addConditionANDToolStripMenuItem, addConditionORToolStripMenuItem, deleteToolStripMenuItem });
            conditionsTreeViewContextMenuStrip.Name = "conditionsTreeViewContextMenuStrip";
            conditionsTreeViewContextMenuStrip.Size = new Size(191, 92);
            // 
            // changeToolStripMenuItem
            // 
            changeToolStripMenuItem.Name = "changeToolStripMenuItem";
            changeToolStripMenuItem.Size = new Size(190, 22);
            changeToolStripMenuItem.Text = "Изменить";
            changeToolStripMenuItem.Click += changeToolStripMenuItem_Click;
            // 
            // addConditionANDToolStripMenuItem
            // 
            addConditionANDToolStripMenuItem.Name = "addConditionANDToolStripMenuItem";
            addConditionANDToolStripMenuItem.Size = new Size(190, 22);
            addConditionANDToolStripMenuItem.Text = "Добавить, связь И";
            addConditionANDToolStripMenuItem.Click += addConditionANDToolStripMenuItem_Click;
            // 
            // addConditionORToolStripMenuItem
            // 
            addConditionORToolStripMenuItem.Name = "addConditionORToolStripMenuItem";
            addConditionORToolStripMenuItem.Size = new Size(190, 22);
            addConditionORToolStripMenuItem.Text = "Добавить, связь ИЛИ";
            addConditionORToolStripMenuItem.Click += addConditionORToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(190, 22);
            deleteToolStripMenuItem.Text = "Удалить";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(675, 391);
            cancelButton.Margin = new Padding(3, 4, 3, 4);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(122, 37);
            cancelButton.TabIndex = 32;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // entityComboBox
            // 
            entityComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            entityComboBox.FormattingEnabled = true;
            entityComboBox.Location = new Point(192, 397);
            entityComboBox.Name = "entityComboBox";
            entityComboBox.Size = new Size(168, 27);
            entityComboBox.TabIndex = 33;
            entityComboBox.TextChanged += entityComboBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 400);
            label1.Name = "label1";
            label1.Size = new Size(174, 19);
            label1.TabIndex = 34;
            label1.Text = "Сортировать таблицу с: ";
            // 
            // clearTreeConditionsButton
            // 
            clearTreeConditionsButton.Location = new Point(547, 391);
            clearTreeConditionsButton.Margin = new Padding(3, 4, 3, 4);
            clearTreeConditionsButton.Name = "clearTreeConditionsButton";
            clearTreeConditionsButton.Size = new Size(122, 37);
            clearTreeConditionsButton.TabIndex = 35;
            clearTreeConditionsButton.Text = "Очистить";
            clearTreeConditionsButton.UseVisualStyleBackColor = true;
            clearTreeConditionsButton.Click += clearTreeConditionsButton_Click;
            // 
            // NewSearchConditionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(937, 439);
            Controls.Add(clearTreeConditionsButton);
            Controls.Add(label1);
            Controls.Add(entityComboBox);
            Controls.Add(cancelButton);
            Controls.Add(conditionsTreeView);
            Controls.Add(okButton);
            Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(3, 4, 3, 4);
            MaximumSize = new Size(953, 478);
            MinimumSize = new Size(953, 478);
            Name = "NewSearchConditionForm";
            Text = "Новый фильтр";
            conditionsTreeViewContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button okButton;
        private TreeView conditionsTreeView;
        private ContextMenuStrip conditionsTreeViewContextMenuStrip;
        private ToolStripMenuItem changeToolStripMenuItem;
        private ToolStripMenuItem addConditionANDToolStripMenuItem;
        private ToolStripMenuItem addConditionORToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private Button cancelButton;
        private ComboBox entityComboBox;
        private Label label1;
        private Button clearTreeConditionsButton;
    }
}