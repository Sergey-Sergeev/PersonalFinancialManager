using System.Text;
using System.Text.Json;
using static PersonalFinancialManager.source.FTSDecodingReceiptsResult;

namespace PersonalFinancialManager.source
{
    public class FTS
    {
        private static HttpClient httpClient = null;

        private static FTS? singleFTS = null;

        private readonly Uri BASE_URI = new Uri("https://proverkacheka.com/api/v1/check/get");
        private string userToken;
        private DataBase database;
        
        private FTS(ref DataBase database) => this.database = database;

        public static FTS FTSFabric(ref DataBase database)
        {
            if (singleFTS != null) return singleFTS;
           
            httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(30)
            };
            singleFTS = new FTS(ref database);

            return singleFTS;
        }

        public void UpdateUserToken()
        {
            userToken = database.UserToken;
        }

        public async Task<FTSDecodingReceiptsResult> GetReceiptsFromQRCodesImages(string[] files)
        {
            FTSDecodingReceiptsResult fTSResult = new FTSDecodingReceiptsResult();

            bool isQRDecoded;
            bool isDataBaseContainReceipt = false;
            FTSResponseResult fTSResponseResult = null;
            FailGettingReceiptData.ErrorCode errorCode = FailGettingReceiptData.ErrorCode.UnknownError;

            for (int i = 0; i < files.Length; i++)
            {
                if ((isQRDecoded = QRCodeData.ParseDataFromQR(files[i], out QRCodeData? qRCodeData)) &&
                    ((isDataBaseContainReceipt = database.IsContainReceipt(qRCodeData.FullStringData)) == false) &&
                    ((fTSResponseResult = await FTSRequest(qRCodeData)).StatusCode == FTSResponseResult.ServerResponseCode.Success) &&
                    ((errorCode = Receipt.ParseReceiptFromJson(fTSResponseResult.Response, qRCodeData.FullStringData, out Receipt? receipt)) == FailGettingReceiptData.ErrorCode.Success))
                {
                    fTSResult.Receipts.Add(receipt);
                }
                else
                {
                    FailGettingReceiptData failGettingReceiptData = new FailGettingReceiptData();
                    failGettingReceiptData.FileName = files[i];
                    failGettingReceiptData.ServerResponseCode = (fTSResponseResult == null ? -1 : fTSResponseResult.ResponseCode);

                    if (!isQRDecoded)
                        failGettingReceiptData.Code = FailGettingReceiptData.ErrorCode.DecodingQRFail;
                    else if (isDataBaseContainReceipt)
                    {
                        failGettingReceiptData.Code = FailGettingReceiptData.ErrorCode.AlreadyExistsInDatabase;
                    }
                    else if (fTSResponseResult.StatusCode != FTSResponseResult.ServerResponseCode.Success)
                    {
                        failGettingReceiptData.Code = FailGettingReceiptData.RecognizeServerStatus(fTSResponseResult.StatusCode);

                        if (fTSResponseResult.StatusCode == FTSResponseResult.ServerResponseCode.IncorrectAPIKey)
                        {
                            fTSResult.FailDecoding.Add(failGettingReceiptData);
                            return fTSResult;
                        }
                    }
                    else failGettingReceiptData.Code = errorCode;

                    fTSResult.FailDecoding.Add(failGettingReceiptData);
                }
            }

            return fTSResult;
        }

        private async Task<FTSResponseResult> FTSRequest(QRCodeData qrData)
        {
            FTSResponseResult fTSResponseResult = new FTSResponseResult();

            using StringContent jsonContent = new(
            JsonSerializer.Serialize(new
            {
                fn = qrData.Fn,
                fd = qrData.I,
                fp = qrData.Fp,
                t = qrData.T,
                n = qrData.N,
                s = qrData.S,
                qr = 1,
                token = userToken
            }),
            Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await httpClient.PostAsync(BASE_URI, jsonContent);

                fTSResponseResult.ResponseCode = (int)response.StatusCode;
                fTSResponseResult.StatusCode = FTSResponseResult.RecognizeServerResponseCode((int)response.StatusCode);

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
                fTSResponseResult.StatusCode = FTSResponseResult.ServerResponseCode.ServerError;
                fTSResponseResult.ResponseCode = (ex.StatusCode == null ? -1 : (int)ex.StatusCode);
            }

            return fTSResponseResult;
        }

    }
}
