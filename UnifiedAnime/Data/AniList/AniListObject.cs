using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class AniListObject
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}