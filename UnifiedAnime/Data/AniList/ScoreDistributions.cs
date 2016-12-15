using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class ScoreDistributions
    {
        [JsonProperty("anime")]
        public ScoreDistribution Anime { get; set; }

        [JsonProperty("manga")]
        public ScoreDistribution Manga { get; set; }
    }
}