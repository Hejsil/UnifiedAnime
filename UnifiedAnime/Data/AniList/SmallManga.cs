using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class SmallManga : SmallSeries
    {
        [JsonProperty("total_chapters")]
        public int TotalChapters { get; set; }

        [JsonProperty("publishing_status")]
        public string PublishingStatus { get; set; }
    }
}