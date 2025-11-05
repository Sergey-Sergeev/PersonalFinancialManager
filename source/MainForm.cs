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

        private StatisticChart yearStatisticChart;
        private StatisticChart monthStatisticChart;
        private SpecialStatisticChart specialStatisticChart;

        public MainForm()
        {
            InitializeComponent();

            dataService = DataService.Fabric(out bool isUserAuthorizated);

            if (!isUserAuthorizated)
                AskUserToken(false);

            UpdateAllReceiptsInDatabaseWindow();
            InitializeStatistic();
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
                    UpdateStatisticCharts();
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

        private void InitializeStatistic()
        {
            yearStatisticChart = new StatisticChart(ref yearChart, $"{DateTime.Now.Year} год",
                Font, dataService.GetCurrentYearReceipts, "MMMM");
            yearStatisticChart.Initialize();

            monthStatisticChart = new StatisticChart(ref monthChart, DateTime.Now.ToString("MMMM"),
                Font, dataService.GetCurrentMonthReceipts, "dd");
            monthStatisticChart.Initialize();

            specialStatisticChart = new SpecialStatisticChart(ref specialChart, Font,
                dataService.GetReceiptsDuringPeriod,
                specialChartSeries1DateFromTextBox,
                specialChartSeries1DateUntilTextBox,
                specialChartSeries1HideCheckBox,
                specialChartSeries3HideCheckBox,
                specialChartSeries3DateUntilTextBox,
                specialChartSeries3DateFromTextBox,
                specialChartSeries2HideCheckBox,
                specialChartSeries2DateUntilTextBox,
                specialChartSeries2DateFromTextBox,
                specialChartIntervalComboBox
                );
            specialStatisticChart.Initialize();

            InitializeAllStatisticPage();
            UpdateStatisticCharts();
        }

        private void InitializeAllStatisticPage()
        {
            ChartArea chartArea = new ChartArea();
            chartArea.BorderColor = Color.WhiteSmoke;

            allStatisticPageChart.ChartAreas.Add(chartArea);

            Series series = new Series();
            series.ChartType = SeriesChartType.Pie;
            series.XValueType = ChartValueType.String;
            series.YValueType = ChartValueType.Double;
            series.Font = Font;
            series.Color = StatisticChart.DEFAULT_CHART_SERIES_COLOR;

            series["PieLabelStyle"] = "Outside";
            series["PieLineColor"] = "Black";
            series.Label = "#VALX #PERCENT{P2}";

            allStatisticPageChart.Series.Add(series);

            Title title = new Title();
            title.Font = Font;
            title.Position.X = 45.5f;
            title.Position.Y = 0;
            title.Position.Width = 10;
            title.Position.Height = 5;

            allStatisticPageChart.Titles.Add(title);


            allStatisticPageYearTextBox.TextChanged += AllStatisticPageAutoSetPastYear;
            allStatisticPageYearTextBox.TextChanged += UpdateAllStatisticPage;

            allStatisticPageMonthTextBox.TextChanged += AllStatisticPageAutoSetPastMonth;
            allStatisticPageMonthTextBox.TextChanged += UpdateAllStatisticPage;

            allStatisticPagePastYearTextBox.TextChanged += UpdateAllStatisticPage;
            allStatisticPagePastMonthTextBox.TextChanged += UpdateAllStatisticPage;

            allStatisticPageYearTextBox.Text = DateTime.Now.ToString("yyyy");
            allStatisticPageMonthTextBox.Text = DateTime.Now.ToString("MM");

            UpdateAllStatisticPage();
        }

        private void AllStatisticPageAutoSetPastYear(object? sender, EventArgs e)
        {
            if (int.TryParse(allStatisticPageYearTextBox.Text, out int year) && year > 1)
            {
                allStatisticPagePastYearTextBox.Text = $"{year - 1}";
            }
        }

        private void AllStatisticPageAutoSetPastMonth(object? sender, EventArgs e)
        {
            allStatisticPagePastMonthTextBox.Text = allStatisticPageMonthTextBox.Text;
        }

        private void UpdateAllStatisticPage(object? sender = null, EventArgs? e = null)
        {
            string yearStr = "____";
            string pastYearStr = "____";
            string monthStr = "__";
            string pastMonthStr = "__";

            string previous_2x_yearStr = "____";

            double yearSum = 0;
            double monthSum = 0;
            double prevYearSum = 0;
            double prevMonthSum = 0;

            double previous_2x_yearSum = 0;
            double previous_2x_monthSum = 0;

            bool isDataExist = false;

            int year = 0;

            if (allStatisticPageYearTextBox.Text != String.Empty &&
                allStatisticPageMonthTextBox.Text != String.Empty &&
                allStatisticPagePastYearTextBox.Text != String.Empty &&
                allStatisticPagePastMonthTextBox.Text != String.Empty &&

                int.TryParse(allStatisticPageYearTextBox.Text, out year) &&
                year > 1800 && year <= DateTime.Now.Year &&

                int.TryParse(allStatisticPageMonthTextBox.Text, out int month) &&
                month > 0 && month < 13 &&

                int.TryParse(allStatisticPagePastYearTextBox.Text, out int pastYear) &&
                pastYear > 1800 && pastYear <= year &&

                int.TryParse(allStatisticPagePastMonthTextBox.Text, out int pastMonth) &&
                pastMonth > 0 && pastMonth < 13
                )
            {
                yearSum = dataService.GetTotalSumDuringPeriod(year, 1, year, 12);
                monthSum = dataService.GetTotalSumDuringPeriod(year, month, year, month);
                prevYearSum = dataService.GetTotalSumDuringPeriod(pastYear, 1, pastYear, 12);
                prevMonthSum = dataService.GetTotalSumDuringPeriod(pastYear, pastMonth, pastYear, pastMonth);

                previous_2x_yearSum = dataService.GetTotalSumDuringPeriod(pastYear - 1, 1, pastYear - 1, 12);
                previous_2x_monthSum = dataService.GetTotalSumDuringPeriod(pastYear - 1, pastMonth, pastYear - 1, pastMonth);

                yearStr = year.ToString();
                monthStr = month.ToString();
                pastYearStr = pastYear.ToString();
                pastMonthStr = pastMonth.ToString();

                previous_2x_yearStr = (pastYear - 1).ToString();                
                isDataExist = true;
            }

            allStatisticPageTotalYearSumDateLabel.Text = $"Общая сумма за {yearStr} год: ";
            allStatisticPageTotalMonthSumDateLabel.Text = $"Общая сумма за {monthStr}.{yearStr} месяц: ";
            allStatisticPageTotalPreviousYearSumDateLabel.Text = $"Общая сумма за {pastYearStr} год: ";
            allStatisticPageTotalPreviousMonthSumDateLabel.Text = $"Общая сумма за {pastMonthStr}.{pastYearStr} месяц: ";
            allStatisticPageDiffBtwYearsDateLabel.Text = $"Разница между {yearStr} и {pastYearStr}:  ";
            allStatisticPageDiffBtwMonthesDateLabel.Text = $"Разница между {monthStr}.{yearStr} и {pastMonthStr}.{pastYearStr}:  ";

            allStatisticPageTotal_2x_PreviousYearSumDateLabel.Text = $"Общая сумма за {previous_2x_yearStr} год: ";
            allStatisticPageTotal_2x_PreviousMonthSumDateLabel.Text = $"Общая сумма за {pastMonthStr}.{previous_2x_yearStr} месяц: ";
            allStatisticPageDiffBtwPreviousYearsDateLabel.Text = $"Разница между {pastYearStr} и {previous_2x_yearStr}:  ";
            allStatisticPageDiffBtwPreviousMonthesDateLabel.Text = $"Разница между {pastMonthStr}.{pastYearStr} и {pastMonthStr}.{previous_2x_yearStr}:  ";

            if (isDataExist)
            {
                double yearDiff = Double.Round(yearSum - prevYearSum, 2);
                double monthDiff = Double.Round(monthSum - prevMonthSum, 2);

                double previous_2x_yearDiff = Double.Round(prevYearSum - previous_2x_yearSum, 2);
                double previous_2x_monthDiff = Double.Round(prevMonthSum - previous_2x_monthSum, 2);

                allStatisticPageTotalYearSumValueLabel.Text = $"{yearSum} руб.";
                allStatisticPageTotalMonthSumValueLabel.Text = $"{monthSum} руб.";
                allStatisticPageTotalPreviousYearSumValueLabel.Text = $"{prevYearSum} руб.";
                allStatisticPageTotalPreviousMonthSumValueLabel.Text = $"{prevMonthSum} руб.";
                allStatisticPageDiffBtwYearsValueLabel.Text = $"{yearDiff} руб. {(yearDiff <= 0 ? '↑' : '↓')}";
                allStatisticPageDiffBtwMonthesValueLabel.Text = $"{monthDiff} руб. {(monthDiff <= 0 ? '↑' : '↓')}";

                allStatisticPageDiffBtwYearsValueLabel.BackColor = yearDiff <= 0 ? Color.PaleGreen : Color.PaleVioletRed;
                allStatisticPageDiffBtwMonthesValueLabel.BackColor = monthDiff <= 0 ? Color.PaleGreen : Color.PaleVioletRed;

                allStatisticPageTotal_2x_PreviousYearSumValueLabel.Text = $"{previous_2x_yearSum} руб.";
                allStatisticPageTotal_2x_PreviousMonthSumValueLabel.Text = $"{previous_2x_monthSum} руб.";
                allStatisticPageDiffBtwPreviousYearsValueLabel.Text = $"{previous_2x_yearDiff} руб. {(previous_2x_yearDiff <= 0 ? '↑' : '↓')}";
                allStatisticPageDiffBtwPreviousMonthesValueLabel.Text = $"{previous_2x_monthDiff} руб. {(previous_2x_monthDiff <= 0 ? '↑' : '↓')}";

                allStatisticPageDiffBtwPreviousYearsValueLabel.BackColor = previous_2x_yearDiff <= 0 ? Color.PaleGreen : Color.PaleVioletRed;
                allStatisticPageDiffBtwPreviousMonthesValueLabel.BackColor = previous_2x_monthDiff <= 0 ? Color.PaleGreen : Color.PaleVioletRed;
            }
            else
            {
                allStatisticPageTotalYearSumValueLabel.Text =
                allStatisticPageTotalMonthSumValueLabel.Text =
                allStatisticPageTotalPreviousYearSumValueLabel.Text =
                allStatisticPageTotalPreviousMonthSumValueLabel.Text =
                allStatisticPageDiffBtwYearsValueLabel.Text =
                allStatisticPageDiffBtwMonthesValueLabel.Text =
                allStatisticPageTotal_2x_PreviousYearSumValueLabel.Text =
                allStatisticPageTotal_2x_PreviousMonthSumValueLabel.Text =
                allStatisticPageDiffBtwPreviousYearsValueLabel.Text =
                allStatisticPageDiffBtwPreviousMonthesValueLabel.Text =
                "-";
            }


            if (!isDataExist) 
                return;


            allStatisticPageChart.Series[0].Points.Clear();

            allStatisticPageCategoriesListView.Items.Clear();

            foreach (KeyValuePair<string, double> data in dataService.GetProductCategoryStatisticDuringYear(year))
            {
                int i = allStatisticPageChart.Series[0].Points.AddXY(data.Key, data.Value);
                allStatisticPageChart.Series[0].Points[i].Font = Font;
                allStatisticPageChart.Series[0].Points[i]["Exploded"] = "True";

                ListViewItem item = new ListViewItem(data.Key);
                item.SubItems.Add($"{data.Value}");
                allStatisticPageCategoriesListView.Items.Add(item);
            }

            allStatisticPageChart.Titles[0].Text = yearStr;

        }
               
        private void UpdateStatisticCharts()
        {
            yearStatisticChart.Update();
            monthStatisticChart.Update();
            specialStatisticChart.Update();
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
                UpdateStatisticCharts();
            }
        }

        private enum DatabaseWindowTag
        {
            ReceiptHeader,
            Data
        }

    }
}
