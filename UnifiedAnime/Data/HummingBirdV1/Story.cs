using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnifiedAnime.Data.HummingBirdV1
{
    public class Story : IFeedEntry
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("story_type")]
        public string StoryType { get; set; }

        [JsonProperty("user")]
        public UserMini User { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("self_post")]
        public bool SelfPost { get; set; }

        [JsonProperty("poster")]
        public UserMini Poster { get; set; }

        [JsonProperty("substories_count")]
        public int SubstoriesCount { get; set; }

        [JsonProperty("substories")]
        public IList<SubStory> Substories { get; set; }
    }
}
