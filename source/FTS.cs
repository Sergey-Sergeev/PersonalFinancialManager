using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;

namespace PersonalFinancialManager.source
{
    public static class FTS
    {

        public static List<Receipt> GetReceiptsFromQRCodes(string[] files, out string[] failDecodingImages)
        {
            List<Receipt> receipts = new List<Receipt>();
            List<string> failDecoding = new List<string>();

            for (int i = 0; i < files.Length; i++)
            {
                if (ParseDataFromQR(files[i], out QRCodeData qRCodeData) && FTSRequest(ref qRCodeData, out List<Product> products))
                {
                    receipts.Add(new Receipt(qRCodeData, products));
                }
                else
                {
                    failDecoding.Add(files[i]);
                }
            }

            failDecodingImages = failDecoding.ToArray();
            return receipts;
        }

        private static bool ParseDataFromQR(string file, out QRCodeData qRCodeData)
        {
            qRCodeData = null;

            BarcodeReader reader = new BarcodeReader()
            {
                AutoRotate = true,
                Options = new DecodingOptions { TryHarder = true }
            };

            Bitmap bitmap = (Bitmap)Image.FromFile(file);
            Result result = reader.Decode(bitmap);

            bitmap.Dispose();

            if (result.Text == null)
                return false;

            qRCodeData = new QRCodeData(result.Text);
            return true;
        }


        private static bool FTSRequest(ref QRCodeData receipt, out List<Product> products)
        {
            products = new List<Product>();


            // TO DO: to make request you need password and login from gosuslugi or FTS


            return true;
        }

    }
}
