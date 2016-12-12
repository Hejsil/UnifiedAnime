using System;
using Newtonsoft.Json;
using RestSharp;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Other;

namespace UnifiedAnime.Clients.Bases
{
    public abstract class RestBasedAnimeClient
    {
        public abstract string Url { get; }

        protected virtual IRestRequest MakeRequest(string resource, Method method) 
            => new RestRequest(resource, method) { JsonSerializer = NewtonsoftJsonSerializer.Default };

        protected Response Execute(IRestRequest request) =>  new Response(RestExecute(request));

        protected Response<T> Execute<T>(IRestRequest request)
        {
            var response = RestExecute(request);
            return new Response<T>(
                response, 
                JsonConvert.DeserializeObject<T>(response.Content,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    }));
        }

        protected Response MakeAndExecute(string resource, Method method)
        {
            var request = MakeRequest(resource, method);
            return Execute(request);
        }

        protected Response<T> MakeAndExecute<T>(string resource, Method method)
        {
            var request = MakeRequest(resource, method);
            return Execute<T>(request);
        }

        protected IRestResponse RestExecute(IRestRequest request)
        {
            var client = new RestClient(Url);
            return client.Execute(request);
        }

        protected IRestResponse MakeAndRestExecute(string resource, Method method)
        {
            var request = MakeRequest(resource, method);
            return RestExecute(request);
        }

    }
}
