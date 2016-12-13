using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UnifiedAnime.Data.AniList
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

        // TODO: Make DateTime
        [JsonProperty("deleted_at")]
        public string DeletedAt { get; set; }

        // TODO: Make DateTime
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

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
        public object[] TagsManga { get; set; }

        [JsonProperty("user")]
        public SmallUser Creator { get; set; }

        [JsonProperty("reply_user")]
        public SmallUser CommentedLast { get; set; }

        [JsonProperty("comments")]
        public Comment[] Comments { get; set; }
    }
}
