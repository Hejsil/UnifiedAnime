using Newtonsoft.Json;

namespace UnifiedAnime.Data.HummingBirdV1
{
    public class Genre
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}