using PersonalFinancialManager.source;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.VisualStyles;
using ZXing;
using static PersonalFinancialManager.source.DataService;

namespace PersonalFinancialManager
{
    public partial class MainForm : Form
    {
        const string FILE_FILTER = "Изображения (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|PNG (*.png)|*.png|JPEG (*.jpg)|*.jpg|JPEG (*.jpeg)|*.jpeg";
        private DataService dataService;


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
                    productsAndTotalPrice.ToArray()) { Tag = DatabaseWindowTag.ReceiptHeader };

                databaseWindow.Nodes.Add(treeNode);
            }
        }

        private void InitializeYearStatisticChart()
        {
            yearChart.ChartAreas.Add(new ChartArea());

            Series series = new Series("Сумма за месяц");
            series.ChartType = SeriesChartType.Line;
            series.Font = Font;
            series.Color = Color.Blue;
            series.BorderWidth = 2;

            yearChart.Series.Add(series);
            yearChart.Legends.Add(new Legend() { Font = Font });

            yearChartYearLabel.Text = $"{DateTime.Now.Year} год";

            UpdateYearStatisticChart();
        }

        private void UpdateYearStatisticChart()
        {
            yearChart.Series.First().Points.Clear();

            foreach (StatisticDataUnit data in dataService.GetCurrentYearReceipts())
                yearChart.Series.First().Points.AddXY(data.date.Month, data.Value);
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

        private enum DatabaseWindowTag
        {
            ReceiptHeader,
            Data
        }
    }
}
