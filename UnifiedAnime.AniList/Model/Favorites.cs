using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
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