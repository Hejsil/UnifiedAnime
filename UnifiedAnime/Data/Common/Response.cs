using RestSharp;

namespace UnifiedAnime.Data.Common
{
    public class Response
    {
        #region Properties
        
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }

        #endregion
    }

    public class Response<T> : Response
    {
        #region Properties

        public T Data { get; set; }

        #endregion
    }
}