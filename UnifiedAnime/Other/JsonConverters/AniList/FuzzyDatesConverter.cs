using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnifiedAnime.Other.JsonConverters.AniList
{
    public class FuzzyDatesConverter : JsonConverter
    {
        private const int YearDivider = 10000;
        private const int MonthDivider = 100;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = (DateTime)value;
            writer.WriteValue((date.Year * YearDivider) + (date.Month * MonthDivider) + date.Day);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return new DateTime();

            var value = (long)reader.Value;
            var year = value / YearDivider;
            var month = (value - (year * YearDivider)) / MonthDivider;
            var day = value - (year * YearDivider) - (month * MonthDivider);
            
            return new DateTime((int)year, (int)month, (int)day);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(long);
        }
    }
}
