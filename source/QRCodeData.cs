using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

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

        // example = t=20251002T1530&s=1234.56&fn=9281000100001234&i=12345&fp=678901234&n=1

        private QRCodeData() { }

        public static bool ParseQRCodeData(string QRData, out QRCodeData? result)
        {
            result = new QRCodeData();

            if (
                ParseParameterFromQRData("&t=", ref QRData, out result.t) &&
                ParseParameterFromQRData("&s=", ref QRData, out result.s) &&
                ParseParameterFromQRData("&fn=", ref QRData, out result.fn) &&
                ParseParameterFromQRData("&i=", ref QRData, out result.i) &&
                ParseParameterFromQRData("&fp=", ref QRData, out result.fp) &&
                ParseParameterFromQRData("&n=", ref QRData, out result.n)
                )
            {
                result.s = result.s.Replace(".", "");
                result.t = ConvertDate(result.t);
                return true;
            }

            result = null;
            return false;
        }

        private static string ConvertDate(string qrDate)
        {

            return qrDate;
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
