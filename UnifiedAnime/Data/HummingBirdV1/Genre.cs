using Newtonsoft.Json;

namespace UnifiedAnime.Data.HummingBirdV1
{
    public class Genre
    {
        #region Properties

        [JsonProperty("name")]
        public string Name { get; set; }

        #endregion
    }
}