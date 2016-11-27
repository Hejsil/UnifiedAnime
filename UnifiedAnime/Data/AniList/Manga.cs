using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class Manga : Series
    {
        [JsonProperty("total_chapters")]
        public int TotalChapters { get; set; }

        [JsonProperty("total_volumes")]
        public int TotalVolumes { get; set; }

        // TODO: https://anilist-api.readthedocs.io/en/latest/series.html
        [JsonProperty("publishing_status")]
        public string PublishingStatus { get; set; }
    }
}
