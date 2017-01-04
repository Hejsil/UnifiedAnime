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
        private const int OnlyYear = 10000;
        private const int OnlyYearAndMonth = 1000000;

        private const int YearDivider = 10000;
        private const int MonthDivider = 100;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = (DateTime)value;
            writer.WriteValue((date.Year * YearDivider) + (date.Month * MonthDivider) + date.Day);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            long year, month = 1, day = 1;

            if (reader.Value == null)
                return new DateTime();

            var value = (long)reader.Value;

            if (OnlyYear > value)
            {
                year = value;
            }
            else if (OnlyYearAndMonth > value)
            {
                year = value / MonthDivider;
                month = value - (year * MonthDivider);
            }
            else
            {
                year = value / YearDivider;
                month = (value - (year * YearDivider)) / MonthDivider;
                day = value - (year * YearDivider) - (month * MonthDivider);
            }

            if (month == 0)
                month = 1;
            if (day == 0)
                day = 1;
            
            return new DateTime((int)year, (int)month, (int)day);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(long);
        }
    }
}
