using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManager.source
{
    public class QRCodeData
    {
        public string Fn { get; private set; }            // fn number
        public string I { get; private set; }             // receipt number
        public string Fp { get; private set; }            // fiscal sing
        public string S { get; private set; }             // total sum
        public string T { get; private set; }             // date
        public string N { get; private set; }             // in/out sing

        // example = t=20251002T1530&s=1234.56&fn=9281000100001234&i=12345&fp=678901234&n=1

        public QRCodeData(string QRData)
        {
            T = ParseParameterFromQRData("t=", ref QRData);
            S = ParseParameterFromQRData("s=", ref QRData);
            Fn = ParseParameterFromQRData("fn=", ref QRData);
            I = ParseParameterFromQRData("i=", ref QRData);
            Fp = ParseParameterFromQRData("fp=", ref QRData);
            N = ParseParameterFromQRData("n=", ref QRData);
        }


        private static string ParseParameterFromQRData(string paramName, ref string QRData)
        {
            string res = "";
            for (int i = QRData.IndexOf(paramName); i < QRData.Length; i++)
            {
                if (QRData[i] == '&')
                    break;
                res += QRData[i];
            }
            return res;
        }
    }
}
