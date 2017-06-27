using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
{
    public class ScoreDistributions
    {
        [JsonProperty("anime")]
        public ScoreDistribution Anime { get; set; }

        [JsonProperty("manga")]
        public ScoreDistribution Manga { get; set; }
    }
}