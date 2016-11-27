using Newtonsoft.Json;
using UnifiedAnime.Other.JsonConverters;
using UnifiedAnime.Other.JsonConverters.HummingBirdV1;

namespace UnifiedAnime.Data.HummingBirdV1
{
    /// <summary>
    /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Structures#library-entry-rating-object
    /// </summary>
    public class LibraryEntryRating
    {
        #region Properties

        [JsonProperty("type")]
        [JsonConverter(typeof(RatingTypeMapper))]
        public RatingType Type { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }

        #endregion
    }
}