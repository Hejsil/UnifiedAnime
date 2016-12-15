using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class AnimeStatusDistribution
    {
        [JsonProperty("watching")]
        public int Watching { get; set; }

        [JsonProperty("plan to watch")]
        public int PlanToWatch { get; set; }

        [JsonProperty("completed")]
        public int Completed { get; set; }

        [JsonProperty("dropped")]
        public int Dropped { get; set; }

        [JsonProperty("on-hold")]
        public int OnHold { get; set; }
    }
}