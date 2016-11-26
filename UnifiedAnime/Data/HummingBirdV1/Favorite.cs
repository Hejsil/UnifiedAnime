using System;
using Newtonsoft.Json;

namespace UnifiedAnime.Data.HummingBirdV1
{
    public class Favorite
    {
        #region Properties

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("item_id")]
        public int ItemId { get; set; }

        [JsonProperty("item_type")]
        public string ItemType { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        #endregion
    }
}