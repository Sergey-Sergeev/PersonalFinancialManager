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
        private FTS(string userToken) => this.userToken = userToken;

        public static FTS FTSFabric(string userToken)
        {
            if (singleFTS != null) return singleFTS;

            httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(30)
            };
            singleFTS = new FTS(userToken);

            return singleFTS;
        }

        public void UpdateUserToken(ref DataBase database)
        {
            userToken = database.UserToken;
        }

        public async Task<FTSDecodingReceiptsResult> GetReceiptsFromQRCodesImages(string[] files)
        {
            FTSDecodingReceiptsResult fTSResult = new FTSDecodingReceiptsResult();

            bool QRDecoded;
            FTSResponseResult fTSResponseResult = null;
            FailGettingReceiptData.ErrorCode errorCode = FailGettingReceiptData.ErrorCode.UnknownError;

            for (int i = 0; i < files.Length; i++)
            {
                if ((QRDecoded = QRCodeData.ParseDataFromQR(files[i], out QRCodeData? qRCodeData)) &&
                    ((fTSResponseResult = await FTSRequest(qRCodeData)).StatusCode == FTSResponseResult.ServerResponseCode.Success) &&
                    ((errorCode = Receipt.ParseReceiptFromJson(fTSResponseResult.Response, out Receipt? receipt)) == FailGettingReceiptData.ErrorCode.Success))
                {
                    fTSResult.Receipts.Add(receipt);
                }
                else
                {
                    FailGettingReceiptData failGettingReceiptData = new FailGettingReceiptData();
                    failGettingReceiptData.FileName = files[i];

                    if (!QRDecoded)
                        failGettingReceiptData.Code = FailGettingReceiptData.ErrorCode.DecodingQRFail;
                    else if (fTSResponseResult.StatusCode != FTSResponseResult.ServerResponseCode.Success)
                        failGettingReceiptData.Code = FailGettingReceiptData.RecognizeServerStatus(fTSResponseResult.StatusCode);
                    else failGettingReceiptData.Code = errorCode;

                    failGettingReceiptData.ServerResponseCode = (fTSResponseResult == null ? -1 : fTSResponseResult.ResponseCode);
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
