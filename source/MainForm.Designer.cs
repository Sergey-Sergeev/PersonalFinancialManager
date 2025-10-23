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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            printPreviewDialog1 = new PrintPreviewDialog();
            loadQRCodesButton = new Button();
            databaseWindow = new TreeView();
            tabControl1 = new TabControl();
            mainPage = new TabPage();
            textBox1 = new TextBox();
            tabPage2 = new TabPage();
            mainChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            tabControl1.SuspendLayout();
            mainPage.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainChart).BeginInit();
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
            databaseWindow.Font = new Font("Calibri", 12F);
            databaseWindow.Location = new Point(443, 38);
            databaseWindow.Name = "databaseWindow";
            databaseWindow.Size = new Size(634, 495);
            databaseWindow.TabIndex = 2;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(mainPage);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1091, 567);
            tabControl1.TabIndex = 3;
            // 
            // mainPage
            // 
            mainPage.Controls.Add(textBox1);
            mainPage.Controls.Add(loadQRCodesButton);
            mainPage.Controls.Add(databaseWindow);
            mainPage.Location = new Point(4, 24);
            mainPage.Name = "mainPage";
            mainPage.Padding = new Padding(3);
            mainPage.Size = new Size(1083, 539);
            mainPage.TabIndex = 0;
            mainPage.Text = "Главная";
            mainPage.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(443, 6);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(634, 27);
            textBox1.TabIndex = 3;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(mainChart);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1083, 539);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // mainChart
            // 
            chartArea1.Name = "ChartArea1";
            mainChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            mainChart.Legends.Add(legend1);
            mainChart.Location = new Point(6, 6);
            mainChart.Name = "mainChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            mainChart.Series.Add(series1);
            mainChart.Size = new Size(1071, 527);
            mainChart.TabIndex = 1;
            mainChart.Text = "chart1";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1115, 591);
            Controls.Add(tabControl1);
            MaximumSize = new Size(1131, 630);
            MinimumSize = new Size(1131, 630);
            Name = "MainForm";
            Text = "Личный Финансовый Менеджер";
            tabControl1.ResumeLayout(false);
            mainPage.ResumeLayout(false);
            mainPage.PerformLayout();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainChart).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Button loadQRCodesButton;
        private TreeView databaseWindow;
        private TabControl tabControl1;
        private TabPage mainPage;
        private TabPage tabPage2;
        private System.Windows.Forms.DataVisualization.Charting.Chart mainChart;
        private TextBox textBox1;
    }
}
