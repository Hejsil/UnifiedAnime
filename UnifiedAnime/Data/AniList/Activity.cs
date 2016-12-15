using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnifiedAnime.Other.JsonConverters.AniList;

namespace UnifiedAnime.Data.AniList
{
    public class Activity : AniListObject
    {
        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("reply_count")]
        public int ReplyCount { get; set; }

        [JsonProperty("activity_type")]
        [JsonConverter(typeof(ActivityTypeMapper))]
        public ActivityType ActivityType { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("users")]
        public SmallUser[] Users { get; set; }

        [JsonProperty("series")]
        public SmallSeries Series { get; set; }

        [JsonProperty("messenger")]
        public object Messenger { get; set; }
    }
}
