using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnifiedAnime.Data.AniList;

namespace UnifiedAnime.Other.JsonConverters.AniList
{
    public class ThreadSearchConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // NOTE: This seems to be a fine way of concating all the elements of all the fields.
            //       It might be better to concat the JArrays, thought. I don't know what method would
            //       would work best.
            var jObject = JObject.Load(reader);
            var threads = jObject["data"];
            return threads.ToObject<AniListThread[]>();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(AniListThread[]);
        }

        public override bool CanWrite => false;
    }
}
