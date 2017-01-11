using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class UserReviews
    {
        [JsonProperty("anime")]
        public AnimeReview[] AnimeReviews { get; set; }
        
        [JsonProperty("manga")]
        public MangaReview[] MangaReviews { get; set; }
    }
}
