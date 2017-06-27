using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
{
    public class UserReviews
    {
        [JsonProperty("anime")]
        public AnimeReview[] AnimeReviews { get; set; }
        
        [JsonProperty("manga")]
        public MangaReview[] MangaReviews { get; set; }
    }
}
