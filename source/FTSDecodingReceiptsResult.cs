using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManager.source
{
    public class FTSDecodingReceiptsResult
    {
        public List<Receipt> Receipts;
        public List<string> FailDecoding;

        public FTSDecodingReceiptsResult() 
        {
            Receipts = new List<Receipt>();
            FailDecoding = new List<string>();
        }
    }
}
