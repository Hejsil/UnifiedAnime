using System;
using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    // TODO: This type needs to be cleaned up somehow.
    //       AniList really made a mess here...
    public class SeriesEntry
    {
        [JsonProperty("record_id")]
        public int RecordId { get; set; }

        [JsonProperty("series_id")]
        public int SeriesId { get; set; }

        [JsonProperty("list_status")]
        public string ListStatus { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("score_raw")]
        public int ScoreRaw { get; set; }

        [JsonProperty("episodes_watched")]
        public int EpisodesWatched { get; set; }

        [JsonProperty("chapters_read")]
        public int ChaptersRead { get; set; }

        [JsonProperty("volumes_read")]
        public int VolumesRead { get; set; }

        [JsonProperty("rewatched")]
        public int Rewatched { get; set; }

        [JsonProperty("reread")]
        public int Reread { get; set; }

        [JsonProperty("priority")]
        public int Priority { get; set; }

        [JsonProperty("private")]
        public int Private { get; set; }

        [JsonProperty("hidden_default")]
        public int HiddenDefault { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("advanced_rating_scores")]
        public string[] AdvancedRatingScores { get; set; }

        [JsonProperty("custom_lists")]
        public string[] CustomLists { get; set; }

        // TODO: Figure these out
        //[JsonProperty("started_on")]
        //public object StartedOn { get; set; }

        // TODO: Figure these out
        //[JsonProperty("finished_on")]
        //public object FinishedOn { get; set; }

        [JsonProperty("added_time")]
        public DateTime AddedTime { get; set; }

        [JsonProperty("updated_time")]
        public DateTime UpdatedTime { get; set; }

        [JsonProperty("anime")]
        public SmallAnime Anime { get; set; }

        [JsonProperty("manga")]
        public SmallManga Manga { get; set; }
    }
}