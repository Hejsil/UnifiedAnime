using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnifiedAnime.Data.AniList;

namespace UnifiedAnime.Other.JsonConverters.AniList
{
    public class SeasonAndYearConveter : JsonConverter
    {
        private const int YearDivider = 10;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var season = (SeasonAndYear)value;

            if (season == null)
                writer.WriteNull();
            else
                writer.WriteValue((season.Year * YearDivider) + new SeasonMapper().Type2ToType1(season.Season));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return null;

            var value = (int)reader.Value;
            var year = value / YearDivider;
            var season = new SeasonMapper().Type1ToType2(value - (year * YearDivider));

            return new SeasonAndYear {Season = season, Year = year};
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(int);
        }
    }
}
