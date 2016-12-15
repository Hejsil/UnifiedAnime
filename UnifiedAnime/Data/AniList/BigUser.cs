using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class BigUser : User
    {
        [JsonProperty("forum_homepage")]
        public int ForumHomepage { get; set; }

        [JsonProperty("legacy_lists")]
        public bool LegacyLists { get; set; }

        [JsonProperty("donator")]
        public int Donator { get; set; }

        [JsonProperty("stats")]
        public Stats Stats { get; set; }

        [JsonProperty("airing_notifications")]
        public int AiringNotifications { get; set; }

        [JsonProperty("custom_lists")]
        public string[] CustomLists { get; set; }

        [JsonProperty("css")]
        public string[] Css { get; set; }

        [JsonProperty("lists")]
        public SeriesList SeriesList { get; set; }
    }
}