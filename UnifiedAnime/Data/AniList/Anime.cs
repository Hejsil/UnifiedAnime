using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class Anime : Series
    {
        [JsonProperty("total_episodes")]
        public int TotalEpisodes { get; set; }

        [JsonProperty("duration")]
        public int? Duration { get; set; }

        // TODO: https://anilist-api.readthedocs.io/en/latest/series.html
        [JsonProperty("airing_status")]
        public string AiringStatus { get; set; }
        
        [JsonProperty("youtube_id")]
        public string YoutubeId { get; set; }

        [JsonProperty("hashtag")]
        public string Hashtag { get; set; }
        
        // TODO: https://anilist-api.readthedocs.io/en/latest/series.html
        [JsonProperty("source")]
        public string Source { get; set; }

        // TODO: https://anilist-api.readthedocs.io/en/latest/series.html
        [JsonProperty("airing_stats")]
        public object[] AiringStats { get; set; }
    }
}
