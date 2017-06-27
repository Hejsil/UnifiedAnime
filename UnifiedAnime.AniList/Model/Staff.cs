using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
{
    public class Staff : SmallStaff
    {

        [JsonProperty("dob")]
        public int Dob { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("info")]
        public string Info { get; set; }

        [JsonProperty("name_first_japanese")]
        public string NameFirstJapanese { get; set; }

        [JsonProperty("name_last_japanese")]
        public string NameLastJapanese { get; set; }
    }
}