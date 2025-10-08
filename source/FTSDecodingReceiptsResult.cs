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
        public List<FailGettingReceiptData> FailDecoding;

        public FTSDecodingReceiptsResult()
        {
            Receipts = new List<Receipt>();
            FailDecoding = new List<FailGettingReceiptData>();
        }

        public class FailGettingReceiptData
        {
            public string FileName;
            public ErrorCode Code;
            public int ServerResponseCode;

            public static ErrorCode RecognizeServerCodeFromJson(int code)
            {
                if (code == 0) return ErrorCode.IncorrectQRData;
                if (code == 1) return ErrorCode.Success;
                if (code == 2) return ErrorCode.NoAvailableQRData;
                if (code == 3) return ErrorCode.TooMuchServerRequests;
                if (code == 4) return ErrorCode.ServerWaitingRequestAgain;
                return ErrorCode.UnknownError;
            }

            public static ErrorCode RecognizeServerStatus(FTSResponseResult.ServerResponseCode serverResponseCode)
            {
                if (serverResponseCode == FTSResponseResult.ServerResponseCode.ServerError) return ErrorCode.ServerError;
                if (serverResponseCode == FTSResponseResult.ServerResponseCode.ClientError) return ErrorCode.ClientError;
                if (serverResponseCode == FTSResponseResult.ServerResponseCode.Success) return ErrorCode.Success;
                return ErrorCode.UnknownError;
            }

            public enum ErrorCode
            {
                Success,
                DecodingQRFail,
                ServerError,
                ClientError,
                IncorrectQRData,
                NoAvailableQRData,
                TooMuchServerRequests,
                ServerWaitingRequestAgain,
                FailDeserializeJSON,
                UnknownError
            }


        }
    }
}
