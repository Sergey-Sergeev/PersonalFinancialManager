using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;

namespace PersonalFinancialManager.source
{
    public class QRCodeData
    {
        private string fn;
        private string i;
        private string fp;
        private string s;
        private string t;
        private string n;

        public string Fn { get => fn; private set => fn = value; }            // fn number
        public string I { get => i; private set => i = value; }             // receipt number
        public string Fp { get => fp; private set => fp = value; }            // fiscal sing
        public string S { get => s; private set => s = value; }             // total sum
        public string T { get => t; private set => t = value; }             // date
        public string N { get => n; private set => n = value; }             // in/out sing
        public string FullStringData { get; private set; }

        // example = t=20251002T1530&s=1234.56&fn=9281000100001234&i=12345&fp=678901234&n=1

        private QRCodeData() { }

        public static bool ParseQRCodeData(string QRData, out QRCodeData? result)
        {
            result = new QRCodeData();

            if (
                ParseParameterFromQRData("t=", ref QRData, out result.t) &&
                ParseParameterFromQRData("&s=", ref QRData, out result.s) &&
                ParseParameterFromQRData("&fn=", ref QRData, out result.fn) &&
                ParseParameterFromQRData("&i=", ref QRData, out result.i) &&
                ParseParameterFromQRData("&fp=", ref QRData, out result.fp) &&
                ParseParameterFromQRData("&n=", ref QRData, out result.n)
                )
            {
                result.FullStringData = QRData;
                return true;
            }

            result = null;
            return false;
        }

        public static async Task<QRCodeData?> ParseDataFromQRImageAsync(string file)
        {
            return await Task.Run<QRCodeData?>(() =>
            {

                QRCodeData? qRCodeData = null;

                using var src = Cv2.ImRead(file, ImreadModes.Color);
                if (src.Empty())
                    return null;

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
                            ParseQRCodeData(result.Text, out qRCodeData);
                            return qRCodeData;
                        }
                    }
                }
                return null;
            });
        }

        public override string ToString()
        {
            return $"t={t}&s={s}&fn={fn}&i={i}&fp={fp}&n={n}";
        }

        private static bool ParseParameterFromQRData(string paramName, ref string QRData, out string data)
        {
            data = "";
            for (int i = QRData.IndexOf(paramName) + paramName.Length; i < QRData.Length; i++)
            {
                if (i == 0 || i < 0)
                {
                    return false;
                }

                if (QRData[i] == '&')
                    break;
                data += QRData[i];
            }
            return true;
        }
    }
}
