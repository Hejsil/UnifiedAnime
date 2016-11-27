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
    public abstract class TypeToTypeMapper<T1, T2> : JsonConverter
    {
        protected abstract Map<T1, T2> Map { get; }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var result = Type2ToType1((T2)value);

            if (result != null)
                writer.WriteValue(result);
            else
                writer.WriteNull();

        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = (T1)reader.Value;
            return Type1ToType2(value);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(T1);
        }

        public T2 Type1ToType2(T1 t1) => Map.Forward[t1];
        public T1 Type2ToType1(T2 t2) => Map.Reverse[t2];
    }
}
