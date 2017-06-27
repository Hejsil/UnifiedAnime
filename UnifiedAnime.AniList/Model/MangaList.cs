using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
{
    public class MangaList
    {
        [JsonProperty("reading")]
        public MangaEntry[] Reading { get; set; }

        [JsonProperty("plan_to_read")]
        public MangaEntry[] PlanToRead { get; set; }

        [JsonProperty("on_hold")]
        public MangaEntry[] OnHold { get; set; }

        [JsonProperty("completed")]
        public MangaEntry[] Completed { get; set; }

        [JsonProperty("dropped")]
        public MangaEntry[] Dropped { get; set; }
    }
}