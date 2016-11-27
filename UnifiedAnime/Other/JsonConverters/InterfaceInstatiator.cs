using System;
using Newtonsoft.Json;

namespace UnifiedAnime.Other.JsonConverters
{
    public class InterfaceInstatiator<TConcret, TInterface> : JsonConverter where TConcret : TInterface
    {
        #region Other Members

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TInterface);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            return serializer.Deserialize<TConcret>(reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}