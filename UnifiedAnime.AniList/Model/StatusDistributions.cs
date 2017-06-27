using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
{
    public class StatusDistributions
    {
        [JsonProperty("anime")]
        public AnimeStatusDistribution Anime { get; set; }

        [JsonProperty("manga")]
        public MangaStatusDistribution Manga { get; set; }
    }
}