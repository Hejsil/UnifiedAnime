using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class SeriesList
    {
        [JsonProperty("watching")]
        public SeriesEntry[] Watching { get; set; }

        [JsonProperty("plan_to_watch")]
        public SeriesEntry[] PlanToWatch { get; set; }

        [JsonProperty("completed")]
        public SeriesEntry[] Completed { get; set; }

        [JsonProperty("on_hold")]
        public SeriesEntry[] OnHold { get; set; }

        [JsonProperty("dropped")]
        public SeriesEntry[] Dropped { get; set; }
    }
}