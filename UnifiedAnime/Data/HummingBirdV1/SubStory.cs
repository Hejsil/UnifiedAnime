using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnifiedAnime.Data.HummingBirdV1
{
    public class SubStory
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("substory_type")]
        public string SubstoryType { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("episode_number")]
        public string EpisodeNumber { get; set; }

        [JsonProperty("followed_user ")]
        public UserMini FollowedUser { get; set; }
    }
}
