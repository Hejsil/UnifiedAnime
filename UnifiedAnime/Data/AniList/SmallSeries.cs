using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class SmallSeries : AniListObject
    {
        // TODO: https://anilist-api.readthedocs.io/en/latest/series.html
        [JsonProperty("series_type")]
        public string SeriesType { get; set; }

        [JsonProperty("title_romaji")]
        public string TitleRomaji { get; set; }

        [JsonProperty("title_english")]
        public string TitleEnglish { get; set; }

        [JsonProperty("title_japanese")]
        public string TitleJapanese { get; set; }

        [JsonProperty("type")]
        public MediaTypes Type { get; set; }

        [JsonProperty("start_date_fuzzy")]
        public int? StartDateFuzzy { get; set; }

        [JsonProperty("end_date_fuzzy")]
        public int? EndDateFuzzy { get; set; }

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
        public int UpdatedAt { get; set; }
    }
}