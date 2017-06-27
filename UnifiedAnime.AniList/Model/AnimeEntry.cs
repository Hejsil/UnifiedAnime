using Newtonsoft.Json;
using UnifiedAnime.AniList.Converters;
using UnifiedAnime.Model;

namespace UnifiedAnime.AniList.Model
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