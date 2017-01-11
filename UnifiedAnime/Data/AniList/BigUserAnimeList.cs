using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class BigUserAnimeList : BigUser
    {
        [JsonProperty("lists")]
        public AnimeList AnimeList { get; set; }

        // TODO: The custom list data received seems inconsistent and broken.
        //[JsonProperty("custom_lists")]
        //public AnimeEntry[][] CustomLists { get; set; }
    }
}