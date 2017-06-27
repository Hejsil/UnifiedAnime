using System;
using Newtonsoft.Json;
using UnifiedAnime.AniList.Converters;

namespace UnifiedAnime.AniList.Model
{
    public class AniListThread : AniListObject
    {
        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("sticky")]
        public bool Sticky { get; set; }

        [JsonProperty("locked")]
        public bool Locked { get; set; }

        [JsonProperty("last_reply")]
        public string LastReply { get; set; }

        [JsonProperty("last_reply_user")]
        public int LastReplyUser { get; set; }
        
        [JsonProperty("deleted_at")]
        [JsonConverter(typeof(FullDateTimeConverter))]
        public DateTime DeletedAt { get; set; }
        
        [JsonProperty("created_at")]
        [JsonConverter(typeof(FullDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("reply_count")]
        public int ReplyCount { get; set; }

        [JsonProperty("view_count")]
        public int ViewCount { get; set; }

        [JsonProperty("subscribed")]
        public bool Subscribed { get; set; }

        [JsonProperty("page_data")]
        public PageData PageData { get; set; }

        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }

        [JsonProperty("tags_anime")]
        public TaggedAnime[] TagsAnime { get; set; }

        [JsonProperty("tags_manga")]
        public TaggedManga[] TagsManga { get; set; }

        [JsonProperty("user")]
        public SmallUser Creator { get; set; }

        [JsonProperty("reply_user")]
        public SmallUser CommentedLast { get; set; }

        [JsonProperty("comments")]
        public Comment[] Comments { get; set; }
    }
}
