using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class SmallCharacter : AniListObject
    {
        [JsonProperty("name_first")]
        public string NameFirst { get; set; }

        [JsonProperty("name_last")]
        public string NameLast { get; set; }

        [JsonProperty("image_url_lge")]
        public string ImageUrlLge { get; set; }

        [JsonProperty("image_url_med")]
        public string ImageUrlMed { get; set; }

        [JsonProperty("role")]
        public object Role { get; set; }
    }
}