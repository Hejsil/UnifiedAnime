using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class MangaStatusDistribution
    {
        [JsonProperty("reading")]
        public int Reading { get; set; }

        [JsonProperty("plan to read")]
        public int PlanToRead { get; set; }

        [JsonProperty("completed")]
        public int Completed { get; set; }

        [JsonProperty("dropped")]
        public int Dropped { get; set; }

        [JsonProperty("on-hold")]
        public int OnHold { get; set; }
    }
}