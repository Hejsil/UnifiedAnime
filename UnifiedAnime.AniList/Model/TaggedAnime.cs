using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
{
    public class TaggedAnime : TaggedObject
    {
        [JsonProperty("anime")]
        public SmallAnime[] Anime { get; set; }
    }
}