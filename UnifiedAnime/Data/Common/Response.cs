using System.Net;
using RestSharp;

namespace UnifiedAnime.Data.Common
{
    public class Response
    {
        #region Properties
        
        public UnifiedStatus Status { get; }
        public string Message { get; }

        #endregion

        public Response(UnifiedStatus status, string message = null)
        {
            Status = status;
            Message = message;
        }

        public Response(IRestResponse restResponse)
        {
            if (restResponse.ResponseStatus == RestSharp.ResponseStatus.TimedOut)
            {
                Status = UnifiedStatus.TimedOut;
                return;
            }

            switch (restResponse.StatusCode)
            {
                case HttpStatusCode.Created:
                case HttpStatusCode.OK:
                    Status = UnifiedStatus.Success;
                    break;
                case HttpStatusCode.InternalServerError:
                    Status = UnifiedStatus.InternalServerError;
                    break;
                case HttpStatusCode.Unauthorized:
                    Status = UnifiedStatus.Unauthorized;
                    break;
                default:
                    Status = UnifiedStatus.Unknown;
                    Message = "Unified anime should never give this response.";
                    break;
            }
        }
    }

    public class Response<T> : Response
    {
        public T Data { get; }

        public Response(UnifiedStatus status, T data, string message = null) 
            : base(status, message)
        {
            Data = data;
        }

        public Response(IRestResponse restResponse, T data) 
            : base(restResponse)
        {
            Data = data;
        }

        public Response(Response response, T data)
            : this(response.Status, data, response.Message)
        { }
    }
}