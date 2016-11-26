using System;
using System.Globalization;
using Newtonsoft.Json;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Data.HummingBirdV1
{
    public class LibraryEntry : IAnimeEntry
    {
        #region Properties

        [JsonProperty("anime")]
        public Anime Anime { get; set; }

        [JsonProperty("episodes_watched")]
        public int EpisodesWatched { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("last_watched")]
        public DateTime LastWatched { get; set; }

        [JsonProperty("rating")]
        public LibraryEntryRating LibraryEntryRating { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("notes_present")]
        public bool NotesPresent { get; set; }

        [JsonProperty("private")]
        public bool Private { get; set; }

        [JsonProperty("rewatching")]
        public bool Rewatching { get; set; }

        [JsonProperty("rewatched_times")]
        public int RewatchTimes { get; set; }

        [JsonIgnore]
        public double Score
        {
            get
            {
                double result;
                if (double.TryParse(LibraryEntryRating.Value, out result))
                    return LibraryEntryRating.ConvertToUnifiedAnimeScore(result);

                return 0.0;
            }
            set
            {
                LibraryEntryRating.Value =
                    LibraryEntryRating.ConvertToHummingBirdRating(value).ToString(CultureInfo.InvariantCulture);
            }
        }

        public AnimeStatus Status
        {
            get { return ConvertToUnifiedAnimeStatus(StringStatus); }
            set { StringStatus = ConvertToHummingBirdStatus(value); }
        }

        [JsonProperty("status")]
        public string StringStatus { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        #endregion

        #region Other Members

        public static string ConvertToHummingBirdStatus(AnimeStatus status)
        {
            switch (status)
            {
                case AnimeStatus.Watching:
                    return "currently-watching";
                case AnimeStatus.PlanToWatch:
                    return "plan-to-watch";
                case AnimeStatus.Completed:
                    return "completed";
                case AnimeStatus.OnHold:
                    return "on-hold";
                case AnimeStatus.Dropped:
                    return "dropped";
            }

            return "";
        }

        public static AnimeStatus ConvertToUnifiedAnimeStatus(string status)
        {
            switch (status)
            {
                case "currently-watching":
                    return AnimeStatus.Watching;
                case "plan-to-watch":
                    return AnimeStatus.PlanToWatch;
                case "completed":
                    return AnimeStatus.Completed;
                case "on-hold":
                    return AnimeStatus.OnHold;
                case "dropped":
                    return AnimeStatus.Dropped;
            }

            return (AnimeStatus) (-1);
        }

        #endregion
    }
}