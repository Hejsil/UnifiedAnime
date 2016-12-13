﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class Favorites
    {
        [JsonProperty("anime")]
        public SmallAnime[] Anime { get; set; }

        [JsonProperty("manga")]
        public SmallManga[] Manga { get; set; }

        [JsonProperty("character")]
        public SmallCharacter[] Character { get; set; }

        [JsonProperty("staff")]
        public Staff[] Staff { get; set; }
    }

    public class Anime : SmallAnime
    {
        [JsonProperty("duration")]
        public int? Duration { get; set; }

        // TODO: https://anilist-api.readthedocs.io/en/latest/series.html

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


        // TODO: https://anilist-api.readthedocs.io/en/latest/series.html
        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        // TODO: https://anilist-api.readthedocs.io/en/latest/series.html
        [JsonProperty("end_date")]
        public string EndDate { get; set; }

        // TODO: https://anilist-api.readthedocs.io/en/latest/series.html
        [JsonProperty("season")]
        public int? Season { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("favourite")]
        public bool Favourite { get; set; }

        [JsonProperty("image_url_banner")]
        public object ImageUrlBanner { get; set; }

        [JsonProperty("score_distribution")]
        public object[] ScoreDistribution { get; set; }

        [JsonProperty("list_stats")]
        public object[] ListStats { get; set; }
    }
}
