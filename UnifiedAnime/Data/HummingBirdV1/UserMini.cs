using Newtonsoft.Json;

namespace UnifiedAnime.Data.HummingBirdV1
{
    public class UserMini
    {
        #region Properties

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("avatar_small")]
        public string AvatarSmall { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        #endregion
    }
}