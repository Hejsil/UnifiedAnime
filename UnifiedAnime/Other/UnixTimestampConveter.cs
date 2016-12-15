using System;
using Newtonsoft.Json;

namespace UnifiedAnime.Other
{
    public class UnixTimestampConveter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = (DateTime)value;
            writer.WriteValue((date.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds); 
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = (long)reader.Value;
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(value).ToLocalTime();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(int);
        }
    }
}
