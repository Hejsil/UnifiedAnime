using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class TaggedAnime : TaggedObject
    {
        [JsonProperty("anime")]
        public SmallAnime[] Anime { get; set; }
    }
}