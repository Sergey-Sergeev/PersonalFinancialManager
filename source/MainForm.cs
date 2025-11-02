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

        private readonly Dictionary<string, SpecialChartInterval> SPECIAL_CHART_INTERVAL_PAIRS = new Dictionary<string, SpecialChartInterval>()
        {
             { "День", SpecialChartInterval.Day },
             { "Месяц", SpecialChartInterval.Month }
        };

        private const int YEAR_CHART_AXIS_Y_INTERVAL_COUNT = 10;
        private const int DEFAULT_YEAR_CHART_AXIS_Y_INTERVAL = 100;

        private readonly Color SPECIAL_CHART_SERIES1_COLOR = Color.Blue;
        private readonly Color SPECIAL_CHART_SERIES2_COLOR = Color.Yellow;
        private readonly Color SPECIAL_CHART_SERIES3_COLOR = Color.Gray;


        private static class SpecialChartInput
        {
            public static TextBox[] FromTextBoxes;
            public static TextBox[] UntilTextBoxes;
            public static CheckBox[] CheckBoxes;
        }

        public MainForm()
        {
            InitializeComponent();

            dataService = DataService.Fabric(out bool isUserAuthorizated);

            if (!isUserAuthorizated)
                AskUserToken(false);

            UpdateAllReceiptsInDatabaseWindow();
            InitializeStatisticCharts();
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

        private void InitializeStatisticCharts()
        {
            InitializeYearStatisticChart();
            InitializeMonthStatisticChart();
            InitializeSpecialStatisticChart();
        }

        private void UpdateStatisticCharts()
        {
            UpdateYearStatisticChart();
            UpdateMonthStatisticChart();
        }

        private void SetDefaultChart(ref Chart chart, string titleName)
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

            chart.ChartAreas.Add(chartArea);

            Series series = new Series();
            series.ChartType = SeriesChartType.Line;
            series.XValueType = ChartValueType.String;
            series.YValueType = ChartValueType.Int32;
            series.IsXValueIndexed = true;
            series.IsValueShownAsLabel = true;
            series.Font = Font;
            series.Color = SPECIAL_CHART_SERIES1_COLOR;
            series.BorderWidth = 3;

            chart.Series.Add(series);

            Title title = new Title();
            title.Position.Auto = false;
            title.Position.X = 45.5f;
            title.Position.Y = 2;
            title.Position.Width = 10;
            title.Position.Height = 5;
            title.Font = new Font(Font.FontFamily, Font.Size + 2, FontStyle.Bold);
            title.Text = titleName;

            chart.Titles.Add(title);
        }

        private void UpdateDefaultChart(Chart chart, Func<IEnumerable<StatisticDataUnit>> getDataFunc, string dateTimeFormatString)
        {
            chart.Series[0].Points.Clear();

            double maximum = 0;
            int interval;

            foreach (StatisticDataUnit data in getDataFunc())
            {
                int i = chart.Series[0].Points.AddXY(data.date.ToString(dateTimeFormatString), data.Value);
                chart.Series[0].Points[i].Font = new Font(Font.FontFamily, Font.Size + 1, FontStyle.Bold);

                if (data.date > DateTime.Now)
                    chart.Series[0].Points[i].IsEmpty = data.Value == 0;

                maximum = Math.Max(maximum, data.Value);
            }

            if (maximum == 0)
            {
                interval = DEFAULT_YEAR_CHART_AXIS_Y_INTERVAL;
                chart.ChartAreas[0].AxisY.Maximum = DEFAULT_YEAR_CHART_AXIS_Y_INTERVAL * YEAR_CHART_AXIS_Y_INTERVAL_COUNT;
            }
            else
            {
                maximum = GetNearestRoundValue(maximum);
                chart.ChartAreas[0].AxisY.Maximum = maximum;
                interval = Convert.ToInt32(maximum / YEAR_CHART_AXIS_Y_INTERVAL_COUNT);
            }

            chart.ChartAreas[0].AxisY.Interval = interval;
        }

        private void InitializeYearStatisticChart()
        {
            SetDefaultChart(ref yearChart, $"{DateTime.Now.Year} год");
            UpdateYearStatisticChart();
        }

        private void UpdateYearStatisticChart()
        {
            UpdateDefaultChart(yearChart, dataService.GetCurrentYearReceipts, "MMMM");
        }

        private void InitializeMonthStatisticChart()
        {
            SetDefaultChart(ref monthChart, DateTime.Now.ToString("MMMM"));
            UpdateMonthStatisticChart();
        }

        private void UpdateMonthStatisticChart()
        {
            UpdateDefaultChart(monthChart, dataService.GetCurrentMonthReceipts, "dd");
        }

        private void InitializeSpecialStatisticChart()
        {
            SetDefaultChart(ref specialChart, "");

            specialChart.ChartAreas[0].Position.X = 0;
            specialChart.ChartAreas[0].Position.Y = 0;
            specialChart.ChartAreas[0].Position.Width = 100;
            specialChart.ChartAreas[0].Position.Height = 80;

            Legend legend = new Legend();

            legend.Position.Height = 15;
            legend.Position.Width = 25;
            legend.Position.X = 0;
            legend.Position.Y = 83;
            legend.LegendStyle = LegendStyle.Column;
            legend.Font = Font;

            specialChart.Legends.Add(legend);

            Series secondSeries = new Series();
            Series thirdSeries = new Series();
            thirdSeries.ChartType = secondSeries.ChartType = specialChart.Series[0].ChartType;
            thirdSeries.XValueType = secondSeries.XValueType = specialChart.Series[0].XValueType;
            thirdSeries.YValueType = secondSeries.YValueType = specialChart.Series[0].YValueType;
            thirdSeries.IsXValueIndexed = secondSeries.IsXValueIndexed = specialChart.Series[0].IsXValueIndexed;
            thirdSeries.IsValueShownAsLabel = secondSeries.IsValueShownAsLabel = specialChart.Series[0].IsValueShownAsLabel;
            thirdSeries.Font = secondSeries.Font = specialChart.Series[0].Font;
            secondSeries.Color = SPECIAL_CHART_SERIES2_COLOR;
            thirdSeries.Color = SPECIAL_CHART_SERIES3_COLOR;
            thirdSeries.BorderWidth = secondSeries.BorderWidth = specialChart.Series[0].BorderWidth;

            specialChart.Series.Add(secondSeries);
            specialChart.Series.Add(thirdSeries);

            specialChart.Series[0].Name = "График под номером 1";
            specialChart.Series[1].Name = "График под номером 2";
            specialChart.Series[2].Name = "График под номером 3";


            foreach (KeyValuePair<string, SpecialChartInterval> pair in SPECIAL_CHART_INTERVAL_PAIRS)
            {
                specialChartIntervalComboBox.Items.Add(pair.Key);
            }

            SpecialChartInput.FromTextBoxes = new TextBox[] {
                specialChartSeries1DateFromTextBox,
                specialChartSeries2DateFromTextBox,
                specialChartSeries3DateFromTextBox
            };

            SpecialChartInput.UntilTextBoxes = new TextBox[] {
                specialChartSeries1DateUntilTextBox,
                specialChartSeries2DateUntilTextBox,
                specialChartSeries3DateUntilTextBox,
            };

            SpecialChartInput.CheckBoxes = new CheckBox[] {
                specialChartSeries1HideCheckBox,
                specialChartSeries2HideCheckBox,
                specialChartSeries3HideCheckBox
            };


            for (int i = 0; i < 3; i++)
            {
                SpecialChartInput.FromTextBoxes[i].Leave += UpdateSpecialStatisticChart;
                SpecialChartInput.FromTextBoxes[i].KeyPress += SpecialChartInputTextBoxEnterPressedEvent;
                SpecialChartInput.UntilTextBoxes[i].Leave += UpdateSpecialStatisticChart;
                SpecialChartInput.UntilTextBoxes[i].KeyPress += SpecialChartInputTextBoxEnterPressedEvent;
                SpecialChartInput.CheckBoxes[i].CheckedChanged += UpdateSpecialStatisticChart;
            }

            UpdateSpecialStatisticChart();
        }

        private void SpecialChartInputTextBoxEnterPressedEvent(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)ConsoleKey.Enter)
            {
                UpdateSpecialStatisticChart();
                Focus();
            }
        }

        private void UpdateSpecialStatisticChart(object? sender = null, EventArgs e = null)
        {
            if (specialChartIntervalComboBox.Text == String.Empty)
                return;

            specialChart.ChartAreas[0].AxisY.Maximum = 0;

            for (int i = 0; i < 3; i++)
            {
                if (SpecialChartInput.CheckBoxes[i].Checked)
                {
                    specialChart.Series[i].Points.Clear();
                    continue;
                }

                if (IsDateCorrect(ref SpecialChartInput.FromTextBoxes[i], out DateTime fromDate) &&
                    IsDateCorrect(ref SpecialChartInput.UntilTextBoxes[i], out DateTime untilDate))
                {
                    //specialChart.Series[i].Name = $"График под номером {i}";
                    specialChart.Series[i].Points.Clear();

                    double maximum = specialChart.ChartAreas[0].AxisY.Maximum;
                    (int d, int m, int y) interval = GetSpecialChartDateTimeInterval(out string dateTimeFormatString);
                    int moneyInterval;

                    foreach (StatisticDataUnit data in dataService.GetReceiptsDuringPeriod(fromDate, untilDate, interval))
                    {
                        int index = specialChart.Series[i].Points.AddXY(data.date.ToString(dateTimeFormatString), data.Value);
                        specialChart.Series[i].Points[index].Font = new Font(Font.FontFamily, Font.Size + 1, FontStyle.Bold);

                        if (data.date > DateTime.Now)
                            specialChart.Series[i].Points[index].IsEmpty = data.Value == 0;

                        maximum = Math.Max(maximum, data.Value);
                    }


                    if (maximum == 0)
                    {
                        moneyInterval = DEFAULT_YEAR_CHART_AXIS_Y_INTERVAL;
                        specialChart.ChartAreas[0].AxisY.Maximum = DEFAULT_YEAR_CHART_AXIS_Y_INTERVAL * YEAR_CHART_AXIS_Y_INTERVAL_COUNT;
                    }
                    else
                    {
                        maximum = GetNearestRoundValue(maximum);
                        specialChart.ChartAreas[0].AxisY.Maximum = maximum;
                        moneyInterval = Convert.ToInt32(maximum / YEAR_CHART_AXIS_Y_INTERVAL_COUNT);
                    }

                    specialChart.ChartAreas[0].AxisY.Interval = moneyInterval;
                }
            }
        }

        private (int d, int m, int y) GetSpecialChartDateTimeInterval(out string dateTimeFormatString)
        {
            switch (SPECIAL_CHART_INTERVAL_PAIRS.GetValueOrDefault(specialChartIntervalComboBox.Text,
                SpecialChartInterval.Month))
            {
                case SpecialChartInterval.Day:
                    dateTimeFormatString = "dd";
                    return (1, 0, 0);
                case SpecialChartInterval.Month:
                default:
                    dateTimeFormatString = "MMMM";
                    return (0, 1, 0);
            }
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

        private double GetNearestRoundValue(double n)
        {
            string value = ((int)n).ToString();

            if (value.Length < 2)
            {
                return 10;
            }
            else
            {
                int firstNumber = value[0] - '0';
                int secondNumber = value[1] - '0';

                if (secondNumber < 5)
                {
                    secondNumber = 5;
                }
                else
                {
                    secondNumber = 0;
                    firstNumber++;
                }

                return (firstNumber * 10 + secondNumber) * Math.Pow(10, value.Length - 2);
            }
        }

        private bool IsDateCorrect(ref TextBox textBox, out DateTime dateTime)
        {
            if (DateTime.TryParse(textBox.Text, out dateTime))
            {
                textBox.Text = dateTime.ToString("dd.MM.yyyy");
                return true;
            }
            return false;
        }

        private void specialChartIntervalComboBox_TextChanged(object sender, EventArgs e)
        {
            UpdateSpecialStatisticChart();
        }

        private enum DatabaseWindowTag
        {
            ReceiptHeader,
            Data
        }

        private enum SpecialChartInterval
        {
            Day,
            Month
        }
    }
}
