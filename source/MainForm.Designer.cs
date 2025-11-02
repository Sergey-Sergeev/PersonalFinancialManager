using System.Drawing;
using Button = System.Windows.Forms.Button;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;
using SizeF = System.Drawing.SizeF;

namespace PersonalFinancialManager
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            printPreviewDialog1 = new PrintPreviewDialog();
            loadQRCodesButton = new Button();
            databaseWindow = new TreeView();
            databaseContextMenuStrip = new ContextMenuStrip(components);
            deleteReceiptFromDatabase = new ToolStripMenuItem();
            mainTabControl = new TabControl();
            mainPage = new TabPage();
            textBox1 = new TextBox();
            yearChartPage = new TabPage();
            yearChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            monthChartPage = new TabPage();
            monthChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            specialChartPage = new TabPage();
            specialChartIntervalComboBox = new ComboBox();
            label10 = new Label();
            specialChartSeries3HideCheckBox = new CheckBox();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            specialChartSeries3DateUntilTextBox = new TextBox();
            specialChartSeries3DateFromTextBox = new TextBox();
            specialChartSeries2HideCheckBox = new CheckBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            specialChartSeries2DateUntilTextBox = new TextBox();
            specialChartSeries2DateFromTextBox = new TextBox();
            specialChartSeries1HideCheckBox = new CheckBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            specialChartSeries1DateUntilTextBox = new TextBox();
            specialChartSeries1DateFromTextBox = new TextBox();
            specialChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            databaseContextMenuStrip.SuspendLayout();
            mainTabControl.SuspendLayout();
            mainPage.SuspendLayout();
            yearChartPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)yearChart).BeginInit();
            monthChartPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)monthChart).BeginInit();
            specialChartPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)specialChart).BeginInit();
            SuspendLayout();
            // 
            // printPreviewDialog1
            // 
            printPreviewDialog1.AutoScrollMargin = new Size(0, 0);
            printPreviewDialog1.AutoScrollMinSize = new Size(0, 0);
            printPreviewDialog1.ClientSize = new Size(400, 300);
            printPreviewDialog1.Enabled = true;
            printPreviewDialog1.Icon = (Icon)resources.GetObject("printPreviewDialog1.Icon");
            printPreviewDialog1.Name = "printPreviewDialog1";
            printPreviewDialog1.Visible = false;
            // 
            // loadQRCodesButton
            // 
            loadQRCodesButton.Font = new Font("Calibri", 12F);
            loadQRCodesButton.Location = new Point(6, 6);
            loadQRCodesButton.Name = "loadQRCodesButton";
            loadQRCodesButton.Size = new Size(127, 30);
            loadQRCodesButton.TabIndex = 1;
            loadQRCodesButton.Text = "Загрузить QR код";
            loadQRCodesButton.UseVisualStyleBackColor = true;
            loadQRCodesButton.Click += loadQRCodesButton_Click;
            // 
            // databaseWindow
            // 
            databaseWindow.BackColor = Color.WhiteSmoke;
            databaseWindow.ContextMenuStrip = databaseContextMenuStrip;
            databaseWindow.Font = new Font("Calibri", 12F);
            databaseWindow.Location = new Point(285, 42);
            databaseWindow.Name = "databaseWindow";
            databaseWindow.Size = new Size(816, 511);
            databaseWindow.TabIndex = 2;
            // 
            // databaseContextMenuStrip
            // 
            databaseContextMenuStrip.Items.AddRange(new ToolStripItem[] { deleteReceiptFromDatabase });
            databaseContextMenuStrip.Name = "contextMenuStrip1";
            databaseContextMenuStrip.Size = new Size(141, 26);
            // 
            // deleteReceiptFromDatabase
            // 
            deleteReceiptFromDatabase.Name = "deleteReceiptFromDatabase";
            deleteReceiptFromDatabase.Size = new Size(140, 22);
            deleteReceiptFromDatabase.Text = "Удалить чек";
            deleteReceiptFromDatabase.Click += deleteReceiptFromDatabase_Click;
            // 
            // mainTabControl
            // 
            mainTabControl.Controls.Add(mainPage);
            mainTabControl.Controls.Add(yearChartPage);
            mainTabControl.Controls.Add(monthChartPage);
            mainTabControl.Controls.Add(specialChartPage);
            mainTabControl.Dock = DockStyle.Fill;
            mainTabControl.Location = new Point(0, 0);
            mainTabControl.Margin = new Padding(0);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new Size(1115, 591);
            mainTabControl.TabIndex = 3;
            // 
            // mainPage
            // 
            mainPage.Controls.Add(textBox1);
            mainPage.Controls.Add(loadQRCodesButton);
            mainPage.Controls.Add(databaseWindow);
            mainPage.Location = new Point(4, 28);
            mainPage.Name = "mainPage";
            mainPage.Padding = new Padding(3);
            mainPage.Size = new Size(1107, 559);
            mainPage.TabIndex = 0;
            mainPage.Text = "Главная";
            mainPage.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(285, 9);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(816, 27);
            textBox1.TabIndex = 3;
            // 
            // yearChartPage
            // 
            yearChartPage.Controls.Add(yearChart);
            yearChartPage.Location = new Point(4, 24);
            yearChartPage.Name = "yearChartPage";
            yearChartPage.Padding = new Padding(3);
            yearChartPage.Size = new Size(1107, 563);
            yearChartPage.TabIndex = 1;
            yearChartPage.Text = "График за год";
            yearChartPage.UseVisualStyleBackColor = true;
            // 
            // yearChart
            // 
            yearChart.Dock = DockStyle.Fill;
            yearChart.Location = new Point(3, 3);
            yearChart.Margin = new Padding(0);
            yearChart.Name = "yearChart";
            yearChart.Size = new Size(1101, 557);
            yearChart.TabIndex = 1;
            yearChart.Text = "chart1";
            // 
            // monthChartPage
            // 
            monthChartPage.Controls.Add(monthChart);
            monthChartPage.Location = new Point(4, 24);
            monthChartPage.Name = "monthChartPage";
            monthChartPage.Padding = new Padding(3);
            monthChartPage.Size = new Size(1107, 563);
            monthChartPage.TabIndex = 2;
            monthChartPage.Text = "График за месяц";
            monthChartPage.UseVisualStyleBackColor = true;
            // 
            // monthChart
            // 
            monthChart.Dock = DockStyle.Fill;
            monthChart.Location = new Point(3, 3);
            monthChart.Margin = new Padding(0);
            monthChart.Name = "monthChart";
            monthChart.Size = new Size(1101, 557);
            monthChart.TabIndex = 2;
            monthChart.Text = "chart1";
            // 
            // specialChartPage
            // 
            specialChartPage.Controls.Add(specialChartIntervalComboBox);
            specialChartPage.Controls.Add(label10);
            specialChartPage.Controls.Add(specialChartSeries3HideCheckBox);
            specialChartPage.Controls.Add(label7);
            specialChartPage.Controls.Add(label8);
            specialChartPage.Controls.Add(label9);
            specialChartPage.Controls.Add(specialChartSeries3DateUntilTextBox);
            specialChartPage.Controls.Add(specialChartSeries3DateFromTextBox);
            specialChartPage.Controls.Add(specialChartSeries2HideCheckBox);
            specialChartPage.Controls.Add(label4);
            specialChartPage.Controls.Add(label5);
            specialChartPage.Controls.Add(label6);
            specialChartPage.Controls.Add(specialChartSeries2DateUntilTextBox);
            specialChartPage.Controls.Add(specialChartSeries2DateFromTextBox);
            specialChartPage.Controls.Add(specialChartSeries1HideCheckBox);
            specialChartPage.Controls.Add(label3);
            specialChartPage.Controls.Add(label2);
            specialChartPage.Controls.Add(label1);
            specialChartPage.Controls.Add(specialChartSeries1DateUntilTextBox);
            specialChartPage.Controls.Add(specialChartSeries1DateFromTextBox);
            specialChartPage.Controls.Add(specialChart);
            specialChartPage.Location = new Point(4, 28);
            specialChartPage.Name = "specialChartPage";
            specialChartPage.Padding = new Padding(3);
            specialChartPage.Size = new Size(1107, 559);
            specialChartPage.TabIndex = 3;
            specialChartPage.Text = "Специальный график";
            specialChartPage.UseVisualStyleBackColor = true;
            // 
            // specialChartIntervalComboBox
            // 
            specialChartIntervalComboBox.BackColor = Color.White;
            specialChartIntervalComboBox.DisplayMember = "0";
            specialChartIntervalComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            specialChartIntervalComboBox.FlatStyle = FlatStyle.System;
            specialChartIntervalComboBox.ForeColor = SystemColors.InfoText;
            specialChartIntervalComboBox.FormattingEnabled = true;
            specialChartIntervalComboBox.ImeMode = ImeMode.NoControl;
            specialChartIntervalComboBox.Location = new Point(357, 487);
            specialChartIntervalComboBox.Name = "specialChartIntervalComboBox";
            specialChartIntervalComboBox.Size = new Size(98, 27);
            specialChartIntervalComboBox.TabIndex = 4;
            specialChartIntervalComboBox.TextChanged += specialChartIntervalComboBox_TextChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(357, 464);
            label10.Margin = new Padding(0, 0, 3, 0);
            label10.Name = "label10";
            label10.Size = new Size(82, 19);
            label10.TabIndex = 20;
            label10.Text = "Интервал: ";
            // 
            // specialChartSeries3HideCheckBox
            // 
            specialChartSeries3HideCheckBox.AutoSize = true;
            specialChartSeries3HideCheckBox.CheckAlign = ContentAlignment.MiddleRight;
            specialChartSeries3HideCheckBox.Location = new Point(952, 514);
            specialChartSeries3HideCheckBox.Name = "specialChartSeries3HideCheckBox";
            specialChartSeries3HideCheckBox.Size = new Size(129, 23);
            specialChartSeries3HideCheckBox.TabIndex = 14;
            specialChartSeries3HideCheckBox.Text = "Скрыть график";
            specialChartSeries3HideCheckBox.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(893, 515);
            label7.Name = "label7";
            label7.Size = new Size(53, 19);
            label7.TabIndex = 19;
            label7.Text = "числа.";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(737, 515);
            label8.Name = "label8";
            label8.Size = new Size(44, 19);
            label8.TabIndex = 18;
            label8.Text = "— до";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(472, 515);
            label9.Margin = new Padding(0, 0, 3, 0);
            label9.Name = "label9";
            label9.Size = new Size(153, 19);
            label9.TabIndex = 15;
            label9.Text = "Показать график 3 от";
            // 
            // specialChartSeries3DateUntilTextBox
            // 
            specialChartSeries3DateUntilTextBox.BackColor = Color.WhiteSmoke;
            specialChartSeries3DateUntilTextBox.BorderStyle = BorderStyle.None;
            specialChartSeries3DateUntilTextBox.Font = new Font("Calibri", 12F);
            specialChartSeries3DateUntilTextBox.Location = new Point(787, 516);
            specialChartSeries3DateUntilTextBox.MaxLength = 11;
            specialChartSeries3DateUntilTextBox.Name = "specialChartSeries3DateUntilTextBox";
            specialChartSeries3DateUntilTextBox.PlaceholderText = "00.00.0000";
            specialChartSeries3DateUntilTextBox.Size = new Size(100, 20);
            specialChartSeries3DateUntilTextBox.TabIndex = 17;
            specialChartSeries3DateUntilTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // specialChartSeries3DateFromTextBox
            // 
            specialChartSeries3DateFromTextBox.BackColor = Color.WhiteSmoke;
            specialChartSeries3DateFromTextBox.BorderStyle = BorderStyle.None;
            specialChartSeries3DateFromTextBox.Font = new Font("Calibri", 12F);
            specialChartSeries3DateFromTextBox.Location = new Point(631, 515);
            specialChartSeries3DateFromTextBox.MaxLength = 11;
            specialChartSeries3DateFromTextBox.Name = "specialChartSeries3DateFromTextBox";
            specialChartSeries3DateFromTextBox.PlaceholderText = "00.00.0000";
            specialChartSeries3DateFromTextBox.Size = new Size(100, 20);
            specialChartSeries3DateFromTextBox.TabIndex = 16;
            specialChartSeries3DateFromTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // specialChartSeries2HideCheckBox
            // 
            specialChartSeries2HideCheckBox.AutoSize = true;
            specialChartSeries2HideCheckBox.CheckAlign = ContentAlignment.MiddleRight;
            specialChartSeries2HideCheckBox.Location = new Point(952, 488);
            specialChartSeries2HideCheckBox.Name = "specialChartSeries2HideCheckBox";
            specialChartSeries2HideCheckBox.Size = new Size(129, 23);
            specialChartSeries2HideCheckBox.TabIndex = 8;
            specialChartSeries2HideCheckBox.Text = "Скрыть график";
            specialChartSeries2HideCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(893, 489);
            label4.Name = "label4";
            label4.Size = new Size(53, 19);
            label4.TabIndex = 13;
            label4.Text = "числа.";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(737, 489);
            label5.Name = "label5";
            label5.Size = new Size(44, 19);
            label5.TabIndex = 12;
            label5.Text = "— до";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(472, 489);
            label6.Margin = new Padding(0, 0, 3, 0);
            label6.Name = "label6";
            label6.Size = new Size(153, 19);
            label6.TabIndex = 9;
            label6.Text = "Показать график 2 от";
            // 
            // specialChartSeries2DateUntilTextBox
            // 
            specialChartSeries2DateUntilTextBox.BackColor = Color.WhiteSmoke;
            specialChartSeries2DateUntilTextBox.BorderStyle = BorderStyle.None;
            specialChartSeries2DateUntilTextBox.Font = new Font("Calibri", 12F);
            specialChartSeries2DateUntilTextBox.Location = new Point(787, 490);
            specialChartSeries2DateUntilTextBox.MaxLength = 11;
            specialChartSeries2DateUntilTextBox.Name = "specialChartSeries2DateUntilTextBox";
            specialChartSeries2DateUntilTextBox.PlaceholderText = "00.00.0000";
            specialChartSeries2DateUntilTextBox.Size = new Size(100, 20);
            specialChartSeries2DateUntilTextBox.TabIndex = 11;
            specialChartSeries2DateUntilTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // specialChartSeries2DateFromTextBox
            // 
            specialChartSeries2DateFromTextBox.BackColor = Color.WhiteSmoke;
            specialChartSeries2DateFromTextBox.BorderStyle = BorderStyle.None;
            specialChartSeries2DateFromTextBox.Font = new Font("Calibri", 12F);
            specialChartSeries2DateFromTextBox.Location = new Point(631, 489);
            specialChartSeries2DateFromTextBox.MaxLength = 11;
            specialChartSeries2DateFromTextBox.Name = "specialChartSeries2DateFromTextBox";
            specialChartSeries2DateFromTextBox.PlaceholderText = "00.00.0000";
            specialChartSeries2DateFromTextBox.Size = new Size(100, 20);
            specialChartSeries2DateFromTextBox.TabIndex = 10;
            specialChartSeries2DateFromTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // specialChartSeries1HideCheckBox
            // 
            specialChartSeries1HideCheckBox.AutoSize = true;
            specialChartSeries1HideCheckBox.CheckAlign = ContentAlignment.MiddleRight;
            specialChartSeries1HideCheckBox.Location = new Point(952, 462);
            specialChartSeries1HideCheckBox.Name = "specialChartSeries1HideCheckBox";
            specialChartSeries1HideCheckBox.Size = new Size(129, 23);
            specialChartSeries1HideCheckBox.TabIndex = 4;
            specialChartSeries1HideCheckBox.Text = "Скрыть график";
            specialChartSeries1HideCheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(893, 463);
            label3.Name = "label3";
            label3.Size = new Size(53, 19);
            label3.TabIndex = 7;
            label3.Text = "числа.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(737, 463);
            label2.Name = "label2";
            label2.Size = new Size(44, 19);
            label2.TabIndex = 6;
            label2.Text = "— до";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(472, 463);
            label1.Margin = new Padding(0, 0, 3, 0);
            label1.Name = "label1";
            label1.Size = new Size(153, 19);
            label1.TabIndex = 4;
            label1.Text = "Показать график 1 от";
            // 
            // specialChartSeries1DateUntilTextBox
            // 
            specialChartSeries1DateUntilTextBox.BackColor = Color.WhiteSmoke;
            specialChartSeries1DateUntilTextBox.BorderStyle = BorderStyle.None;
            specialChartSeries1DateUntilTextBox.Font = new Font("Calibri", 12F);
            specialChartSeries1DateUntilTextBox.Location = new Point(787, 464);
            specialChartSeries1DateUntilTextBox.MaxLength = 11;
            specialChartSeries1DateUntilTextBox.Name = "specialChartSeries1DateUntilTextBox";
            specialChartSeries1DateUntilTextBox.PlaceholderText = "00.00.0000";
            specialChartSeries1DateUntilTextBox.Size = new Size(100, 20);
            specialChartSeries1DateUntilTextBox.TabIndex = 5;
            specialChartSeries1DateUntilTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // specialChartSeries1DateFromTextBox
            // 
            specialChartSeries1DateFromTextBox.BackColor = Color.WhiteSmoke;
            specialChartSeries1DateFromTextBox.BorderStyle = BorderStyle.None;
            specialChartSeries1DateFromTextBox.Font = new Font("Calibri", 12F);
            specialChartSeries1DateFromTextBox.Location = new Point(631, 463);
            specialChartSeries1DateFromTextBox.MaxLength = 11;
            specialChartSeries1DateFromTextBox.Name = "specialChartSeries1DateFromTextBox";
            specialChartSeries1DateFromTextBox.PlaceholderText = "00.00.0000";
            specialChartSeries1DateFromTextBox.Size = new Size(100, 20);
            specialChartSeries1DateFromTextBox.TabIndex = 4;
            specialChartSeries1DateFromTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // specialChart
            // 
            specialChart.Dock = DockStyle.Fill;
            specialChart.Location = new Point(3, 3);
            specialChart.Margin = new Padding(0);
            specialChart.Name = "specialChart";
            specialChart.Size = new Size(1101, 553);
            specialChart.TabIndex = 3;
            specialChart.Text = "chart1";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1115, 591);
            Controls.Add(mainTabControl);
            Font = new Font("Calibri", 12F);
            MaximumSize = new Size(1131, 630);
            MinimumSize = new Size(1131, 630);
            Name = "MainForm";
            Text = "Личный Финансовый Менеджер";
            databaseContextMenuStrip.ResumeLayout(false);
            mainTabControl.ResumeLayout(false);
            mainPage.ResumeLayout(false);
            mainPage.PerformLayout();
            yearChartPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)yearChart).EndInit();
            monthChartPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)monthChart).EndInit();
            specialChartPage.ResumeLayout(false);
            specialChartPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)specialChart).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Button loadQRCodesButton;
        private TreeView databaseWindow;
        private TabControl mainTabControl;
        private TabPage mainPage;
        private TabPage yearChartPage;
        private System.Windows.Forms.DataVisualization.Charting.Chart yearChart;
        private TextBox textBox1;
        private ContextMenuStrip databaseContextMenuStrip;
        private ToolStripMenuItem deleteReceiptFromDatabase;
        private TabPage monthChartPage;
        private System.Windows.Forms.DataVisualization.Charting.Chart monthChart;
        private TabPage specialChartPage;
        private System.Windows.Forms.DataVisualization.Charting.Chart specialChart;
        private TextBox specialChartSeries1DateFromTextBox;
        private TextBox specialChartSeries1DateUntilTextBox;
        private Label label3;
        private Label label2;
        private Label label1;
        private CheckBox specialChartSeries1HideCheckBox;
        private CheckBox specialChartSeries3HideCheckBox;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox specialChartSeries3DateUntilTextBox;
        private TextBox specialChartSeries3DateFromTextBox;
        private CheckBox specialChartSeries2HideCheckBox;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox specialChartSeries2DateUntilTextBox;
        private TextBox specialChartSeries2DateFromTextBox;
        private Label label10;
        public ComboBox specialChartIntervalComboBox;
    }
}
