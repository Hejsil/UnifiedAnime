using System;
using Newtonsoft.Json;
using UnifiedAnime.Other.JsonConverters.HummingBirdV1;

namespace UnifiedAnime.Data.HummingBirdV1
{
    /// <summary>
    /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Structures#story-object
    /// </summary>
    public class Story : IFeedEntry
    {
        #region Properties

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("story_type")]
        [JsonConverter(typeof(StoryTypeConverter))]
        public StoryType StoryType { get; set; }

        [JsonProperty("user")]
        public UserMini User { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("media")]
        public Anime Media { get; set; }

        [JsonProperty("self_post")]
        public bool SelfPost { get; set; }

        [JsonProperty("poster")]
        public UserMini Poster { get; set; }

        [JsonProperty("substories_count")]
        public int SubstoriesCount { get; set; }

        [JsonProperty("substories")]
        public SubStory[] Substories { get; set; }

        #endregion
    }
}