using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
{
    public class Comment : AniListObject
    {
        [JsonProperty("parent_id")]
        public int? ParentId { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("thread_id")]
        public int ThreadId { get; set; }

        [JsonProperty("comment")]
        public string Content { get; set; }

        // TODO: Make DateTime
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        // TODO: Make DateTime
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("children")]
        public Comment[] Children { get; set; }
    }
}