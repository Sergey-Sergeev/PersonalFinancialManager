using PersonalFinancialManager.source;
using ZXing;

namespace PersonalFinancialManager
{
    public partial class MainForm : Form
    {
        const string FILE_FILTER = "Изображения (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|PNG (*.png)|*.png|JPEG (*.jpg)|*.jpg|JPEG (*.jpeg)|*.jpeg";
        private FTS fts;
        private DataBase dataBase;

        public MainForm()
        {
            InitializeComponent();

            dataBase = DataBase.Fabric();
            fts = FTS.FTSFabric(ref dataBase);

            if (!dataBase.IsUserAuthorizated())
            {
                AskUserToken(false);
            }

            LoadAllReceiptsInDatabaseWindow();
        }

        private async void loadQRCodesButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = FILE_FILTER;
            ofd.FilterIndex = 0;
            ofd.Multiselect = true;

            if (ofd.ShowDialog() != DialogResult.Cancel && ofd.FileNames.Length != 0)
            {
                FTSDecodingReceiptsResult result = await fts.GetReceiptsFromQRCodesImages(ofd.FileNames);
                dataBase.AddNewReceipts(result.Receipts);

                if (result.FailDecoding.Count != 0)
                {
                    if (result.FailDecoding[0].Code == FTSDecodingReceiptsResult.FailGettingReceiptData.ErrorCode.IncorrectAPIKey)
                    {
                        AskUserToken(true);
                    }
                    else
                    {
                        new FailGetReceiptsForm(result.FailDecoding).ShowDialog();
                    }
                }

                if (result.Receipts.Count != 0)
                    LoadAllReceiptsInDatabaseWindow();
            }
        }

        private void LoadAllReceiptsInDatabaseWindow()
        {

            foreach (Receipt receipt in dataBase.GetAllReceipts())
            {
                if (IsDataBaseWindowContainReceipt($"{receipt.DateAndTime}:  {receipt.RetailPlaceAddress}"))
                    continue;

                TreeNode treeNode = new TreeNode(
                    $"{receipt.DateAndTime}:  {receipt.RetailPlaceAddress}",
                    receipt.ListOfProducts.ConvertAll<TreeNode>((product) => new TreeNode(
                        $"{product.Name}\n",
                        new TreeNode[] {
                            new TreeNode($"Категория:  {product.Category}"),
                            new TreeNode($"Цена:  {product.Price}\n"),
                            new TreeNode($"Количество:  {product.Quantity}\n"),
                            new TreeNode($"Сумма:  {product.Sum}\n")}
                        )).ToArray());

                databaseWindow.Nodes.Add(treeNode);
            }
        }

        private bool IsDataBaseWindowContainReceipt(string receiptHeader)
        {
            for (int i = 0; i < databaseWindow.Nodes.Count; i++)
            {
                if (databaseWindow.Nodes[i].Text == receiptHeader)
                    return true;
            }
            return false;
        }

        private void AskUserToken(bool isWrongAPI)
        {
            UserTokenForm utf = new UserTokenForm(isWrongAPI);
            utf.ShowDialog();
            dataBase.SetUserData(utf.UserToken);
            fts.UpdateUserToken();
        }

    }
}
