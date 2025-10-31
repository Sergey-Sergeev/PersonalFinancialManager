using PersonalFinancialManager.source;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.VisualStyles;
using ZXing;
using static PersonalFinancialManager.source.DataService;
using static PersonalFinancialManager.source.JsonServerClass;

namespace PersonalFinancialManager
{
    public partial class MainForm : Form
    {
        const string FILE_FILTER = "Изображения (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|PNG (*.png)|*.png|JPEG (*.jpg)|*.jpg|JPEG (*.jpeg)|*.jpeg";
        private DataService dataService;


        private const int YEAR_CHART_AXIS_Y_INTERVAL_COUNT = 10;
        private const int DEFAULT_YEAR_CHART_AXIS_Y_INTERVAL = 100;

        public MainForm()
        {
            InitializeComponent();

            dataService = DataService.Fabric(out bool isUserAuthorizated);

            if (!isUserAuthorizated)
                AskUserToken(false);

            UpdateAllReceiptsInDatabaseWindow();
            InitializeYearStatisticChart();
        }

        private async void loadQRCodesButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = FILE_FILTER;
            ofd.FilterIndex = 0;
            ofd.Multiselect = true;

            if (ofd.ShowDialog() != DialogResult.Cancel && ofd.FileNames.Length != 0)
            {
                FTSDecodingReceiptsResult result = await dataService.GetReceiptsFromQRCodes(ofd.FileNames);

                if (result.FailDecoding.Count != 0)
                {
                    if (result.FailDecoding.Any((item) => item.Code == FTSDecodingReceiptsResult.FailGettingReceiptData.ErrorCode.IncorrectAPIKey))
                    {
                        AskUserToken(true);
                    }
                    else
                    {
                        new FailGetReceiptsForm(result.FailDecoding).ShowDialog();
                    }
                }

                if (result.Receipts.Count != 0)
                {
                    UpdateAllReceiptsInDatabaseWindow();
                    UpdateYearStatisticChart();
                }
            }
        }

        private void UpdateAllReceiptsInDatabaseWindow()
        {
            databaseWindow.Nodes.Clear();

            foreach (Receipt receipt in dataService.GetReceiptsFromDB())
            {
                List<TreeNode> productsAndTotalPrice = receipt.ListOfProducts.ConvertAll<TreeNode>(
                        (product) => new TreeNode($"{product.Name}\n",
                        new TreeNode[] {
                            new TreeNode($"Категория:  {product.Category}") { Tag = DatabaseWindowTag.Data },
                            new TreeNode($"Цена:  {product.Price}\n"){ Tag = DatabaseWindowTag.Data },
                            new TreeNode($"Количество:  {product.Quantity}\n"){ Tag = DatabaseWindowTag.Data },
                            new TreeNode($"Сумма:  {product.Sum}\n"){ Tag = DatabaseWindowTag.Data }}
                        )
                        { Tag = DatabaseWindowTag.Data });

                productsAndTotalPrice.Add(new TreeNode($"Полная сумма:  {receipt.TotalPrice}") { Tag = DatabaseWindowTag.Data });

                TreeNode treeNode = new TreeNode($"{receipt.DateAndTime}:  {receipt.RetailPlaceAddress}",
                    productsAndTotalPrice.ToArray())
                { Tag = DatabaseWindowTag.ReceiptHeader };

                databaseWindow.Nodes.Add(treeNode);
            }
        }

        private void InitializeYearStatisticChart()
        {
            ChartArea chartArea = new ChartArea();
            chartArea.IsSameFontSizeForAllAxes = true;
            chartArea.BorderDashStyle = ChartDashStyle.Solid;
            chartArea.BorderWidth = 1;
            chartArea.BorderColor = Color.WhiteSmoke;

            chartArea.AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;

            // Point list contain x and y as a double type, so if you add x as a string,
            // there will be 0 every time you add new x, and therefore chart will have a lot of points with x = 0,
            // and Chart display only one. AxisX.Interval = 1 solves this problem
            chartArea.AxisX.Interval = 1;

            chartArea.AxisX.MajorGrid.LineWidth = 1;
            chartArea.AxisY.MajorGrid.LineWidth = 1;
            chartArea.AxisX.MajorGrid.LineColor = Color.DarkGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.DarkGray;
            chartArea.AxisX.LineWidth = 2;
            chartArea.AxisY.LineWidth = 2;
            chartArea.Position.Auto = false;
            chartArea.Position.X = 0;
            chartArea.Position.Y = 100;
            chartArea.Position.Width = 95;
            chartArea.Position.Height = 95;

            yearChart.ChartAreas.Add(chartArea);

            Series series = new Series("Сумма за месяц");
            series.ChartType = SeriesChartType.Line;
            series.XValueType = ChartValueType.String;
            series.YValueType = ChartValueType.Int32;
            series.IsXValueIndexed = true;
            series.IsValueShownAsLabel = true;
            series.Font = Font;
            series.Color = Color.Blue;
            series.BorderWidth = 3;

            yearChart.Series.Add(series);

            Title title = new Title();
            title.Position.Auto = false;
            title.Position.X = 45.5f;
            title.Position.Y = 2;
            title.Position.Width = 10;
            title.Position.Height = 5;
            title.Font = new Font(Font.FontFamily, Font.Size + 2, FontStyle.Bold);
            title.Text = $"{DateTime.Now.Year} год";

            yearChart.Titles.Add(title);

            UpdateYearStatisticChart();
        }

        private void UpdateYearStatisticChart()
        {
            yearChart.Series.First().Points.Clear();

            double maximum = 0;
            int interval;

            foreach (StatisticDataUnit data in dataService.GetCurrentYearReceipts())
            {
                int i = yearChart.Series.First().Points.AddXY(data.date.ToString("MMMM"), data.Value);
                yearChart.Series.First().Points[i].Font = new Font(Font.FontFamily, Font.Size + 1, FontStyle.Bold);
                yearChart.Series.First().Points[i].IsEmpty = data.Value == 0;
                maximum = Math.Max(maximum, data.Value);
            }

            if (maximum == 0)
                interval = DEFAULT_YEAR_CHART_AXIS_Y_INTERVAL;
            else
            {
                maximum = GetNearestRoundValue(maximum);
                interval = Convert.ToInt32(maximum / YEAR_CHART_AXIS_Y_INTERVAL_COUNT);
            }

            yearChart.ChartAreas.First().AxisY.Interval = interval;
            yearChart.ChartAreas.First().AxisY.Maximum = maximum;
        }

        private void AskUserToken(bool isWrongAPI)
        {
            UserTokenForm utf = new UserTokenForm(isWrongAPI);
            utf.ShowDialog();
            dataService.SetUserToken(utf.UserToken);
        }


        private void deleteReceiptFromDatabase_Click(object sender, EventArgs e)
        {
            if (databaseWindow.SelectedNode != null)
            {
                TreeNode treeNode = databaseWindow.SelectedNode;

                while ((DatabaseWindowTag)treeNode.Tag != DatabaseWindowTag.ReceiptHeader)
                    treeNode = treeNode.Parent;

                DialogResult dr = MessageBox.Show("Вы действительно хотите удалить чек?",
                      "", MessageBoxButtons.YesNo);

                if (dr != DialogResult.Yes)
                {
                    return;
                }

                DateTime time = DateTime.Parse(treeNode.Text.Substring(0, treeNode.Text.IndexOf(": ")));

                dataService.DeleteReceipt(time);
                UpdateAllReceiptsInDatabaseWindow();
                UpdateYearStatisticChart();
            }
        }


        private double GetNearestRoundValue(double n)
        {
            double zeros = Math.Pow(10, ((int)n).ToString().Length);
            n = n / zeros;
            n = Math.Ceiling(n * 100) / 100;
            return n * zeros;
        }

        private enum DatabaseWindowTag
        {
            ReceiptHeader,
            Data
        }
    }
}
