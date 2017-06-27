using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
{
    public abstract class AniListObject
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}