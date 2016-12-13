using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class TaggedObject : AniListObject
    {
        [JsonProperty("thread_id")]
        public int ThreadId { get; set; }

        [JsonProperty("tag_id")]
        public int TagId { get; set; }
    }
}