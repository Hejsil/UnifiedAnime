using System;
using Newtonsoft.Json;
using UnifiedAnime.Other.JsonConverters.AniList;

namespace UnifiedAnime.Data.AniList
{
    public class SeriesEntry
    {
        [JsonProperty("record_id")]
        public int RecordId { get; set; }

        [JsonProperty("series_id")]
        public int SeriesId { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("score_raw")]
        public int ScoreRaw { get; set; }

        // TODO: What is this used for. I don't know... DAMN YOU DOCUMENTATION!
        [JsonProperty("priority")]
        public int Priority { get; set; }

        [JsonProperty("private")]
        public bool Private { get; set; }

        [JsonProperty("hidden_default")]
        public bool HiddenDefault { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }
        
        [JsonProperty("advanced_rating_scores")]
        public int[] AdvancedRatingScores { get; set; }
        
        [JsonProperty("custom_lists")]
        public int[] CustomLists { get; set; }
        
        [JsonProperty("started_on")]
        [JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? StartedOn { get; set; }
        
        [JsonProperty("finished_on")]
        [JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? FinishedOn { get; set; }

        [JsonProperty("added_time")]
        public DateTime AddedTime { get; set; }

        [JsonProperty("updated_time")]
        public DateTime UpdatedTime { get; set; }
    }
}