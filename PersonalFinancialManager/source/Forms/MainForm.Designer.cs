using System.Drawing;
using Button = System.Windows.Forms.Button;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;
using SizeF = System.Drawing.SizeF;

namespace PersonalFinancialManager.source.Forms
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
            databaseWindowTreeView = new TreeView();
            databaseContextMenuStrip = new ContextMenuStrip(components);
            deleteReceiptFromDatabaseToolStripMenuItem = new ToolStripMenuItem();
            loadQRCodesToolStripMenuItem = new ToolStripMenuItem();
            loadQRCodesImagesToolStripMenuItem = new ToolStripMenuItem();
            добавитьВРучнуюToolStripMenuItem = new ToolStripMenuItem();
            addUserReceiptToolStripMenuItem = new ToolStripMenuItem();
            loadUsingDataReceiptToolStripMenuItem = new ToolStripMenuItem();
            loadUsingDataStringReceiptToolStripMenuItem = new ToolStripMenuItem();
            changeToolStripMenuItem = new ToolStripMenuItem();
            changeAPIToolStripMenuItem = new ToolStripMenuItem();
            changeReceiptToolStripMenuItem = new ToolStripMenuItem();
            changeProductCategoryToolStripMenuItem = new ToolStripMenuItem();
            mainTabControl = new TabControl();
            mainPage = new TabPage();
            sortDatabaseButton = new Button();
            currentDatabaseConditionTextBox = new TextBox();
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
            allStatisticPage = new TabPage();
            panel3 = new Panel();
            allStatisticPageCategoriesListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            label16 = new Label();
            panel2 = new Panel();
            allStatisticPagePastMonthTextBox = new TextBox();
            label13 = new Label();
            allStatisticPagePastYearTextBox = new TextBox();
            label14 = new Label();
            panel1 = new Panel();
            allStatisticPageMonthTextBox = new TextBox();
            label12 = new Label();
            allStatisticPageYearTextBox = new TextBox();
            label11 = new Label();
            allStatisticPageChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel4 = new Panel();
            allStatisticPageTotalYearSumValueLabel = new Label();
            allStatisticPageDiffBtwPreviousYearsDateLabel = new Label();
            allStatisticPageTotalYearSumDateLabel = new Label();
            allStatisticPageTotal_2x_PreviousMonthSumDateLabel = new Label();
            allStatisticPageTotalPreviousYearSumDateLabel = new Label();
            allStatisticPageDiffBtwPreviousMonthesValueLabel = new Label();
            allStatisticPageTotalPreviousYearSumValueLabel = new Label();
            allStatisticPageDiffBtwPreviousMonthesDateLabel = new Label();
            allStatisticPageTotalMonthSumDateLabel = new Label();
            allStatisticPageDiffBtwPreviousYearsValueLabel = new Label();
            allStatisticPageTotalMonthSumValueLabel = new Label();
            allStatisticPageTotal_2x_PreviousMonthSumValueLabel = new Label();
            allStatisticPageTotalPreviousMonthSumDateLabel = new Label();
            allStatisticPageTotal_2x_PreviousYearSumValueLabel = new Label();
            allStatisticPageTotalPreviousMonthSumValueLabel = new Label();
            allStatisticPageTotal_2x_PreviousYearSumDateLabel = new Label();
            allStatisticPageDiffBtwYearsDateLabel = new Label();
            allStatisticPageDiffBtwYearsValueLabel = new Label();
            allStatisticPageDiffBtwMonthesValueLabel = new Label();
            allStatisticPageDiffBtwMonthesDateLabel = new Label();
            databaseContextMenuStrip.SuspendLayout();
            mainTabControl.SuspendLayout();
            mainPage.SuspendLayout();
            yearChartPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)yearChart).BeginInit();
            monthChartPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)monthChart).BeginInit();
            specialChartPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)specialChart).BeginInit();
            allStatisticPage.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)allStatisticPageChart).BeginInit();
            panel4.SuspendLayout();
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
            // databaseWindowTreeView
            // 
            databaseWindowTreeView.BackColor = Color.White;
            databaseWindowTreeView.ContextMenuStrip = databaseContextMenuStrip;
            databaseWindowTreeView.Font = new Font("Calibri", 12F);
            databaseWindowTreeView.HotTracking = true;
            databaseWindowTreeView.Location = new Point(8, 42);
            databaseWindowTreeView.Name = "databaseWindowTreeView";
            databaseWindowTreeView.Size = new Size(1091, 511);
            databaseWindowTreeView.TabIndex = 2;
            // 
            // databaseContextMenuStrip
            // 
            databaseContextMenuStrip.Font = new Font("Calibri", 12F);
            databaseContextMenuStrip.Items.AddRange(new ToolStripItem[] { deleteReceiptFromDatabaseToolStripMenuItem, loadQRCodesToolStripMenuItem, changeToolStripMenuItem });
            databaseContextMenuStrip.Name = "contextMenuStrip1";
            databaseContextMenuStrip.Size = new Size(181, 76);
            // 
            // deleteReceiptFromDatabaseToolStripMenuItem
            // 
            deleteReceiptFromDatabaseToolStripMenuItem.Name = "deleteReceiptFromDatabaseToolStripMenuItem";
            deleteReceiptFromDatabaseToolStripMenuItem.Size = new Size(180, 24);
            deleteReceiptFromDatabaseToolStripMenuItem.Text = "Удалить чек";
            deleteReceiptFromDatabaseToolStripMenuItem.Click += deleteReceiptFromDatabase_Click;
            // 
            // loadQRCodesToolStripMenuItem
            // 
            loadQRCodesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadQRCodesImagesToolStripMenuItem, добавитьВРучнуюToolStripMenuItem });
            loadQRCodesToolStripMenuItem.Name = "loadQRCodesToolStripMenuItem";
            loadQRCodesToolStripMenuItem.Size = new Size(180, 24);
            loadQRCodesToolStripMenuItem.Text = "Добавить чеки";
            // 
            // loadQRCodesImagesToolStripMenuItem
            // 
            loadQRCodesImagesToolStripMenuItem.Name = "loadQRCodesImagesToolStripMenuItem";
            loadQRCodesImagesToolStripMenuItem.Size = new Size(211, 24);
            loadQRCodesImagesToolStripMenuItem.Text = "Загрузить QR коды";
            loadQRCodesImagesToolStripMenuItem.Click += addQRCodesImagesToolStripMenuItem_Click;
            // 
            // добавитьВРучнуюToolStripMenuItem
            // 
            добавитьВРучнуюToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addUserReceiptToolStripMenuItem, loadUsingDataReceiptToolStripMenuItem, loadUsingDataStringReceiptToolStripMenuItem });
            добавитьВРучнуюToolStripMenuItem.Name = "добавитьВРучнуюToolStripMenuItem";
            добавитьВРучнуюToolStripMenuItem.Size = new Size(211, 24);
            добавитьВРучнуюToolStripMenuItem.Text = "Добавить в ручную";
            // 
            // addUserReceiptToolStripMenuItem
            // 
            addUserReceiptToolStripMenuItem.Name = "addUserReceiptToolStripMenuItem";
            addUserReceiptToolStripMenuItem.Size = new Size(272, 24);
            addUserReceiptToolStripMenuItem.Text = "Свой чек";
            addUserReceiptToolStripMenuItem.Click += addUserReceiptToolStripMenuItem_Click;
            // 
            // loadUsingDataReceiptToolStripMenuItem
            // 
            loadUsingDataReceiptToolStripMenuItem.Name = "loadUsingDataReceiptToolStripMenuItem";
            loadUsingDataReceiptToolStripMenuItem.Size = new Size(272, 24);
            loadUsingDataReceiptToolStripMenuItem.Text = "Данные чека";
            loadUsingDataReceiptToolStripMenuItem.Click += loadUsingDataReceiptToolStripMenuItem_Click;
            // 
            // loadUsingDataStringReceiptToolStripMenuItem
            // 
            loadUsingDataStringReceiptToolStripMenuItem.Name = "loadUsingDataStringReceiptToolStripMenuItem";
            loadUsingDataStringReceiptToolStripMenuItem.Size = new Size(272, 24);
            loadUsingDataStringReceiptToolStripMenuItem.Text = "Через строку с данным чека";
            loadUsingDataStringReceiptToolStripMenuItem.Click += loadUsingDataStringReceiptToolStripMenuItem_Click;
            // 
            // changeToolStripMenuItem
            // 
            changeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { changeAPIToolStripMenuItem, changeReceiptToolStripMenuItem, changeProductCategoryToolStripMenuItem });
            changeToolStripMenuItem.Name = "changeToolStripMenuItem";
            changeToolStripMenuItem.Size = new Size(180, 24);
            changeToolStripMenuItem.Text = "Изменить";
            // 
            // changeAPIToolStripMenuItem
            // 
            changeAPIToolStripMenuItem.Name = "changeAPIToolStripMenuItem";
            changeAPIToolStripMenuItem.Size = new Size(217, 24);
            changeAPIToolStripMenuItem.Text = "Ключ API";
            changeAPIToolStripMenuItem.Click += changeAPIToolStripMenuItem_Click_1;
            // 
            // changeReceiptToolStripMenuItem
            // 
            changeReceiptToolStripMenuItem.Name = "changeReceiptToolStripMenuItem";
            changeReceiptToolStripMenuItem.Size = new Size(217, 24);
            changeReceiptToolStripMenuItem.Text = "Данные чека";
            changeReceiptToolStripMenuItem.Click += changeReceiptToolStripMenuItem_Click;
            // 
            // changeProductCategoryToolStripMenuItem
            // 
            changeProductCategoryToolStripMenuItem.Name = "changeProductCategoryToolStripMenuItem";
            changeProductCategoryToolStripMenuItem.Size = new Size(217, 24);
            changeProductCategoryToolStripMenuItem.Text = "Категорию продукта";
            changeProductCategoryToolStripMenuItem.Click += changeProductCategoryToolStripMenuItem_Click;
            // 
            // mainTabControl
            // 
            mainTabControl.Controls.Add(mainPage);
            mainTabControl.Controls.Add(yearChartPage);
            mainTabControl.Controls.Add(monthChartPage);
            mainTabControl.Controls.Add(specialChartPage);
            mainTabControl.Controls.Add(allStatisticPage);
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
            mainPage.BackColor = Color.WhiteSmoke;
            mainPage.Controls.Add(sortDatabaseButton);
            mainPage.Controls.Add(currentDatabaseConditionTextBox);
            mainPage.Controls.Add(databaseWindowTreeView);
            mainPage.Location = new Point(4, 28);
            mainPage.Name = "mainPage";
            mainPage.Padding = new Padding(3);
            mainPage.Size = new Size(1107, 559);
            mainPage.TabIndex = 0;
            mainPage.Text = "Главная";
            // 
            // sortDatabaseButton
            // 
            sortDatabaseButton.Font = new Font("Calibri", 12F);
            sortDatabaseButton.Location = new Point(972, 9);
            sortDatabaseButton.Name = "sortDatabaseButton";
            sortDatabaseButton.Size = new Size(127, 27);
            sortDatabaseButton.TabIndex = 4;
            sortDatabaseButton.Text = "Сортировать";
            sortDatabaseButton.UseVisualStyleBackColor = true;
            sortDatabaseButton.Click += sortDatabaseButton_Click;
            // 
            // currentDatabaseConditionTextBox
            // 
            currentDatabaseConditionTextBox.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            currentDatabaseConditionTextBox.Location = new Point(8, 9);
            currentDatabaseConditionTextBox.Name = "currentDatabaseConditionTextBox";
            currentDatabaseConditionTextBox.ReadOnly = true;
            currentDatabaseConditionTextBox.Size = new Size(958, 27);
            currentDatabaseConditionTextBox.TabIndex = 3;
            // 
            // yearChartPage
            // 
            yearChartPage.BackColor = Color.WhiteSmoke;
            yearChartPage.Controls.Add(yearChart);
            yearChartPage.Location = new Point(4, 24);
            yearChartPage.Name = "yearChartPage";
            yearChartPage.Padding = new Padding(3);
            yearChartPage.Size = new Size(1107, 563);
            yearChartPage.TabIndex = 1;
            yearChartPage.Text = "График за год";
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
            monthChartPage.BackColor = Color.WhiteSmoke;
            monthChartPage.Controls.Add(monthChart);
            monthChartPage.Location = new Point(4, 24);
            monthChartPage.Name = "monthChartPage";
            monthChartPage.Padding = new Padding(3);
            monthChartPage.Size = new Size(1107, 563);
            monthChartPage.TabIndex = 2;
            monthChartPage.Text = "График за месяц";
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
            specialChartPage.BackColor = Color.WhiteSmoke;
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
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.White;
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
            specialChartSeries3HideCheckBox.BackColor = Color.White;
            specialChartSeries3HideCheckBox.CheckAlign = ContentAlignment.MiddleRight;
            specialChartSeries3HideCheckBox.Location = new Point(952, 514);
            specialChartSeries3HideCheckBox.Name = "specialChartSeries3HideCheckBox";
            specialChartSeries3HideCheckBox.Size = new Size(129, 23);
            specialChartSeries3HideCheckBox.TabIndex = 14;
            specialChartSeries3HideCheckBox.Text = "Скрыть график";
            specialChartSeries3HideCheckBox.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.White;
            label7.Location = new Point(893, 515);
            label7.Name = "label7";
            label7.Size = new Size(53, 19);
            label7.TabIndex = 19;
            label7.Text = "числа.";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.White;
            label8.Location = new Point(737, 515);
            label8.Name = "label8";
            label8.Size = new Size(44, 19);
            label8.TabIndex = 18;
            label8.Text = "— до";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.White;
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
            specialChartSeries2HideCheckBox.BackColor = Color.White;
            specialChartSeries2HideCheckBox.CheckAlign = ContentAlignment.MiddleRight;
            specialChartSeries2HideCheckBox.Location = new Point(952, 488);
            specialChartSeries2HideCheckBox.Name = "specialChartSeries2HideCheckBox";
            specialChartSeries2HideCheckBox.Size = new Size(129, 23);
            specialChartSeries2HideCheckBox.TabIndex = 8;
            specialChartSeries2HideCheckBox.Text = "Скрыть график";
            specialChartSeries2HideCheckBox.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.White;
            label4.Location = new Point(893, 489);
            label4.Name = "label4";
            label4.Size = new Size(53, 19);
            label4.TabIndex = 13;
            label4.Text = "числа.";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.White;
            label5.Location = new Point(737, 489);
            label5.Name = "label5";
            label5.Size = new Size(44, 19);
            label5.TabIndex = 12;
            label5.Text = "— до";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.White;
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
            specialChartSeries1HideCheckBox.BackColor = Color.White;
            specialChartSeries1HideCheckBox.CheckAlign = ContentAlignment.MiddleRight;
            specialChartSeries1HideCheckBox.Location = new Point(952, 462);
            specialChartSeries1HideCheckBox.Name = "specialChartSeries1HideCheckBox";
            specialChartSeries1HideCheckBox.Size = new Size(129, 23);
            specialChartSeries1HideCheckBox.TabIndex = 4;
            specialChartSeries1HideCheckBox.Text = "Скрыть график";
            specialChartSeries1HideCheckBox.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.White;
            label3.Location = new Point(893, 463);
            label3.Name = "label3";
            label3.Size = new Size(53, 19);
            label3.TabIndex = 7;
            label3.Text = "числа.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Location = new Point(737, 463);
            label2.Name = "label2";
            label2.Size = new Size(44, 19);
            label2.TabIndex = 6;
            label2.Text = "— до";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
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
            // allStatisticPage
            // 
            allStatisticPage.BackColor = Color.WhiteSmoke;
            allStatisticPage.Controls.Add(panel3);
            allStatisticPage.Controls.Add(panel2);
            allStatisticPage.Controls.Add(panel1);
            allStatisticPage.Controls.Add(allStatisticPageChart);
            allStatisticPage.Controls.Add(panel4);
            allStatisticPage.Location = new Point(4, 24);
            allStatisticPage.Name = "allStatisticPage";
            allStatisticPage.Padding = new Padding(3);
            allStatisticPage.Size = new Size(1107, 563);
            allStatisticPage.TabIndex = 4;
            allStatisticPage.Text = "Общая статистика";
            // 
            // panel3
            // 
            panel3.BackColor = Color.WhiteSmoke;
            panel3.Controls.Add(allStatisticPageCategoriesListView);
            panel3.Controls.Add(label16);
            panel3.Location = new Point(11, 370);
            panel3.Margin = new Padding(6);
            panel3.Name = "panel3";
            panel3.Size = new Size(499, 181);
            panel3.TabIndex = 10;
            // 
            // allStatisticPageCategoriesListView
            // 
            allStatisticPageCategoriesListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            allStatisticPageCategoriesListView.FullRowSelect = true;
            allStatisticPageCategoriesListView.GridLines = true;
            allStatisticPageCategoriesListView.Location = new Point(3, 32);
            allStatisticPageCategoriesListView.Name = "allStatisticPageCategoriesListView";
            allStatisticPageCategoriesListView.Size = new Size(493, 146);
            allStatisticPageCategoriesListView.TabIndex = 38;
            allStatisticPageCategoriesListView.UseCompatibleStateImageBehavior = false;
            allStatisticPageCategoriesListView.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Категория";
            columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Сумма";
            columnHeader2.Width = 120;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Calibri", 14F);
            label16.Location = new Point(177, 3);
            label16.Margin = new Padding(3);
            label16.Name = "label16";
            label16.Size = new Size(122, 23);
            label16.TabIndex = 37;
            label16.Text = "Топ категорий";
            // 
            // panel2
            // 
            panel2.BackColor = Color.WhiteSmoke;
            panel2.Controls.Add(allStatisticPagePastMonthTextBox);
            panel2.Controls.Add(label13);
            panel2.Controls.Add(allStatisticPagePastYearTextBox);
            panel2.Controls.Add(label14);
            panel2.Location = new Point(292, 9);
            panel2.Margin = new Padding(6);
            panel2.Name = "panel2";
            panel2.Size = new Size(443, 46);
            panel2.TabIndex = 10;
            // 
            // allStatisticPagePastMonthTextBox
            // 
            allStatisticPagePastMonthTextBox.BackColor = Color.Silver;
            allStatisticPagePastMonthTextBox.BorderStyle = BorderStyle.None;
            allStatisticPagePastMonthTextBox.Font = new Font("Calibri", 14F);
            allStatisticPagePastMonthTextBox.Location = new Point(383, 11);
            allStatisticPagePastMonthTextBox.MaxLength = 2;
            allStatisticPagePastMonthTextBox.Name = "allStatisticPagePastMonthTextBox";
            allStatisticPagePastMonthTextBox.PlaceholderText = "00";
            allStatisticPagePastMonthTextBox.Size = new Size(47, 23);
            allStatisticPagePastMonthTextBox.TabIndex = 8;
            allStatisticPagePastMonthTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Calibri", 14F);
            label13.Location = new Point(226, 11);
            label13.Margin = new Padding(0, 0, 3, 0);
            label13.Name = "label13";
            label13.Size = new Size(151, 23);
            label13.TabIndex = 7;
            label13.Text = "Прошлый месяц: ";
            // 
            // allStatisticPagePastYearTextBox
            // 
            allStatisticPagePastYearTextBox.BackColor = Color.Silver;
            allStatisticPagePastYearTextBox.BorderStyle = BorderStyle.None;
            allStatisticPagePastYearTextBox.Font = new Font("Calibri", 14F);
            allStatisticPagePastYearTextBox.Location = new Point(148, 11);
            allStatisticPagePastYearTextBox.MaxLength = 4;
            allStatisticPagePastYearTextBox.Name = "allStatisticPagePastYearTextBox";
            allStatisticPagePastYearTextBox.PlaceholderText = "0000";
            allStatisticPagePastYearTextBox.Size = new Size(54, 23);
            allStatisticPagePastYearTextBox.TabIndex = 6;
            allStatisticPagePastYearTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Calibri", 14F);
            label14.Location = new Point(13, 11);
            label14.Margin = new Padding(0, 0, 3, 0);
            label14.Name = "label14";
            label14.Size = new Size(129, 23);
            label14.TabIndex = 5;
            label14.Text = "Прошлый год: ";
            // 
            // panel1
            // 
            panel1.BackColor = Color.WhiteSmoke;
            panel1.Controls.Add(allStatisticPageMonthTextBox);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(allStatisticPageYearTextBox);
            panel1.Controls.Add(label11);
            panel1.Location = new Point(11, 9);
            panel1.Margin = new Padding(6);
            panel1.Name = "panel1";
            panel1.Size = new Size(269, 46);
            panel1.TabIndex = 9;
            // 
            // allStatisticPageMonthTextBox
            // 
            allStatisticPageMonthTextBox.BackColor = Color.Silver;
            allStatisticPageMonthTextBox.BorderStyle = BorderStyle.None;
            allStatisticPageMonthTextBox.Font = new Font("Calibri", 14F);
            allStatisticPageMonthTextBox.Location = new Point(206, 11);
            allStatisticPageMonthTextBox.MaxLength = 2;
            allStatisticPageMonthTextBox.Name = "allStatisticPageMonthTextBox";
            allStatisticPageMonthTextBox.PlaceholderText = "00";
            allStatisticPageMonthTextBox.Size = new Size(47, 23);
            allStatisticPageMonthTextBox.TabIndex = 8;
            allStatisticPageMonthTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Calibri", 14F);
            label12.Location = new Point(129, 11);
            label12.Margin = new Padding(0, 0, 3, 0);
            label12.Name = "label12";
            label12.Size = new Size(71, 23);
            label12.TabIndex = 7;
            label12.Text = "Месяц: ";
            // 
            // allStatisticPageYearTextBox
            // 
            allStatisticPageYearTextBox.BackColor = Color.Silver;
            allStatisticPageYearTextBox.BorderStyle = BorderStyle.None;
            allStatisticPageYearTextBox.Font = new Font("Calibri", 14F);
            allStatisticPageYearTextBox.Location = new Point(64, 11);
            allStatisticPageYearTextBox.MaxLength = 4;
            allStatisticPageYearTextBox.Name = "allStatisticPageYearTextBox";
            allStatisticPageYearTextBox.PlaceholderText = "0000";
            allStatisticPageYearTextBox.Size = new Size(54, 23);
            allStatisticPageYearTextBox.TabIndex = 6;
            allStatisticPageYearTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Calibri", 14F);
            label11.Location = new Point(13, 11);
            label11.Margin = new Padding(0, 0, 3, 0);
            label11.Name = "label11";
            label11.Size = new Size(45, 23);
            label11.TabIndex = 5;
            label11.Text = "Год: ";
            // 
            // allStatisticPageChart
            // 
            allStatisticPageChart.BackColor = Color.WhiteSmoke;
            allStatisticPageChart.BorderlineColor = Color.WhiteSmoke;
            allStatisticPageChart.BorderlineWidth = 0;
            allStatisticPageChart.Location = new Point(519, 64);
            allStatisticPageChart.Name = "allStatisticPageChart";
            allStatisticPageChart.Size = new Size(580, 487);
            allStatisticPageChart.TabIndex = 36;
            allStatisticPageChart.TabStop = false;
            allStatisticPageChart.Text = "chart1";
            // 
            // panel4
            // 
            panel4.BackColor = Color.WhiteSmoke;
            panel4.Controls.Add(allStatisticPageTotalYearSumValueLabel);
            panel4.Controls.Add(allStatisticPageDiffBtwPreviousYearsDateLabel);
            panel4.Controls.Add(allStatisticPageTotalYearSumDateLabel);
            panel4.Controls.Add(allStatisticPageTotal_2x_PreviousMonthSumDateLabel);
            panel4.Controls.Add(allStatisticPageTotalPreviousYearSumDateLabel);
            panel4.Controls.Add(allStatisticPageDiffBtwPreviousMonthesValueLabel);
            panel4.Controls.Add(allStatisticPageTotalPreviousYearSumValueLabel);
            panel4.Controls.Add(allStatisticPageDiffBtwPreviousMonthesDateLabel);
            panel4.Controls.Add(allStatisticPageTotalMonthSumDateLabel);
            panel4.Controls.Add(allStatisticPageDiffBtwPreviousYearsValueLabel);
            panel4.Controls.Add(allStatisticPageTotalMonthSumValueLabel);
            panel4.Controls.Add(allStatisticPageTotal_2x_PreviousMonthSumValueLabel);
            panel4.Controls.Add(allStatisticPageTotalPreviousMonthSumDateLabel);
            panel4.Controls.Add(allStatisticPageTotal_2x_PreviousYearSumValueLabel);
            panel4.Controls.Add(allStatisticPageTotalPreviousMonthSumValueLabel);
            panel4.Controls.Add(allStatisticPageTotal_2x_PreviousYearSumDateLabel);
            panel4.Controls.Add(allStatisticPageDiffBtwYearsDateLabel);
            panel4.Controls.Add(allStatisticPageDiffBtwYearsValueLabel);
            panel4.Controls.Add(allStatisticPageDiffBtwMonthesValueLabel);
            panel4.Controls.Add(allStatisticPageDiffBtwMonthesDateLabel);
            panel4.Location = new Point(11, 61);
            panel4.Margin = new Padding(6);
            panel4.Name = "panel4";
            panel4.Size = new Size(499, 303);
            panel4.TabIndex = 39;
            // 
            // allStatisticPageTotalYearSumValueLabel
            // 
            allStatisticPageTotalYearSumValueLabel.AutoSize = true;
            allStatisticPageTotalYearSumValueLabel.Font = new Font("Calibri", 14F);
            allStatisticPageTotalYearSumValueLabel.Location = new Point(318, 3);
            allStatisticPageTotalYearSumValueLabel.Margin = new Padding(3);
            allStatisticPageTotalYearSumValueLabel.Name = "allStatisticPageTotalYearSumValueLabel";
            allStatisticPageTotalYearSumValueLabel.Size = new Size(102, 23);
            allStatisticPageTotalYearSumValueLabel.TabIndex = 11;
            allStatisticPageTotalYearSumValueLabel.Text = "32 152 руб.";
            // 
            // allStatisticPageDiffBtwPreviousYearsDateLabel
            // 
            allStatisticPageDiffBtwPreviousYearsDateLabel.AutoSize = true;
            allStatisticPageDiffBtwPreviousYearsDateLabel.Font = new Font("Calibri", 14F);
            allStatisticPageDiffBtwPreviousYearsDateLabel.Location = new Point(9, 248);
            allStatisticPageDiffBtwPreviousYearsDateLabel.Margin = new Padding(3);
            allStatisticPageDiffBtwPreviousYearsDateLabel.Name = "allStatisticPageDiffBtwPreviousYearsDateLabel";
            allStatisticPageDiffBtwPreviousYearsDateLabel.Size = new Size(250, 23);
            allStatisticPageDiffBtwPreviousYearsDateLabel.TabIndex = 35;
            allStatisticPageDiffBtwPreviousYearsDateLabel.Text = "Разница между 0000 и 0000:  ";
            // 
            // allStatisticPageTotalYearSumDateLabel
            // 
            allStatisticPageTotalYearSumDateLabel.AutoSize = true;
            allStatisticPageTotalYearSumDateLabel.Font = new Font("Calibri", 14F);
            allStatisticPageTotalYearSumDateLabel.Location = new Point(9, 3);
            allStatisticPageTotalYearSumDateLabel.Margin = new Padding(3);
            allStatisticPageTotalYearSumDateLabel.Name = "allStatisticPageTotalYearSumDateLabel";
            allStatisticPageTotalYearSumDateLabel.Size = new Size(222, 23);
            allStatisticPageTotalYearSumDateLabel.TabIndex = 10;
            allStatisticPageTotalYearSumDateLabel.Text = "Общая сумма за ____ год: ";
            // 
            // allStatisticPageTotal_2x_PreviousMonthSumDateLabel
            // 
            allStatisticPageTotal_2x_PreviousMonthSumDateLabel.AutoSize = true;
            allStatisticPageTotal_2x_PreviousMonthSumDateLabel.Font = new Font("Calibri", 14F);
            allStatisticPageTotal_2x_PreviousMonthSumDateLabel.Location = new Point(9, 219);
            allStatisticPageTotal_2x_PreviousMonthSumDateLabel.Margin = new Padding(3);
            allStatisticPageTotal_2x_PreviousMonthSumDateLabel.Name = "allStatisticPageTotal_2x_PreviousMonthSumDateLabel";
            allStatisticPageTotal_2x_PreviousMonthSumDateLabel.Size = new Size(273, 23);
            allStatisticPageTotal_2x_PreviousMonthSumDateLabel.TabIndex = 34;
            allStatisticPageTotal_2x_PreviousMonthSumDateLabel.Text = "Общая сумма за 00.0000 месяц: ";
            // 
            // allStatisticPageTotalPreviousYearSumDateLabel
            // 
            allStatisticPageTotalPreviousYearSumDateLabel.AutoSize = true;
            allStatisticPageTotalPreviousYearSumDateLabel.Font = new Font("Calibri", 14F);
            allStatisticPageTotalPreviousYearSumDateLabel.Location = new Point(9, 61);
            allStatisticPageTotalPreviousYearSumDateLabel.Margin = new Padding(3);
            allStatisticPageTotalPreviousYearSumDateLabel.Name = "allStatisticPageTotalPreviousYearSumDateLabel";
            allStatisticPageTotalPreviousYearSumDateLabel.Size = new Size(226, 23);
            allStatisticPageTotalPreviousYearSumDateLabel.TabIndex = 12;
            allStatisticPageTotalPreviousYearSumDateLabel.Text = "Общая сумма за 0000 год: ";
            // 
            // allStatisticPageDiffBtwPreviousMonthesValueLabel
            // 
            allStatisticPageDiffBtwPreviousMonthesValueLabel.AutoSize = true;
            allStatisticPageDiffBtwPreviousMonthesValueLabel.Font = new Font("Calibri", 14F);
            allStatisticPageDiffBtwPreviousMonthesValueLabel.Location = new Point(318, 277);
            allStatisticPageDiffBtwPreviousMonthesValueLabel.Margin = new Padding(3);
            allStatisticPageDiffBtwPreviousMonthesValueLabel.Name = "allStatisticPageDiffBtwPreviousMonthesValueLabel";
            allStatisticPageDiffBtwPreviousMonthesValueLabel.Size = new Size(102, 23);
            allStatisticPageDiffBtwPreviousMonthesValueLabel.TabIndex = 33;
            allStatisticPageDiffBtwPreviousMonthesValueLabel.Text = "32 152 руб.";
            // 
            // allStatisticPageTotalPreviousYearSumValueLabel
            // 
            allStatisticPageTotalPreviousYearSumValueLabel.AutoSize = true;
            allStatisticPageTotalPreviousYearSumValueLabel.Font = new Font("Calibri", 14F);
            allStatisticPageTotalPreviousYearSumValueLabel.Location = new Point(318, 61);
            allStatisticPageTotalPreviousYearSumValueLabel.Margin = new Padding(3);
            allStatisticPageTotalPreviousYearSumValueLabel.Name = "allStatisticPageTotalPreviousYearSumValueLabel";
            allStatisticPageTotalPreviousYearSumValueLabel.Size = new Size(102, 23);
            allStatisticPageTotalPreviousYearSumValueLabel.TabIndex = 13;
            allStatisticPageTotalPreviousYearSumValueLabel.Text = "32 152 руб.";
            // 
            // allStatisticPageDiffBtwPreviousMonthesDateLabel
            // 
            allStatisticPageDiffBtwPreviousMonthesDateLabel.AutoSize = true;
            allStatisticPageDiffBtwPreviousMonthesDateLabel.Font = new Font("Calibri", 14F);
            allStatisticPageDiffBtwPreviousMonthesDateLabel.Location = new Point(9, 277);
            allStatisticPageDiffBtwPreviousMonthesDateLabel.Margin = new Padding(3);
            allStatisticPageDiffBtwPreviousMonthesDateLabel.Name = "allStatisticPageDiffBtwPreviousMonthesDateLabel";
            allStatisticPageDiffBtwPreviousMonthesDateLabel.Size = new Size(300, 23);
            allStatisticPageDiffBtwPreviousMonthesDateLabel.TabIndex = 32;
            allStatisticPageDiffBtwPreviousMonthesDateLabel.Text = "Разница между 00.0000 и 00.0000:  ";
            // 
            // allStatisticPageTotalMonthSumDateLabel
            // 
            allStatisticPageTotalMonthSumDateLabel.AutoSize = true;
            allStatisticPageTotalMonthSumDateLabel.Font = new Font("Calibri", 14F);
            allStatisticPageTotalMonthSumDateLabel.Location = new Point(9, 32);
            allStatisticPageTotalMonthSumDateLabel.Margin = new Padding(3);
            allStatisticPageTotalMonthSumDateLabel.Name = "allStatisticPageTotalMonthSumDateLabel";
            allStatisticPageTotalMonthSumDateLabel.Size = new Size(267, 23);
            allStatisticPageTotalMonthSumDateLabel.TabIndex = 14;
            allStatisticPageTotalMonthSumDateLabel.Text = "Общая сумма за __.____ месяц: ";
            // 
            // allStatisticPageDiffBtwPreviousYearsValueLabel
            // 
            allStatisticPageDiffBtwPreviousYearsValueLabel.AutoSize = true;
            allStatisticPageDiffBtwPreviousYearsValueLabel.Font = new Font("Calibri", 14F);
            allStatisticPageDiffBtwPreviousYearsValueLabel.Location = new Point(318, 248);
            allStatisticPageDiffBtwPreviousYearsValueLabel.Margin = new Padding(3);
            allStatisticPageDiffBtwPreviousYearsValueLabel.Name = "allStatisticPageDiffBtwPreviousYearsValueLabel";
            allStatisticPageDiffBtwPreviousYearsValueLabel.Size = new Size(102, 23);
            allStatisticPageDiffBtwPreviousYearsValueLabel.TabIndex = 31;
            allStatisticPageDiffBtwPreviousYearsValueLabel.Text = "32 152 руб.";
            // 
            // allStatisticPageTotalMonthSumValueLabel
            // 
            allStatisticPageTotalMonthSumValueLabel.AutoSize = true;
            allStatisticPageTotalMonthSumValueLabel.Font = new Font("Calibri", 14F);
            allStatisticPageTotalMonthSumValueLabel.Location = new Point(318, 32);
            allStatisticPageTotalMonthSumValueLabel.Margin = new Padding(3);
            allStatisticPageTotalMonthSumValueLabel.Name = "allStatisticPageTotalMonthSumValueLabel";
            allStatisticPageTotalMonthSumValueLabel.Size = new Size(102, 23);
            allStatisticPageTotalMonthSumValueLabel.TabIndex = 15;
            allStatisticPageTotalMonthSumValueLabel.Text = "32 152 руб.";
            // 
            // allStatisticPageTotal_2x_PreviousMonthSumValueLabel
            // 
            allStatisticPageTotal_2x_PreviousMonthSumValueLabel.AutoSize = true;
            allStatisticPageTotal_2x_PreviousMonthSumValueLabel.Font = new Font("Calibri", 14F);
            allStatisticPageTotal_2x_PreviousMonthSumValueLabel.Location = new Point(318, 219);
            allStatisticPageTotal_2x_PreviousMonthSumValueLabel.Margin = new Padding(3);
            allStatisticPageTotal_2x_PreviousMonthSumValueLabel.Name = "allStatisticPageTotal_2x_PreviousMonthSumValueLabel";
            allStatisticPageTotal_2x_PreviousMonthSumValueLabel.Size = new Size(102, 23);
            allStatisticPageTotal_2x_PreviousMonthSumValueLabel.TabIndex = 29;
            allStatisticPageTotal_2x_PreviousMonthSumValueLabel.Text = "32 152 руб.";
            // 
            // allStatisticPageTotalPreviousMonthSumDateLabel
            // 
            allStatisticPageTotalPreviousMonthSumDateLabel.AutoSize = true;
            allStatisticPageTotalPreviousMonthSumDateLabel.Font = new Font("Calibri", 14F);
            allStatisticPageTotalPreviousMonthSumDateLabel.Location = new Point(9, 90);
            allStatisticPageTotalPreviousMonthSumDateLabel.Margin = new Padding(3);
            allStatisticPageTotalPreviousMonthSumDateLabel.Name = "allStatisticPageTotalPreviousMonthSumDateLabel";
            allStatisticPageTotalPreviousMonthSumDateLabel.Size = new Size(273, 23);
            allStatisticPageTotalPreviousMonthSumDateLabel.TabIndex = 16;
            allStatisticPageTotalPreviousMonthSumDateLabel.Text = "Общая сумма за 00.0000 месяц: ";
            // 
            // allStatisticPageTotal_2x_PreviousYearSumValueLabel
            // 
            allStatisticPageTotal_2x_PreviousYearSumValueLabel.AutoSize = true;
            allStatisticPageTotal_2x_PreviousYearSumValueLabel.Font = new Font("Calibri", 14F);
            allStatisticPageTotal_2x_PreviousYearSumValueLabel.Location = new Point(318, 190);
            allStatisticPageTotal_2x_PreviousYearSumValueLabel.Margin = new Padding(3);
            allStatisticPageTotal_2x_PreviousYearSumValueLabel.Name = "allStatisticPageTotal_2x_PreviousYearSumValueLabel";
            allStatisticPageTotal_2x_PreviousYearSumValueLabel.Size = new Size(102, 23);
            allStatisticPageTotal_2x_PreviousYearSumValueLabel.TabIndex = 25;
            allStatisticPageTotal_2x_PreviousYearSumValueLabel.Text = "32 152 руб.";
            // 
            // allStatisticPageTotalPreviousMonthSumValueLabel
            // 
            allStatisticPageTotalPreviousMonthSumValueLabel.AutoSize = true;
            allStatisticPageTotalPreviousMonthSumValueLabel.Font = new Font("Calibri", 14F);
            allStatisticPageTotalPreviousMonthSumValueLabel.Location = new Point(318, 90);
            allStatisticPageTotalPreviousMonthSumValueLabel.Margin = new Padding(3);
            allStatisticPageTotalPreviousMonthSumValueLabel.Name = "allStatisticPageTotalPreviousMonthSumValueLabel";
            allStatisticPageTotalPreviousMonthSumValueLabel.Size = new Size(102, 23);
            allStatisticPageTotalPreviousMonthSumValueLabel.TabIndex = 17;
            allStatisticPageTotalPreviousMonthSumValueLabel.Text = "32 152 руб.";
            // 
            // allStatisticPageTotal_2x_PreviousYearSumDateLabel
            // 
            allStatisticPageTotal_2x_PreviousYearSumDateLabel.AutoSize = true;
            allStatisticPageTotal_2x_PreviousYearSumDateLabel.Font = new Font("Calibri", 14F);
            allStatisticPageTotal_2x_PreviousYearSumDateLabel.Location = new Point(9, 190);
            allStatisticPageTotal_2x_PreviousYearSumDateLabel.Margin = new Padding(3);
            allStatisticPageTotal_2x_PreviousYearSumDateLabel.Name = "allStatisticPageTotal_2x_PreviousYearSumDateLabel";
            allStatisticPageTotal_2x_PreviousYearSumDateLabel.Size = new Size(226, 23);
            allStatisticPageTotal_2x_PreviousYearSumDateLabel.TabIndex = 24;
            allStatisticPageTotal_2x_PreviousYearSumDateLabel.Text = "Общая сумма за 0000 год: ";
            // 
            // allStatisticPageDiffBtwYearsDateLabel
            // 
            allStatisticPageDiffBtwYearsDateLabel.AutoSize = true;
            allStatisticPageDiffBtwYearsDateLabel.Font = new Font("Calibri", 14F);
            allStatisticPageDiffBtwYearsDateLabel.Location = new Point(9, 119);
            allStatisticPageDiffBtwYearsDateLabel.Margin = new Padding(3);
            allStatisticPageDiffBtwYearsDateLabel.Name = "allStatisticPageDiffBtwYearsDateLabel";
            allStatisticPageDiffBtwYearsDateLabel.Size = new Size(250, 23);
            allStatisticPageDiffBtwYearsDateLabel.TabIndex = 18;
            allStatisticPageDiffBtwYearsDateLabel.Text = "Разница между 0000 и 0000:  ";
            // 
            // allStatisticPageDiffBtwYearsValueLabel
            // 
            allStatisticPageDiffBtwYearsValueLabel.AutoSize = true;
            allStatisticPageDiffBtwYearsValueLabel.Font = new Font("Calibri", 14F);
            allStatisticPageDiffBtwYearsValueLabel.Location = new Point(318, 119);
            allStatisticPageDiffBtwYearsValueLabel.Margin = new Padding(3);
            allStatisticPageDiffBtwYearsValueLabel.Name = "allStatisticPageDiffBtwYearsValueLabel";
            allStatisticPageDiffBtwYearsValueLabel.Size = new Size(102, 23);
            allStatisticPageDiffBtwYearsValueLabel.TabIndex = 19;
            allStatisticPageDiffBtwYearsValueLabel.Text = "32 152 руб.";
            // 
            // allStatisticPageDiffBtwMonthesValueLabel
            // 
            allStatisticPageDiffBtwMonthesValueLabel.AutoSize = true;
            allStatisticPageDiffBtwMonthesValueLabel.Font = new Font("Calibri", 14F);
            allStatisticPageDiffBtwMonthesValueLabel.Location = new Point(318, 148);
            allStatisticPageDiffBtwMonthesValueLabel.Margin = new Padding(3);
            allStatisticPageDiffBtwMonthesValueLabel.Name = "allStatisticPageDiffBtwMonthesValueLabel";
            allStatisticPageDiffBtwMonthesValueLabel.Size = new Size(102, 23);
            allStatisticPageDiffBtwMonthesValueLabel.TabIndex = 21;
            allStatisticPageDiffBtwMonthesValueLabel.Text = "32 152 руб.";
            // 
            // allStatisticPageDiffBtwMonthesDateLabel
            // 
            allStatisticPageDiffBtwMonthesDateLabel.AutoSize = true;
            allStatisticPageDiffBtwMonthesDateLabel.Font = new Font("Calibri", 14F);
            allStatisticPageDiffBtwMonthesDateLabel.Location = new Point(9, 148);
            allStatisticPageDiffBtwMonthesDateLabel.Margin = new Padding(3);
            allStatisticPageDiffBtwMonthesDateLabel.Name = "allStatisticPageDiffBtwMonthesDateLabel";
            allStatisticPageDiffBtwMonthesDateLabel.Size = new Size(300, 23);
            allStatisticPageDiffBtwMonthesDateLabel.TabIndex = 20;
            allStatisticPageDiffBtwMonthesDateLabel.Text = "Разница между 00.0000 и 00.0000:  ";
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
            allStatisticPage.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)allStatisticPageChart).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PrintPreviewDialog printPreviewDialog1;
        private TreeView databaseWindowTreeView;
        private TabControl mainTabControl;
        private TabPage mainPage;
        private TabPage yearChartPage;
        private System.Windows.Forms.DataVisualization.Charting.Chart yearChart;
        private TextBox currentDatabaseConditionTextBox;
        private ContextMenuStrip databaseContextMenuStrip;
        private ToolStripMenuItem deleteReceiptFromDatabaseToolStripMenuItem;
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
        private TabPage allStatisticPage;
        private Panel panel1;
        private Label label12;
        private TextBox allStatisticPageMonthTextBox;
        private Label label11;
        private TextBox allStatisticPageYearTextBox;
        private Label allStatisticPageTotalYearSumDateLabel;
        private Label allStatisticPageTotalYearSumValueLabel;
        private Label allStatisticPageTotalPreviousYearSumValueLabel;
        private Label allStatisticPageTotalPreviousYearSumDateLabel;
        private Label allStatisticPageTotalMonthSumValueLabel;
        private Label allStatisticPageTotalMonthSumDateLabel;
        private Label allStatisticPageTotalPreviousMonthSumValueLabel;
        private Label allStatisticPageTotalPreviousMonthSumDateLabel;
        private Label allStatisticPageDiffBtwYearsValueLabel;
        private Label allStatisticPageDiffBtwYearsDateLabel;
        private Label allStatisticPageDiffBtwMonthesValueLabel;
        private Label allStatisticPageDiffBtwMonthesDateLabel;
        private Panel panel2;
        private TextBox allStatisticPagePastMonthTextBox;
        private Label label13;
        private TextBox allStatisticPagePastYearTextBox;
        private Label label14;
        private Label allStatisticPageDiffBtwPreviousMonthesValueLabel;
        private Label allStatisticPageDiffBtwPreviousMonthesDateLabel;
        private Label allStatisticPageDiffBtwPreviousYearsValueLabel;
        private Label label18;
        private Label allStatisticPageTotal_2x_PreviousMonthSumValueLabel;
        private Label label20;
        private Label label21;
        private Label label22;
        private Label allStatisticPageTotal_2x_PreviousYearSumValueLabel;
        private Label allStatisticPageTotal_2x_PreviousYearSumDateLabel;
        private Label label25;
        private Label label26;
        private Label allStatisticPageTotal_2x_PreviousMonthSumDateLabel;
        private Label allStatisticPageDiffBtwPreviousYearsDateLabel;
        private System.Windows.Forms.DataVisualization.Charting.Chart allStatisticPageChart;
        private Label label16;
        private Panel panel3;
        private ListView allStatisticPageCategoriesListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Panel panel4;
        private Button sortDatabaseButton;
        private ToolStripMenuItem loadQRCodesToolStripMenuItem;
        private ToolStripMenuItem loadQRCodesImagesToolStripMenuItem;
        private ToolStripMenuItem добавитьВРучнуюToolStripMenuItem;
        private ToolStripMenuItem addUserReceiptToolStripMenuItem;
        private ToolStripMenuItem loadUsingDataReceiptToolStripMenuItem;
        private ToolStripMenuItem loadUsingDataStringReceiptToolStripMenuItem;
        private ToolStripMenuItem changeToolStripMenuItem;
        private ToolStripMenuItem changeAPIToolStripMenuItem;
        private ToolStripMenuItem changeReceiptToolStripMenuItem;
        private ToolStripMenuItem changeProductCategoryToolStripMenuItem;
    }
}
