using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class PageData
    {

        [JsonProperty("total_root")]
        public int TotalRoot { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }

        [JsonProperty("last_page")]
        public int LastPage { get; set; }

        [JsonProperty("from")]
        public int From { get; set; }

        [JsonProperty("to")]
        public int To { get; set; }
    }
}