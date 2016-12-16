using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class MangaEntry : SeriesEntry
    {
        // TODO: Use enum
        [JsonProperty("list_status")]
        public string ListStatus { get; set; }

        [JsonProperty("chapters_read")]
        public int ChaptersRead { get; set; }

        [JsonProperty("volumes_read")]
        public int VolumesRead { get; set; }

        [JsonProperty("reread")]
        public int Reread { get; set; }

        [JsonProperty("manga")]
        public SmallManga Manga { get; set; }
    }
}