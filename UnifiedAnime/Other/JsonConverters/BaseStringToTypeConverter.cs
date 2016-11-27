using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreCollections;
using Newtonsoft.Json;
using RestSharp.Extensions;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Other.JsonConverters
{
    public abstract class BaseStringToTypeConverter<T> : JsonConverter
    {
        protected abstract Map<string, T> Map { get; }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var result = TypeToString((T)value);

            if (result != null)
                writer.WriteValue(result);
            else
                writer.WriteNull();

        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = (string)reader.Value;
            return StringToType(value);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public string TypeToString(T t) => Map.Reverse[t];
        public T StringToType(string str) => Map.Forward[str];
    }
}
