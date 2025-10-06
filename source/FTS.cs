using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Net.Http.Headers;
using System.Text;
using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

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
                //if (ParseDataFromQR(files[i], out QRCodeData? qRCodeData) && ((fTSResponseResult = await FTSRequest(qRCodeData)).Response != null))
                if (ParseDataFromQR(files[i], out QRCodeData? qRCodeData))
                {
                    //Console.WriteLine(fTSResponseResult.Response);
                    //Console.WriteLine(fTSResponseResult.StatusCode);
                    //List<Product> products = Receipt.ParseProductsFromJSON(fTSResponseResult.Response);
                    //fTSResult.Receipts.Add(new Receipt(qRCodeData, products));
                }
                else
                {
                    fTSResult.FailDecoding.Add(files[i] + (fTSResponseResult?.Response == null ? $", Error Code: {fTSResponseResult?.StatusCode}" : String.Empty));
                }
            }

            return fTSResult;
        }

        private bool ParseDataFromQR(string file, out QRCodeData? qRCodeData)
        {
            qRCodeData = null;

            using var src = Cv2.ImRead(file, ImreadModes.Color);
            if (src.Empty())
                return false;

            using var gray = new Mat();
            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

            using var blurred = new Mat();
            Cv2.GaussianBlur(gray, blurred, new OpenCvSharp.Size(9, 9), 0);

            using var th = new Mat();
            Cv2.AdaptiveThreshold(blurred, th, 255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 41, 0);

            using var kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(5, 5));
            using var morph = new Mat();
            Cv2.MorphologyEx(th, morph, MorphTypes.Close, kernel);


            // Try all
            var candidates = new[] { morph, th, gray, src, blurred };

            var reader = new BarcodeReader
            {
                AutoRotate = true,
                Options = new DecodingOptions
                {
                    TryHarder = true,
                    TryInverted = true,
                    PossibleFormats = new List<BarcodeFormat>
                    {
                        BarcodeFormat.QR_CODE,
                        BarcodeFormat.DATA_MATRIX,
                        BarcodeFormat.PDF_417,
                        BarcodeFormat.AZTEC
                    }
                }
            };

            Result result;

            foreach (var mat in candidates)
            {
                using var scaled = new Mat();
                Cv2.Resize(mat, scaled, new OpenCvSharp.Size(mat.Width * 2, mat.Height * 2), 0, 0, InterpolationFlags.Nearest);

                foreach (var tryInvert in new[] { false, true })
                {
                    Mat toDecode;
                    if (tryInvert)
                    {
                        toDecode = new Mat();
                        Cv2.BitwiseNot(scaled, toDecode);
                    }
                    else
                    {
                        toDecode = scaled;
                    }

                    using Bitmap bmp = BitmapConverter.ToBitmap(toDecode);

                    using var bmp24 = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format24bppRgb);
                    using (var g = Graphics.FromImage(bmp24)) g.DrawImage(bmp, 0, 0);

                    result = reader.Decode(bmp24);
                    if (result != null)
                    {
                        //Console.WriteLine(result.Text);
                        return QRCodeData.ParseQRCodeData(result.Text, out qRCodeData);
                    }
                }
            }
            return false;
        }


        private async Task<FTSResponseResult> FTSRequest(QRCodeData qrData)
        {
            FTSResponseResult fTSResponseResult = new FTSResponseResult();
            Uri uri = new Uri(
                $"https://proverkacheka.nalog.ru:9999/v1/ofds/*/inns/*/fss/{qrData.Fn}/operations/{qrData.N}/tickets/{qrData.I}?fiscalSign={qrData.Fp}&date={qrData.T}&sum={qrData.S}"
                //$"https://proverkacheka.nalog.ru:9999/v1/inns/*/kkts/*/fss/{qrData.Fn}/tickets/{qrData.I}?fiscalSign={qrData.Fp}&date={qrData.T}&sum={qrData.S}"
                );

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(uri);

                fTSResponseResult.StatusCode = (int)response.StatusCode;

                if (response.IsSuccessStatusCode)
                {
                    fTSResponseResult.Response = await response.Content.ReadAsStringAsync();

                    //--------------------------------------
                    Console.WriteLine(fTSResponseResult.Response);
                    //--------------------------------------
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
