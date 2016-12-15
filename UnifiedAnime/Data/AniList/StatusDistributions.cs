using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class StatusDistributions
    {
        [JsonProperty("anime")]
        public AnimeStatusDistribution Anime { get; set; }

        [JsonProperty("manga")]
        public MangaStatusDistribution Manga { get; set; }
    }
}