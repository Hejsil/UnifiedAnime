using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
{
    public class Tag : AniListObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}