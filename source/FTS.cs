using System.Net.Http.Headers;
using System.Text;
using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;

namespace PersonalFinancialManager.source
{
    public class FTS
    {
        private static HttpClient httpClient = null;

        private static FTS? singleFTS = null;
        private static readonly object locker = new object();   // for safe stream

        private FTS() { }

        public static FTS FTSFabric(string login, string password)
        {
            if (singleFTS != null) return singleFTS;

            lock (locker)
            {
                httpClient = new HttpClient()
                {
                    Timeout = TimeSpan.FromSeconds(30)
                };
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                byte[] bytes = Encoding.ASCII.GetBytes($"{login}:{password}");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));

                singleFTS = new FTS();
            }

            return singleFTS;
        }

        public async Task<FTSDecodingReceiptsResult> GetReceiptsFromQRCodes(string[] files)
        {
            FTSDecodingReceiptsResult fTSResult = new FTSDecodingReceiptsResult();

            FTSResponseResult fTSResponseResult = null;

            for (int i = 0; i < files.Length; i++)
            {
                if (ParseDataFromQR(files[i], out QRCodeData? qRCodeData) && ((fTSResponseResult = await FTSRequest(qRCodeData)).Response != null))
                {
                    List<Product> products = Receipt.ParseProductsFromJSON(fTSResponseResult.Response);
                    fTSResult.Receipts.Add(new Receipt(qRCodeData, products));
                }
                else
                {
                    fTSResult.FailDecoding.Add(files[i] + (fTSResponseResult?.Response == null ? $", Error Code: {fTSResponseResult.StatusCode}" : String.Empty));
                }
            }

            return fTSResult;
        }

        private bool ParseDataFromQR(string file, out QRCodeData? qRCodeData)
        {
            qRCodeData = null;

            BarcodeReader reader = new BarcodeReader()
            {
                AutoRotate = true,
                Options = new DecodingOptions { TryHarder = true }
            };

            using (Bitmap bitmap = (Bitmap)Image.FromFile(file))
            {
                Result result = reader.Decode(bitmap);
                if (result?.Text == null)
                    return false;

                return QRCodeData.ParseQRCodeData(result.Text, out qRCodeData);
            }
        }


        private async Task<FTSResponseResult> FTSRequest(QRCodeData qrData)
        {
            FTSResponseResult fTSResponseResult = new FTSResponseResult();
            Uri uri = new Uri($"https://proverkacheka.nalog.ru:9999/v1/inns/*/kkts/*/fss/{qrData.Fn}/tickets/{qrData.I}?fiscalSign={qrData.Fp}&date={qrData.T}&sum={qrData.S}");

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(uri);

                fTSResponseResult.StatusCode = (int)response.StatusCode;

                if (response.IsSuccessStatusCode)
                {
                    fTSResponseResult.Response = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    fTSResponseResult.Response = null;
                }
            }
            catch (HttpRequestException ex)
            {
                fTSResponseResult.Response = null;
                fTSResponseResult.StatusCode = (int)(ex.StatusCode ?? 0);
            }

            return fTSResponseResult;
        }

    }
}
