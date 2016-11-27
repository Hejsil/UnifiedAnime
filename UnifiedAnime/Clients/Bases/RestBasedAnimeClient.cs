using System;
using Newtonsoft.Json;
using RestSharp;
using UnifiedAnime.Other;

namespace UnifiedAnime.Clients.Bases
{
    public abstract class RestBasedAnimeClient
    {
        public abstract string Url { get; }

        public RestRequest MakeRequest(string resource, Method method)
        {
            return new RestRequest(resource, method) { JsonSerializer = NewtonsoftJsonSerializer.Default };
        }

        public IRestResponse Execute(RestRequest request)
        {
            var client = new RestClient(Url);
            return client.Execute(request);
        }

        public Tuple<IRestResponse, T> Execute<T>(RestRequest request)
        {
            var response = Execute(request);
            return new Tuple<IRestResponse, T>(
                response, 
                JsonConvert.DeserializeObject<T>(response.Content,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    }));
        }
    }
}
