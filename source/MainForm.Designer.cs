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
            tabControl1 = new TabControl();
            mainPage = new TabPage();
            textBox1 = new TextBox();
            yearChartPage = new TabPage();
            yearChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            databaseContextMenuStrip.SuspendLayout();
            tabControl1.SuspendLayout();
            mainPage.SuspendLayout();
            yearChartPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)yearChart).BeginInit();
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
            // tabControl1
            // 
            tabControl1.Controls.Add(mainPage);
            tabControl1.Controls.Add(yearChartPage);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1115, 591);
            tabControl1.TabIndex = 3;
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
            yearChartPage.Location = new Point(4, 28);
            yearChartPage.Name = "yearChartPage";
            yearChartPage.Padding = new Padding(3);
            yearChartPage.Size = new Size(1107, 559);
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
            yearChart.Size = new Size(1101, 553);
            yearChart.TabIndex = 1;
            yearChart.Text = "chart1";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1115, 591);
            Controls.Add(tabControl1);
            Font = new Font("Calibri", 12F);
            MaximumSize = new Size(1131, 630);
            MinimumSize = new Size(1131, 630);
            Name = "MainForm";
            Text = "Личный Финансовый Менеджер";
            databaseContextMenuStrip.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            mainPage.ResumeLayout(false);
            mainPage.PerformLayout();
            yearChartPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)yearChart).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Button loadQRCodesButton;
        private TreeView databaseWindow;
        private TabControl tabControl1;
        private TabPage mainPage;
        private TabPage yearChartPage;
        private System.Windows.Forms.DataVisualization.Charting.Chart yearChart;
        private TextBox textBox1;
        private ContextMenuStrip databaseContextMenuStrip;
        private ToolStripMenuItem deleteReceiptFromDatabase;
    }
}
