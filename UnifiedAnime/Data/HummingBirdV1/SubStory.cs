using System;
using Newtonsoft.Json;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Other.JsonConverters.HummingBirdV1;

namespace UnifiedAnime.Data.HummingBirdV1
{
    /// <summary>
    /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Structures#substory-object
    /// </summary>
    public class SubStory
    {
        #region Properties
        
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("substory_type")]
        [JsonConverter(typeof(SubStoryTypeMapper))]
        public SubStoryType SubstoryType { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("episode_number")]
        public string EpisodeNumber { get; set; }

        [JsonProperty("followed_user ")]
        public UserMini FollowedUser { get; set; }
        
        [JsonProperty("new_status")]
        [JsonConverter(typeof(AnimeEntryStatusMapper))]
        public AnimeEntryStatus NewStatus { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        #endregion
    }
}