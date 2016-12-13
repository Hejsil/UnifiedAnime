using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class Tag : AniListObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}