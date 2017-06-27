using Newtonsoft.Json;
using UnifiedAnime.Model;

namespace UnifiedAnime.AniList.Model
{
    public class SmallAnime : SmallSeries, IAnimeInfo
    {
        [JsonProperty("total_episodes")]
        public int TotalEpisodes { get; set; }

        [JsonProperty("airing_status")]
        public string AiringStatus { get; set; }

        [JsonIgnore]
        public string ImageUrl => ImageUrlLge;
    }
}