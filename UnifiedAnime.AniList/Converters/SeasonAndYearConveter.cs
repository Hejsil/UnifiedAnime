using System;
using Newtonsoft.Json;
using UnifiedAnime.AniList.Model;

namespace UnifiedAnime.AniList.Converters
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
                writer.WriteValue((season.Year * YearDivider) + new IntSeasonMapper().T2ToT1(season.Season));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return null;

            var value = (int)(long)reader.Value;
            var year = value / YearDivider;
            var season = new IntSeasonMapper().T1ToT2(value - (year * YearDivider));

            return new SeasonAndYear {Season = season, Year = year};
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(int);
        }
    }
}
