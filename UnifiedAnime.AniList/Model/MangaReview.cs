using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
{
    public class MangaReview : Review
    {
        [JsonProperty("manga")]
        public SmallManga Manga { get; set; }
    }
}
