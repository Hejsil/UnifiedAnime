using Newtonsoft.Json;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Other.JsonConverters.AniList;

namespace UnifiedAnime.Data.AniList
{
    public class AnimeEntry : SeriesEntry
    {
        [JsonProperty("list_status")]
        [JsonConverter(typeof(AnimeEntryStatusMapper))]
        public AnimeEntryStatus ListStatus { get; set; }

        [JsonProperty("episodes_watched")]
        public int EpisodesWatched { get; set; }

        [JsonProperty("rewatched")]
        public int Rewatched { get; set; }

        [JsonProperty("anime")]
        public SmallAnime Anime { get; set; }
    }
}