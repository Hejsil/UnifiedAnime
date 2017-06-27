﻿using System;
using System.Globalization;
using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Converters
{
    public class NullableDateTimeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var date = (DateTime)value;
            writer.WriteValue(date.ToString("yyyy/MM/dd"));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return null;

            var value = (string)reader.Value;
            DateTime result;
            if (DateTime.TryParseExact(value, "yyyy/MM/dd", null, DateTimeStyles.None, out result))
                return result;

            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
