using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class SmallAnime : SmallSeries
    {
        [JsonProperty("total_episodes")]
        public int TotalEpisodes { get; set; }

        [JsonProperty("airing_status")]
        public string AiringStatus { get; set; }
    }
}