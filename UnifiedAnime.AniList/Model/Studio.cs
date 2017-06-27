using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
{
    public class Studio : AniListObject
    {
        [JsonProperty("studio_name")]
        public string StudioName { get; set; }

        [JsonProperty("studio_wiki")]
        public string StudioWiki { get; set; }
        
        [JsonProperty("main_studio")]
        public object MainStudio { get; set; }
    }
}