using System;
using Newtonsoft.Json;

namespace UnifiedAnime.Data.HummingBirdV1
{
    public class User : IUserInfo
    {
        #region Properties

        [JsonProperty("about")]
        public object About { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("cover_image")]
        public string CoverImage { get; set; }

        [JsonProperty("favorites")]
        public Favorite[] Favorites { get; set; }

        [JsonProperty("karma")]
        public int Karma { get; set; }

        [JsonProperty("last_library_update")]
        public DateTime LastLibraryUpdate { get; set; }

        [JsonProperty("life_spent_on_anime")]
        public int LifeSpentOnAnime { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("online")]
        public bool Online { get; set; }

        [JsonProperty("show_adult_content")]
        public bool ShowAdultContent { get; set; }

        [JsonProperty("title_language_preference")]
        public string TitleLanguagePreference { get; set; }

        [JsonProperty("waifu")]
        public string Waifu { get; set; }

        [JsonProperty("waifu_char_id")]
        public string WaifuCharId { get; set; }

        [JsonProperty("waifu_or_husbando")]
        public string WaifuOrHusbando { get; set; }

        [JsonProperty("waifu_slug")]
        public string WaifuSlug { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        #endregion
    }
}