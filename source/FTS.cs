using System.Text;
using System.Text.Json;
using static PersonalFinancialManager.source.TryGetReceiptsResultUnit;

namespace PersonalFinancialManager.source
{
    public class FTS
    {
        private readonly Uri BASE_URI = new Uri("https://proverkacheka.com/api/v1/check/get");
        private const int REQUEST_TIMEOUT_IN_SECONDS = 30;

        private static HttpClient httpClient = null;
        private static FTS? singleInstance = null;

        private string userToken;

        private FTS() { }

        public static FTS Fabric(string token)
        {
            if (singleInstance != null) return singleInstance;
           
            httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(REQUEST_TIMEOUT_IN_SECONDS)
            };
            singleInstance = new FTS();
            singleInstance.userToken = token;

            return singleInstance;
        }

        public void UpdateUserToken(string userToken)
        {
            this.userToken = userToken;
        }
               

        public async Task<FTSResponseResult> RequestAsync(QRCodeData qrData)
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
