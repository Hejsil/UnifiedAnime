using Newtonsoft.Json;
using UnifiedAnime.AniList.Converters;
using UnifiedAnime.Model;

namespace UnifiedAnime.AniList.Model
{
    public class User : SmallUser
    {
        [JsonProperty("anime_time")]
        public int AnimeTime { get; set; }

        [JsonProperty("manga_chap")]
        public int MangaChap { get; set; }

        [JsonProperty("about")]
        public string About { get; set; }

        [JsonProperty("list_order")]
        public ListOrder ListOrder { get; set; }

        [JsonProperty("adult_content")]
        public bool AdultContent { get; set; }

        [JsonProperty("following")]
        public bool Following { get; set; }

        [JsonProperty("image_url_banner")]
        public string ImageUrlBanner { get; set; }

        [JsonProperty("title_language")]
        [JsonConverter(typeof(TitleLanguageMapper))]
        public TitleLanguage TitleLanguage { get; set; }

        // TODO: Each integer is some kind of rating type. Make an enum 
        //       and a converter to convert the integer to that rating type
        [JsonProperty("score_type")]
        public ScoreSystem ScoreType { get; set; }

        [JsonProperty("custom_list_anime")]
        public string[] CustomListAnime { get; set; }

        [JsonProperty("custom_list_manga")]
        public string[] CustomListManga { get; set; }

        [JsonProperty("advanced_rating")]
        public bool AdvancedRating { get; set; }

        [JsonProperty("advanced_rating_names")]
        public string[] AdvancedRatingNames { get; set; }

        [JsonProperty("notifications")]
        public int Notifications { get; set; }
    }
}
