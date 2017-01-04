using System;
using Newtonsoft.Json;
using UnifiedAnime.Other;
using UnifiedAnime.Other.JsonConverters.AniList;

namespace UnifiedAnime.Data.AniList
{
    public class SmallSeries : AniListObject
    {
        [JsonProperty("series_type")]
        [JsonConverter(typeof(SeriesTypeMapper))]
        public SeriesType SeriesType { get; set; }

        [JsonProperty("title_romaji")]
        public string TitleRomaji { get; set; }

        [JsonProperty("title_english")]
        public string TitleEnglish { get; set; }

        [JsonProperty("title_japanese")]
        public string TitleJapanese { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(MediaTypeMapper))]
        public MediaType Type { get; set; }

        [JsonProperty("start_date_fuzzy")]
        [JsonConverter(typeof(FuzzyDatesConverter))]
        public DateTime StartDateFuzzy { get; set; }

        [JsonProperty("end_date_fuzzy")]
        [JsonConverter(typeof(FuzzyDatesConverter))]
        public DateTime EndDateFuzzy { get; set; }

        [JsonProperty("synonyms")]
        public string[] Synonyms { get; set; }

        [JsonProperty("genres")]
        public string[] Genres { get; set; }

        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("average_score")]
        public double AverageScore { get; set; }

        [JsonProperty("popularity")]
        public int Popularity { get; set; }

        [JsonProperty("image_url_sml")]
        public string ImageUrlSml { get; set; }

        [JsonProperty("image_url_med")]
        public string ImageUrlMed { get; set; }

        [JsonProperty("image_url_lge")]
        public string ImageUrlLge { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(UnixTimestampConveter))]
        public DateTime UpdatedAt { get; set; }
    }
}