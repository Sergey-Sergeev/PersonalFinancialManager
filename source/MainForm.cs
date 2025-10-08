

using PersonalFinancialManager.source;

namespace PersonalFinancialManager
{
    public partial class MainForm : Form
    {
        const string FILE_FILTER = "Изображения (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|PNG (*.png)|*.png|JPEG (*.jpg)|*.jpg|JPEG (*.jpeg)|*.jpeg";
        FTS fts;
        public MainForm()
        {


            InitializeComponent();

            fts = FTS.FTSFabric("token");          // TO DO: Get login and password from user
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


                for (int i = 0; i < result.Receipts.Count; i++)
                {
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("---SUCCESS---");
                    Console.WriteLine();

                    Console.WriteLine(result.Receipts[i].RetailPlaceAddress);
                    Console.WriteLine(result.Receipts[i].DateAndTime);

                    for (int k = 0; k < result.Receipts[i].ListOfProducts.Count; k++)
                    {
                        Console.WriteLine("Name: " + result.Receipts[i].ListOfProducts[k].Name);
                        Console.WriteLine("Price: " + result.Receipts[i].ListOfProducts[k].Price);
                        Console.WriteLine("Count: " + result.Receipts[i].ListOfProducts[k].Quantity);
                        Console.WriteLine("Sum: " + result.Receipts[i].ListOfProducts[k].Sum);
                        Console.WriteLine("Category: " + result.Receipts[i].ListOfProducts[k].Category);
                    }

                    Console.WriteLine("Cash: " + result.Receipts[i].CashTotalSum);
                    Console.WriteLine("Ecash: " + result.Receipts[i].EcashTotalSum);
                    Console.WriteLine("Total price: " + result.Receipts[i].TotalPrice);
                    Console.WriteLine("----------------------------------------");
                }

                Console.WriteLine();

                for (int i = 0; i < result.FailDecoding.Count; i++)
                {
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("---FAIL---");
                    Console.WriteLine(result.FailDecoding[i].FileName);
                    Console.WriteLine(result.FailDecoding[i].Code);
                    Console.WriteLine(result.FailDecoding[i].ServerResponseCode);
                    Console.WriteLine("----------------------------------------");
                }
            }



        }


    }
}
