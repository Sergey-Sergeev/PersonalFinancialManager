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

        private const string ACCEPTABLE_CHARS = "0123456789T.";
        

        private QRCodeData() { }

        public QRCodeData(ulong fn, int i, int fp, double s, DateTime t, bool n)
        {
            this.fn = fn.ToString();
            this.i = i.ToString();
            this.fp = fp.ToString();
            this.s = s.ToString().Replace(",", ".");
            this.t = $"{t.ToString("yyyy")}{t.ToString("MM")}{t.ToString("dd")}T{t.ToString("HH")}{t.ToString("mm")}";
            this.n = n ? "1" : "0";
            FullStringData = ToString();
        }

        public override string ToString()
        {
            return $"t={t}&s={s}&fn={fn}&i={i}&fp={fp}&n={n}";
        }

        public static bool TryParseQRCodeData(string QRData, out QRCodeData? result)
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

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            QRCodeData other = obj as QRCodeData;
            if (other == null) return false;
            return Equals(other);
        }

        public bool Equals(QRCodeData? other)
        {
            if (other == null) return false;

            return this.FullStringData == other.FullStringData;
        }

        public static bool operator ==(QRCodeData qr1, QRCodeData qr2)
        {
            if (object.ReferenceEquals(qr1, null))
            {
                if (object.ReferenceEquals(qr2, null))
                {
                    return true;
                }
                else return false;
            }

            return qr1.Equals(qr2);
        }

        public static bool operator !=(QRCodeData qr1, QRCodeData qr2)
        {
            return !(qr1 == qr2);
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
                            TryParseQRCodeData(result.Text, out qRCodeData);
                            return qRCodeData;
                        }
                    }
                }
                return null;
            });
        }


        private static bool ParseParameterFromQRData(string paramName, ref string QRData, out string data)
        {
            data = "";

            int index = QRData.IndexOf(paramName);

            if (index == -1)
                return false;

            for (int i = index + paramName.Length; i < QRData.Length; i++)
            {
                if (i == 0 || i < 0)                
                    return false;                

                if (QRData[i] == '&')
                    break;

                if (!ACCEPTABLE_CHARS.Contains(QRData[i]))
                    return false;

                data += QRData[i];
            }

            if (data == String.Empty)
                return false;

            return true;
        }
    }
}
