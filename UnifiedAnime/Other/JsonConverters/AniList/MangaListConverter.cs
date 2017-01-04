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
    public class MangaListConverter : JsonConverter
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
            var lists = jObject["lists"];
            var watching = lists["reading"].ToObject<List<MangaEntry>>();
            var planToWatch = lists["plan_to_read"].ToObject<MangaEntry[]>();
            var completed = lists["on_hold"].ToObject<MangaEntry[]>();
            var onHold = lists["completed"].ToObject<MangaEntry[]>();
            var dropped = lists["dropped"].ToObject<MangaEntry[]>();

            watching.AddRange(planToWatch);
            watching.AddRange(completed);
            watching.AddRange(onHold);
            watching.AddRange(dropped);
            return watching.ToArray();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(MangaEntry[]);
        }

        public override bool CanWrite => false;
    }
}
