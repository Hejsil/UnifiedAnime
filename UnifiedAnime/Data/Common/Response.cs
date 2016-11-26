using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace UnifiedAnime.Data.Common
{
    public class Response
    {
        public ResponseStatus Status { get; set; }
        public IRestResponse RestResponse { get; set; }
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }
    }
}
