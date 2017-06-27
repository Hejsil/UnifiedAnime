using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
{
    public class SmallManga : SmallSeries
    {
        [JsonProperty("total_chapters")]
        public int TotalChapters { get; set; }

        [JsonProperty("publishing_status")]
        public string PublishingStatus { get; set; }
    }
}