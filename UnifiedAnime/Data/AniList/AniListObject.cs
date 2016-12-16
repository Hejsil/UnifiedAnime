using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public abstract class AniListObject
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}