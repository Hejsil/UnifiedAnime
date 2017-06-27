using Newtonsoft.Json;
using UnifiedAnime.AniList.Converters;

namespace UnifiedAnime.AniList.Model
{
    public class Manga : SmallManga
    {
        [JsonProperty("total_volumes")]
        public int TotalVolumes { get; set; }

        // NOTE: The api states that this is deprecated, so we wont save it.
        //[JsonProperty("start_date")]
        //public string StartDate { get; set; }

        // NOTE: The api states that this is deprecated, so we wont save it.
        //[JsonProperty("end_date")]
        //public string EndDate { get; set; }

        [JsonProperty("season")]
        [JsonConverter(typeof(SeasonAndYearConveter))]
        public SeasonAndYear Season { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("favourite")]
        public bool Favourite { get; set; }

        [JsonProperty("image_url_banner")]
        public object ImageUrlBanner { get; set; }

        [JsonProperty("score_distribution")]
        public ScoreDistribution ScoreDistribution { get; set; }

        [JsonProperty("list_stats")]
        public ListStats ListStats { get; set; }
    }
}
