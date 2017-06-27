using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
{
    public class Character : SmallCharacter
    {
        [JsonProperty("name_alt")]
        public string NameAlt { get; set; }

        [JsonProperty("info")]
        public string Info { get; set; }

        [JsonProperty("name_japanese")]
        public string NameJapanese { get; set; }
    }
}