﻿using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
{
    public class SmallUser : AniListObject
    {
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("image_url_lge")]
        public string ImageUrlLge { get; set; }

        [JsonProperty("image_url_med")]
        public string ImageUrlMed { get; set; }
    }
}
