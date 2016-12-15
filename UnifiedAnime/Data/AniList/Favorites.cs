using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class Favorites
    {
        [JsonProperty("anime")]
        public SmallAnime[] Anime { get; set; }

        [JsonProperty("manga")]
        public SmallManga[] Manga { get; set; }

        [JsonProperty("character")]
        public SmallCharacter[] Character { get; set; }

        [JsonProperty("staff")]
        public Staff[] Staff { get; set; }
    }
}