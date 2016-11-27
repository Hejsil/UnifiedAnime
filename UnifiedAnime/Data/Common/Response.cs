using System.Net;
using RestSharp;

namespace UnifiedAnime.Data.Common
{
    public class Response
    {
        #region Properties
        
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }

        #endregion

        public Response()
        { }

        public Response(IRestResponse restResponse)
        {
            if (restResponse.ResponseStatus == RestSharp.ResponseStatus.TimedOut)
            {
                Status = ResponseStatus.TimedOut;
                return;
            }

            switch (restResponse.StatusCode)
            {
                case HttpStatusCode.Created:
                case HttpStatusCode.OK:
                    Status = ResponseStatus.Success;
                    break;
                case HttpStatusCode.InternalServerError:
                    Status = ResponseStatus.InternalServerError;
                    break;
                case HttpStatusCode.Unauthorized:
                    Status = ResponseStatus.Unauthorized;
                    break;
                default:
                    Status = ResponseStatus.Unknown;
                    Message = "Unified anime should never give this response.";
                    break;
            }
        }
    }
}