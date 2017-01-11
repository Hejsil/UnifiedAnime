using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class AnimeReview : Review
    {
        [JsonProperty("anime")]
        public SmallAnime Anime { get; set; }
    }
}
