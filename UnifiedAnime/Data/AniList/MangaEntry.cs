﻿using Newtonsoft.Json;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Other.JsonConverters.AniList;

namespace UnifiedAnime.Data.AniList
{
    public class MangaEntry : SeriesEntry
    {
        [JsonProperty("list_status")]
        [JsonConverter(typeof(MangaEntryStatusMapper))]
        public MangaEntryStatus ListStatus { get; set; }

        [JsonProperty("chapters_read")]
        public int ChaptersRead { get; set; }

        [JsonProperty("volumes_read")]
        public int VolumesRead { get; set; }

        [JsonProperty("reread")]
        public int Reread { get; set; }

        [JsonProperty("manga")]
        public SmallManga Manga { get; set; }
    }
}