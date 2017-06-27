using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
{
    public class AnimeReview : Review
    {
        [JsonProperty("anime")]
        public SmallAnime Anime { get; set; }
    }
}
