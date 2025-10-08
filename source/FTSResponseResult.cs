using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManager.source
{
    public class FTSResponseResult
    {
        public string Response;
        public ServerResponseCode StatusCode;
        public int ResponseCode;

        public static ServerResponseCode RecognizeServerResponseCode(int code)
        {
            if (code >= 200 && code < 300) return ServerResponseCode.Success;
            else if (code >= 400 && code < 500) return ServerResponseCode.ClientError;
            else if (code >= 500) return ServerResponseCode.ServerError;
            else return ServerResponseCode.OtherError;
        }

        public enum ServerResponseCode
        {
            Success,
            ClientError,
            ServerError,
            OtherError
        }
    }
}
