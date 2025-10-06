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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            TreeNode treeNode3 = new TreeNode("1. Конфеты");
            TreeNode treeNode4 = new TreeNode("10.12.2024", new TreeNode[] { treeNode3 });
            printPreviewDialog1 = new PrintPreviewDialog();
            mainChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            loadQRCodesButton = new Button();
            treeView1 = new TreeView();
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
            // mainChart
            // 
            chartArea2.Name = "ChartArea1";
            mainChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            mainChart.Legends.Add(legend2);
            mainChart.Location = new Point(12, 12);
            mainChart.Name = "mainChart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            mainChart.Series.Add(series2);
            mainChart.Size = new Size(665, 403);
            mainChart.TabIndex = 0;
            mainChart.Text = "chart1";
            // 
            // loadQRCodesButton
            // 
            loadQRCodesButton.Location = new Point(22, 444);
            loadQRCodesButton.Name = "loadQRCodesButton";
            loadQRCodesButton.Size = new Size(111, 24);
            loadQRCodesButton.TabIndex = 1;
            loadQRCodesButton.Text = "Загрузить QR код";
            loadQRCodesButton.UseVisualStyleBackColor = true;
            loadQRCodesButton.Click += loadQRCodesButton_Click;
            // 
            // treeView1
            // 
            treeView1.Location = new Point(683, 12);
            treeView1.Name = "treeView1";
            treeNode3.Name = "123";
            treeNode3.Text = "1. Конфеты";
            treeNode4.Name = "10.12.2024";
            treeNode4.Text = "10.12.2024";
            treeView1.Nodes.AddRange(new TreeNode[] { treeNode4 });
            treeView1.Size = new Size(420, 567);
            treeView1.TabIndex = 2;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1115, 591);
            Controls.Add(treeView1);
            Controls.Add(loadQRCodesButton);
            Controls.Add(mainChart);
            Name = "MainForm";
            Text = "Личный Финансовый Менеджер";
            ((System.ComponentModel.ISupportInitialize)mainChart).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.DataVisualization.Charting.Chart mainChart;
        private System.Windows.Forms.Button loadQRCodesButton;
        private TreeView treeView1;
    }
}
