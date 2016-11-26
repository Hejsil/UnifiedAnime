using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnifiedAnime.Json
{
    public class InterfaceInstatiator<TConcret, TInterface> : JsonConverter where TConcret : TInterface
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<TConcret>(reader);
        }
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TInterface);
        }
    }
}
