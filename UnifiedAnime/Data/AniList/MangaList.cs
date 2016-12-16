using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class MangaList
    {
        [JsonProperty("watching")]
        public MangaEntry[] Watching { get; set; }

        [JsonProperty("plan_to_watch")]
        public MangaEntry[] PlanToWatch { get; set; }

        [JsonProperty("completed")]
        public MangaEntry[] Completed { get; set; }

        [JsonProperty("on_hold")]
        public MangaEntry[] OnHold { get; set; }

        [JsonProperty("dropped")]
        public MangaEntry[] Dropped { get; set; }
    }
}