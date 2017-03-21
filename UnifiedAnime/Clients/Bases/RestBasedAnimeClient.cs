using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Other;

namespace UnifiedAnime.Clients.Bases
{
    public abstract class RestBasedAnimeClient
    {
        private readonly RestClient _client;

        protected RestBasedAnimeClient(string url)
        {
            _client = new RestClient(url);
        }

        protected virtual IRestRequest MakeRequest(string resource, Method method)
            => new RestRequest(resource, method) { JsonSerializer = NewtonsoftJsonSerializer.Default };

        protected IRestRequest MakeRequest(string resource, Method method, Parameters parameters)
            => MakeRequest(resource, method, (IEnumerable<Parameter>)parameters);

        protected IRestRequest MakeRequest(string resource, Method method, params Parameter[] parameters)
            => MakeRequest(resource, method, (IEnumerable<Parameter>)parameters);

        protected IRestRequest MakeRequest(string resource, Method method, string parameterName, object parameterValue)
            => MakeRequest(resource, method, new Parameter { Name = parameterName, Value = parameterValue });

        protected IRestRequest MakeRequest(string resource, Method method, IEnumerable<Parameter> parameters)
        {
            var request = MakeRequest(resource, method);

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                    request.AddParameter(parameter.Name, parameter.Value);
            }

            return request;
        }

        protected IRestResponse Execute(IRestRequest request) => _client.Execute(request);
        protected (T Data, IRestResponse RestResponse) Execute<T>(IRestRequest request)
        {
            var response = Execute(request);
            var data = default(T);

            try
            {
                data = JsonConvert.DeserializeObject<T>(response.Content,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    });
            }
            catch { }

            return (data, response);
        }

        protected IRestResponse MakeAndExecute(string resource, Method method, Parameters parameters) 
            => MakeAndExecute(resource, method, (IEnumerable<Parameter>)parameters);

        protected IRestResponse MakeAndExecute(string resource, Method method, params Parameter[] parameters) 
            => MakeAndExecute(resource, method, (IEnumerable<Parameter>)parameters);

        protected IRestResponse MakeAndExecute(string resource, Method method, string parameterName, object parameterValue) 
            => MakeAndExecute(resource, method, new Parameter { Name = parameterName, Value = parameterValue });

        protected IRestResponse MakeAndExecute(string resource, Method method, IEnumerable<Parameter> parameters = null) 
            => Execute(MakeRequest(resource, method, parameters));

        
        protected (T Data, IRestResponse RestResponse) MakeAndExecute<T>(string resource, Method method, Parameters parameters) 
            => MakeAndExecute<T>(resource, method, (IEnumerable<Parameter>)parameters);

        protected (T Data, IRestResponse RestResponse) MakeAndExecute<T>(string resource, Method method, params Parameter[] parameters) 
            => MakeAndExecute<T>(resource, method, (IEnumerable<Parameter>)parameters);

        protected (T Data, IRestResponse RestResponse) MakeAndExecute<T>(string resource, Method method, string parameterName, object parameterValue) 
            => MakeAndExecute<T>(resource, method, new Parameter { Name = parameterName, Value = parameterValue });

        protected (T Data, IRestResponse RestResponse) MakeAndExecute<T>(string resource, Method method, IEnumerable<Parameter> parameters = null)
            => Execute<T>(MakeRequest(resource, method, parameters));
    }
}
