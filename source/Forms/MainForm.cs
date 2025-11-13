using PersonalFinancialManager.source;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.VisualStyles;
using ZXing;
using static PersonalFinancialManager.source.DataService;
using static PersonalFinancialManager.source.JsonServerClass;
using static PersonalFinancialManager.source.TryGetReceiptsResultUnit;
using static System.Net.Mime.MediaTypeNames;

namespace PersonalFinancialManager.source.Forms
{
    public partial class MainForm : Form
    {
        const string FILE_FILTER = "Изображения (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|PNG (*.png)|*.png|JPEG (*.jpg)|*.jpg|JPEG (*.jpeg)|*.jpeg";
        private DataService dataService;

        private StatisticChart yearStatisticChart;
        private StatisticChart monthStatisticChart;
        private SpecialStatisticChart specialStatisticChart;

        private const string TREENODE_ID_START_MARKER = "ID: ";
        private const string TREENODE_ID_STOP_MARKER = ";";

        private const string MESSAGEBOX_CAPTION_ERROR = "Ошибка";
        private const string MESSAGEBOX_TEXT_CANT_CHANGE_RECEIPT = "Вы не можете изменить этот чек, потому что он не создан пользователем.";
        private const string MESSAGEBOX_TEXT_SURE_DELETE_RECEIPT = "Вы действительно хотите удалить чек?";
        private const string MESSAGEBOX_TEXT_USER_NEED_SELECT_RECEIPT = "Выделите тело или заголовок чека.";
        private const string MESSAGEBOX_TEXT_USER_NEED_SELECT_PRODUCT = "Выделите заголовок или тело продукта.";



        public MainForm()
        {
            InitializeComponent();

            dataService = DataService.Fabric(out bool isUserAuthorizated);

            if (!isUserAuthorizated)
                AskUserToken(false);

            UpdateAllEntitiesInDatabaseWindow();
            InitializeStatistic();

            Disposed += MainFormDisposed;
        }

        private void MainFormDisposed(object? sender, EventArgs e)
        {
            dataService.Dispose();
            yearStatisticChart.Dispose();
        }

        private void UpdateAll()
        {
            UpdateAllEntitiesInDatabaseWindow();
            UpdateStatisticCharts();
            UpdateAllStatisticPage();
        }

        private int ParseIdFromTreeNode(TreeNode node)
        {
            int s = node.Text.IndexOf(TREENODE_ID_START_MARKER) + TREENODE_ID_START_MARKER.Length;
            int l = node.Text.IndexOf(TREENODE_ID_STOP_MARKER) - s;

            return int.Parse(node.Text.Substring(s, l));
        }

        private TreeNode? GetReceiptIDTreeNode(TreeNode treeNode)
        {
            while ((DatabaseWindowTag)treeNode.Tag != DatabaseWindowTag.ReceiptHeader)
                treeNode = treeNode.Parent;

            foreach (TreeNode child in treeNode.Nodes)
            {
                if ((DatabaseWindowTag)child.Tag == DatabaseWindowTag.ReceiptId)
                {
                    return child;
                }
            }

            return null;
        }

        private void UpdateAllEntitiesInDatabaseWindow()
        {
            if (dataService.GetDatabaseCurrentEntityType() == Database.EntityType.Receipt)
                LoadReceiptsFromDatabase();
            else LoadProductsFromDatabase();
        }

        private void LoadProductsFromDatabase()
        {
            databaseWindowTreeView.Nodes.Clear();

            foreach (Product product in dataService.GetProductsFromDB())
            {
                TreeNode treeNode = new TreeNode($"{product.Name}\n",
                        new TreeNode[] {
                            new TreeNode($"Категория:  {product.Category.Name}") { Tag = DatabaseWindowTag.ProductData },
                            new TreeNode($"Цена:  {product.Price}\n"){ Tag = DatabaseWindowTag.ProductData },
                            new TreeNode($"Количество:  {product.Quantity}\n"){ Tag = DatabaseWindowTag.ProductData },
                            new TreeNode($"Сумма:  {product.Sum}\n"){ Tag = DatabaseWindowTag.ProductData },
                            new TreeNode($"{TREENODE_ID_START_MARKER}{product.Id}{TREENODE_ID_STOP_MARKER}") { Tag = DatabaseWindowTag.ProductId }
                        }) { Tag = DatabaseWindowTag.ProductHeader };

                databaseWindowTreeView.Nodes.Add(treeNode);
            }
        }

        private void LoadReceiptsFromDatabase()
        {
            databaseWindowTreeView.Nodes.Clear();

            foreach (Receipt receipt in dataService.GetReceiptsFromDB())
            {
                List<TreeNode> productsAndTotalPrice = new List<TreeNode>();
                productsAndTotalPrice.Add(new TreeNode($"{TREENODE_ID_START_MARKER}{receipt.Id}{TREENODE_ID_STOP_MARKER}") { Tag = DatabaseWindowTag.ReceiptId });

                productsAndTotalPrice.AddRange(receipt.ListOfProducts.ConvertAll<TreeNode>(
                        (product) => new TreeNode($"{product.Name}\n",
                        new TreeNode[] {
                            new TreeNode($"Категория:  {product.Category.Name}") { Tag = DatabaseWindowTag.ProductData },
                            new TreeNode($"Цена:  {product.Price}\n"){ Tag = DatabaseWindowTag.ProductData },
                            new TreeNode($"Количество:  {product.Quantity}\n"){ Tag = DatabaseWindowTag.ProductData },
                            new TreeNode($"Сумма:  {product.Sum}\n"){ Tag = DatabaseWindowTag.ProductData },
                            new TreeNode($"{TREENODE_ID_START_MARKER}{product.Id}{TREENODE_ID_STOP_MARKER}") { Tag = DatabaseWindowTag.ProductId }
                        })
                        { Tag = DatabaseWindowTag.ProductHeader }));

                productsAndTotalPrice.Add(new TreeNode($"Наличкой: {receipt.CashTotalSum}") { Tag = DatabaseWindowTag.ReceiptData });
                productsAndTotalPrice.Add(new TreeNode($"Электронно: {receipt.EcashTotalSum}") { Tag = DatabaseWindowTag.ReceiptData });
                productsAndTotalPrice.Add(new TreeNode($"Полная сумма:  {receipt.TotalSum}") { Tag = DatabaseWindowTag.ReceiptData });


                TreeNode treeNode = new TreeNode($"{receipt.DateAndTime}:  {receipt.RetailPlaceAddress}",
                    productsAndTotalPrice.ToArray())
                { Tag = DatabaseWindowTag.ReceiptHeader };

                databaseWindowTreeView.Nodes.Add(treeNode);
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
                ref specialChartSeries1DateFromTextBox,
                ref specialChartSeries1DateUntilTextBox,
                ref specialChartSeries1HideCheckBox,
                ref specialChartSeries3HideCheckBox,
                ref specialChartSeries3DateUntilTextBox,
                ref specialChartSeries3DateFromTextBox,
                ref specialChartSeries2HideCheckBox,
                ref specialChartSeries2DateUntilTextBox,
                ref specialChartSeries2DateFromTextBox,
                ref specialChartIntervalComboBox
                );
            specialStatisticChart.Initialize();

            InitializeAllStatisticPage();
            UpdateStatisticCharts();
        }

        private void InitializeAllStatisticPage()
        {
            ChartArea chartArea = new ChartArea();
            chartArea.BorderColor = Color.WhiteSmoke;
            chartArea.BackColor = Color.WhiteSmoke;
            chartArea.Position.X = 0;
            chartArea.Position.Y = 0;
            chartArea.Position.Width = 100;
            chartArea.Position.Height = 100;

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
            string previous_2x_yearStr = "____";
            string monthStr = "__";
            string pastMonthStr = "__";

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

            if (utf.IsOk)
                dataService.SetUserToken(utf.UserToken);
        }

        private void deleteReceiptFromDatabase_Click(object sender, EventArgs e)
        {
            if (databaseWindowTreeView.SelectedNode != null)
            {
                DialogResult dr = MessageBox.Show(MESSAGEBOX_TEXT_SURE_DELETE_RECEIPT,
                      "", MessageBoxButtons.YesNo);

                if (dr != DialogResult.Yes) return;

                TreeNode? treeNode = GetReceiptIDTreeNode(databaseWindowTreeView.SelectedNode);

                if (treeNode == null)
                {
                    MessageBox.Show(MESSAGEBOX_TEXT_USER_NEED_SELECT_RECEIPT, MESSAGEBOX_CAPTION_ERROR, MessageBoxButtons.OK);
                    return;
                }

                dataService.DeleteReceipt(ParseIdFromTreeNode(treeNode));
                UpdateAll();
            }
        }

        private async void addQRCodesImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = FILE_FILTER;
            ofd.FilterIndex = 0;
            ofd.Multiselect = true;

            if (ofd.ShowDialog() != DialogResult.Cancel && ofd.FileNames.Length != 0)
            {
                List<TryGetReceiptsResultUnit> result = await dataService.GetReceiptsFromQRCodes(ofd.FileNames);
                ShowReceiptsProcessResult(result);
            }
        }

        private void ShowReceiptsProcessResult(List<TryGetReceiptsResultUnit> results)
        {
            if (results.Count == 0)
                return;

            List<Receipt> receipts = new List<Receipt>();
            List<FailData> fails = new List<FailData>();

            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].Receipt != null)
                {
                    receipts.Add(results[i].Receipt);
                }

                if (results[i].Fail != null)
                {
                    fails.Add(results[i].Fail);
                }
            }


            if (fails.Count != 0)
            {
                if (fails.Any((item) => item.Code == TryGetReceiptsResultUnit.FailData.ErrorCode.IncorrectAPIKey))
                {
                    AskUserToken(true);
                }
                else
                {
                    new FailReceiptsForm(fails).ShowDialog();
                }
            }

            if (receipts.Count != 0)
            {
                List<Product> products = new List<Product>();
                receipts.ForEach((r) => products.AddRange(r.ListOfProducts));
                SetCategoriesForeachProductForm setCategoriesForm = new SetCategoriesForeachProductForm(ref products);
                setCategoriesForm.ShowDialog();

                dataService.AddReceiptsInDatabase(receipts);
                UpdateAll();
            }
        }

        private void addUserReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReceiptForm receiptForm = new ReceiptForm();
            receiptForm.ShowDialog();

            if (receiptForm.IsOk)
            {
                dataService.AddUserReceipt(receiptForm.OutReceipt);
                UpdateAll();
            }
        }

        private async void loadUsingDataReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QRDataForm getFromUserQRDataForm = new QRDataForm();
            getFromUserQRDataForm.ShowDialog();

            if (getFromUserQRDataForm.IsOk)
            {
                TryGetReceiptsResultUnit result = await dataService.GetReceiptFromQRData(getFromUserQRDataForm.OutQRData);
                ShowReceiptsProcessResult(new List<TryGetReceiptsResultUnit>() { result });
            }
        }

        private async void loadUsingDataStringReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringQRDataForm getQRDataStringForm = new StringQRDataForm();
            getQRDataStringForm.ShowDialog();

            if (getQRDataStringForm.QRDataString != null)
            {
                TryGetReceiptsResultUnit result = await dataService.GetReceiptFromDataString(getQRDataStringForm.QRDataString);
                ShowReceiptsProcessResult(new List<TryGetReceiptsResultUnit>() { result });
            }
        }

        private void changeAPIToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AskUserToken(false);
        }

        private void changeReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (databaseWindowTreeView.SelectedNode != null)
            {

                TreeNode? treeNode = GetReceiptIDTreeNode(databaseWindowTreeView.SelectedNode);

                if (treeNode == null)
                {
                    MessageBox.Show(MESSAGEBOX_TEXT_USER_NEED_SELECT_RECEIPT, MESSAGEBOX_CAPTION_ERROR,
                        MessageBoxButtons.OK);
                    return;
                }

                int id = ParseIdFromTreeNode(treeNode);


                if (dataService.CheckAndGetUserReceipt(id, out Receipt? receipt))
                {
                    ReceiptForm receiptForm = new ReceiptForm(receipt);
                    receiptForm.ShowDialog();

                    if (receiptForm.IsOk)
                    {
                        dataService.ChangeUserReceipt(receiptForm.OutReceipt);
                        UpdateAll();
                    }
                }
                else
                {
                    MessageBox.Show(MESSAGEBOX_TEXT_CANT_CHANGE_RECEIPT, MESSAGEBOX_CAPTION_ERROR, MessageBoxButtons.OK);
                }

            }
        }

        private void sortDatabaseButton_Click(object sender, EventArgs e)
        {
            NewSearchConditionForm newSearchConditionForm = new NewSearchConditionForm(Database.Fabric().CurrentConditionTree);
            newSearchConditionForm.ShowDialog();

            if (newSearchConditionForm.IsOk)
            {
                currentDatabaseConditionTextBox.Text = newSearchConditionForm.OutRoot.GetConditionsString();
                dataService.SetDatabaseCurrentConditionTree(newSearchConditionForm.OutRoot, newSearchConditionForm.OutEntity);
                UpdateAllEntitiesInDatabaseWindow();
            }
        }

        private void changeProductCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (databaseWindowTreeView.SelectedNode != null)
            {
                if ((DatabaseWindowTag)databaseWindowTreeView.SelectedNode.Tag != DatabaseWindowTag.ProductData &&
                   (DatabaseWindowTag)databaseWindowTreeView.SelectedNode.Tag != DatabaseWindowTag.ProductHeader &&
                   (DatabaseWindowTag)databaseWindowTreeView.SelectedNode.Tag != DatabaseWindowTag.ProductId)
                {
                    MessageBox.Show(MESSAGEBOX_TEXT_USER_NEED_SELECT_PRODUCT, MESSAGEBOX_CAPTION_ERROR,
                        MessageBoxButtons.OK);
                    return;
                }

                TreeNode treeNode = databaseWindowTreeView.SelectedNode;

                while ((DatabaseWindowTag)treeNode.Tag != DatabaseWindowTag.ProductHeader)
                    treeNode = treeNode.Parent;

                foreach (TreeNode child in treeNode.Nodes)
                {
                    if ((DatabaseWindowTag)child.Tag == DatabaseWindowTag.ProductId)
                    {
                        treeNode = child;
                        break;
                    }
                }

                int id = ParseIdFromTreeNode(treeNode);

                if (!dataService.TryGetProductFromDatabaseById(id, out Product? product))
                    return;

                ProductCategoryForm productCategoryForm = new ProductCategoryForm(product);
                productCategoryForm.ShowDialog();

                if (productCategoryForm.IsOk)
                {
                    dataService.ChangeProductCategory(product, productCategoryForm.OutProductCategory);
                    UpdateAll();
                }

            }
        }


        private enum DatabaseWindowTag
        {
            ReceiptHeader,
            ProductHeader,
            ReceiptId,
            ProductId,
            ReceiptData,
            ProductData
        }

    }
}
