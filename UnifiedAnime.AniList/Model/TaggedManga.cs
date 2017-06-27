using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
{
    public class TaggedManga : TaggedObject
    {
        [JsonProperty("manga")]
        public SmallManga[] Manga { get; set; }
    }
}