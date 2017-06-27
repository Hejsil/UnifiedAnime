using System;
using Newtonsoft.Json;
using UnifiedAnime.Collections;

namespace UnifiedAnime.Converters
{
    public abstract class TypeToTypeMapper<T1, T2> : JsonConverter
    {
        protected abstract Map<T1, T2> Map { get; }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var result = T2ToT1((T2)value);

            if (result != null)
                writer.WriteValue(result);
            else
                writer.WriteNull();

        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = (T1)reader.Value;
            return T1ToT2(value);
        }

        public override bool CanConvert(Type objectType) => objectType == typeof(T1);

        public T2 T1ToT2(T1 t1) => Map.Forward[t1];
        public T1 T2ToT1(T2 t2) => Map.Reverse[t2];
    }
}
