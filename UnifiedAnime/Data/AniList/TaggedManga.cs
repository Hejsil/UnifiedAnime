using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class TaggedManga : TaggedObject
    {
        [JsonProperty("manga")]
        public SmallManga[] Manga { get; set; }
    }
}