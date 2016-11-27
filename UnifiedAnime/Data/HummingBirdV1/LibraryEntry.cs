using System;
using System.Globalization;
using Newtonsoft.Json;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Clients.HummingBirdV1;
using UnifiedAnime.Other;
using UnifiedAnime.Other.JsonConverters;
using UnifiedAnime.Other.JsonConverters.HummingBirdV1;

namespace UnifiedAnime.Data.HummingBirdV1
{
    /// <summary>
    /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Structures#library-entry-object
    /// </summary>
    public class LibraryEntry : IAnimeEntry
    {
        #region Properties

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("episodes_watched")]
        public int EpisodesWatched { get; set; }

        [JsonProperty("last_watched")]
        public DateTime LastWatched { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("rewatched_times")]
        public int RewatchTimes { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("notes_present")]
        public bool NotesPresent { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(EntryStatusConverter))]
        public EntryStatus Status { get; set; }

        [JsonProperty("private")]
        public bool Private { get; set; }

        [JsonProperty("rewatching")]
        public bool Rewatching { get; set; }

        [JsonProperty("anime")]
        public Anime Anime { get; set; }

        [JsonProperty("rating")]
        public LibraryEntryRating Rating { get; set; }

        [JsonIgnore]
        public double Score
        {
            get { return ScoreConverter.HummingBirdToUnified(Rating.Value); }
            set { Rating.Value = ScoreConverter.UnifiedToHummingBird(value); }
        }

        #endregion
    }
}