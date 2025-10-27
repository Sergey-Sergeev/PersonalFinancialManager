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

            public static string CodeToString(ErrorCode code)
            {
                switch (code)
                {
                    case ErrorCode.Success:
                        return "Успешно";
                    case ErrorCode.DecodingQRFail:
                    case ErrorCode.IncorrectQRData:
                        return "Ошибка декодирования QR";
                    case ErrorCode.IncorrectAPIKey:
                        return "Неподходящий API ключ";
                    case ErrorCode.ServerError:
                        return "Ошибка на сервере";
                    case ErrorCode.ClientError:
                        return "Ошибка у клиента";
                    case ErrorCode.NoAvailableQRData:
                        return "Чека с такими данными не существует";
                    case ErrorCode.TooMuchServerRequests:
                        return "Превышен предел обращений на сервер";
                    case ErrorCode.ServerWaitingRequestAgain:
                        return "Сервер ожидает запрос";
                    case ErrorCode.FailDeserializeJSON:
                        return "Ошибка десериализации";
                    case ErrorCode.AlreadyExistsInDatabase:
                        return "Чек уже был загружен ранее";
                    case ErrorCode.UnknownError:
                    default:
                        return "Неизвестная ошибка";
                }
            }

            public static ErrorCode RecognizeServerCodeFromJson(int code)
            {
                if (code == 0) return ErrorCode.IncorrectQRData;
                if (code == 1) return ErrorCode.Success;
                if (code == 2) return ErrorCode.NoAvailableQRData;
                if (code == 3) return ErrorCode.TooMuchServerRequests;
                if (code == 4) return ErrorCode.ServerWaitingRequestAgain;
                if (code == 401) return ErrorCode.IncorrectAPIKey;
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
                IncorrectAPIKey,
                ServerError,
                ClientError,
                IncorrectQRData,
                NoAvailableQRData,
                TooMuchServerRequests,
                ServerWaitingRequestAgain,
                FailDeserializeJSON,
                AlreadyExistsInDatabase,
                UnknownError
            }


        }
    }
}
