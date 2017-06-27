using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
{
    public class BigUserMangaList : BigUser
    {
        [JsonProperty("lists")]
        public MangaList MangaList { get; set; }

        // TODO: The custom list data received seems inconsistent and broken.
        //[JsonProperty("custom_lists")]
        //public MangaEntry[][] CustomLists { get; set; }
    }
}