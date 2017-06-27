using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
{
    public class AnimeList
    {
        [JsonProperty("watching")]
        public AnimeEntry[] Watching { get; set; }

        [JsonProperty("plan_to_watch")]
        public AnimeEntry[] PlanToWatch { get; set; }

        [JsonProperty("completed")]
        public AnimeEntry[] Completed { get; set; }

        [JsonProperty("on_hold")]
        public AnimeEntry[] OnHold { get; set; }

        [JsonProperty("dropped")]
        public AnimeEntry[] Dropped { get; set; }
    }
}