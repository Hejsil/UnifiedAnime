using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace UnifiedAnime.Converters
{
    public class NewtonsoftJsonSerializer : ISerializer, IDeserializer
    {
        private readonly JsonSerializerSettings _settings;

        public NewtonsoftJsonSerializer(JsonSerializerSettings settings)
        {
            _settings = settings;
        }

        public string ContentType
        {
            get { return "application/json"; }
            set { }
        }

        public string DateFormat { get; set; }

        public string Namespace { get; set; }

        public string RootElement { get; set; }

        public T Deserialize<T>(IRestResponse response) => JsonConvert.DeserializeObject<T>(response.Content, _settings);
        public string Serialize(object obj) => JsonConvert.SerializeObject(obj, _settings);

        public static NewtonsoftJsonSerializer Default => new NewtonsoftJsonSerializer(new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}