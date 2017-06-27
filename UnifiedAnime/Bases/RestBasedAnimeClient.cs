using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using UnifiedAnime.Converters;

namespace UnifiedAnime.Bases
{
    public abstract class RestBasedAnimeClient
    {
        protected readonly RestClient Client;

        protected RestBasedAnimeClient(string url) => Client = new RestClient(url);

        protected virtual bool HandleUnauthorized() => false;

        protected IRestRequest MakeRequest(string resource, Method method)
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

            if (parameters == null)
                return request;

            foreach (var parameter in parameters)
                request.AddParameter(parameter.Name, parameter.Value);

            return request;
        }

        protected IRestResponse Execute(IRestRequest request) => Client.Execute(request);
        protected IRestResponse<T> Execute<T>(IRestRequest request)
        {
            var response = Execute(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized && HandleUnauthorized())
                response = Execute(request);

            var data = default(T);
            
                data = JsonConvert.DeserializeObject<T>(response.Content,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    });

            return new Response<T>(response, data);
        }

        protected IRestResponse MakeAndExecute(string resource, Method method, Parameters parameters) 
            => MakeAndExecute(resource, method, (IEnumerable<Parameter>)parameters);

        protected IRestResponse MakeAndExecute(string resource, Method method, params Parameter[] parameters) 
            => MakeAndExecute(resource, method, (IEnumerable<Parameter>)parameters);

        protected IRestResponse MakeAndExecute(string resource, Method method, string parameterName, object parameterValue) 
            => MakeAndExecute(resource, method, new Parameter { Name = parameterName, Value = parameterValue });

        protected IRestResponse MakeAndExecute(string resource, Method method, IEnumerable<Parameter> parameters = null) 
            => Execute(MakeRequest(resource, method, parameters));

        
        protected IRestResponse<T> MakeAndExecute<T>(string resource, Method method, Parameters parameters) 
            => MakeAndExecute<T>(resource, method, (IEnumerable<Parameter>)parameters);

        protected IRestResponse<T> MakeAndExecute<T>(string resource, Method method, params Parameter[] parameters) 
            => MakeAndExecute<T>(resource, method, (IEnumerable<Parameter>)parameters);

        protected IRestResponse<T> MakeAndExecute<T>(string resource, Method method, string parameterName, object parameterValue) 
            => MakeAndExecute<T>(resource, method, new Parameter { Name = parameterName, Value = parameterValue });

        protected IRestResponse<T> MakeAndExecute<T>(string resource, Method method, IEnumerable<Parameter> parameters = null)
            => Execute<T>(MakeRequest(resource, method, parameters));
    }
}
