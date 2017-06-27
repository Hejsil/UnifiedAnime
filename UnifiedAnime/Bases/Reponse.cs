using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;

namespace UnifiedAnime.Bases
{
    public class Response : IRestResponse
    {
        private readonly IRestResponse _response;

        public Response(IRestResponse response)
        {
            _response = response;

            while (_response is Response r)
                _response = r._response;
        }

        public IRestRequest Request { get => _response.Request; set => _response.Request = value; }
        public string ContentType { get => _response.ContentType; set => _response.ContentType = value; }
        public long ContentLength { get => _response.ContentLength; set => _response.ContentLength = value; }
        public string ContentEncoding { get => _response.ContentEncoding; set => _response.ContentEncoding = value; }
        public string Content { get => _response.Content; set => _response.Content = value; }
        public HttpStatusCode StatusCode { get => _response.StatusCode; set => _response.StatusCode = value; }
        public string StatusDescription { get => _response.StatusDescription; set => _response.StatusDescription = value; }
        public byte[] RawBytes { get => _response.RawBytes; set => _response.RawBytes = value; }
        public Uri ResponseUri { get => _response.ResponseUri; set => _response.ResponseUri = value; }
        public string Server { get => _response.Server; set => _response.Server = value; }

        public IList<RestResponseCookie> Cookies => _response.Cookies;

        public IList<Parameter> Headers => _response.Headers;

        public ResponseStatus ResponseStatus { get => _response.ResponseStatus; set => _response.ResponseStatus = value; }
        public string ErrorMessage { get => _response.ErrorMessage; set => _response.ErrorMessage = value; }
        public Exception ErrorException { get => _response.ErrorException; set => _response.ErrorException = value; }
    }

    public class Response<T> : Response, IRestResponse<T>
    {
        public Response(IRestResponse response, T data)
            : base(response) => Data = data;

        public T Data { get; set; }
    }
}
