using Newtonsoft.Json;

namespace UnifiedAnime.Data.HummingBirdV1
{
    /// <summary>
    /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Structures#user-object-mini
    /// </summary>
    public class UserMini
    {
        #region Properties
        
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("avatar_small")]
        public string AvatarSmall { get; set; }

        #endregion
    }
}