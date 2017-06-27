using Newtonsoft.Json;
using UnifiedAnime.AniList.Converters;
using UnifiedAnime.Model;

namespace UnifiedAnime.AniList.Model
{
    public class Anime : SmallAnime
    {
        [JsonProperty("duration")]
        public int? Duration { get; set; }

        // TODO: https://anilist-api.readthedocs.io/en/latest/series.html
        [JsonProperty("youtube_id")]
        public string YoutubeId { get; set; }

        [JsonProperty("hashtag")]
        public string Hashtag { get; set; }
        
        [JsonProperty("source")]
        [JsonConverter(typeof(AnimeSourceMapper))]
        public AnimeSource Source { get; set; }

        // TODO: https://anilist-api.readthedocs.io/en/latest/series.html
        [JsonProperty("airing_stats")]
        public object[] AiringStats { get; set; }
        
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
        public string ImageUrlBanner { get; set; }

        [JsonProperty("score_distribution")]
        public ScoreDistribution ScoreDistribution { get; set; }

        [JsonProperty("list_stats")]
        public ListStats ListStats { get; set; }
    }
}
