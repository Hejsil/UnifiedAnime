using System;
using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Converters
{
    public class FullDateTimeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = (DateTime)value;
            writer.WriteValue(date.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = (string)reader.Value;
            return DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
