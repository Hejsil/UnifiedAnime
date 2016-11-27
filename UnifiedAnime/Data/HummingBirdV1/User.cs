using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using UnifiedAnime.Other.JsonConverters;
using UnifiedAnime.Other.JsonConverters.HummingBirdV1;

namespace UnifiedAnime.Data.HummingBirdV1
{
    /// <summary>
    /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Structures#user-object
    /// </summary>
    public class User : IUserInfo
    {
        #region Properties


        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("waifu")]
        public string Waifu { get; set; }

        [JsonProperty("waifu_or_husbando")]
        public string WaifuOrHusbando { get; set; }

        [JsonProperty("waifu_slug")]
        public string WaifuSlug { get; set; }

        [JsonProperty("waifu_char_id")]
        public string WaifuCharId { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("cover_image")]
        public string CoverImage { get; set; }

        [JsonProperty("about")]
        public string About { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("life_spent_on_anime")]
        public int LifeSpentOnAnime { get; set; }

        [JsonProperty("show_adult_content")]
        public bool ShowAdultContent { get; set; }

        [JsonProperty("title_language_preference")]
        [JsonConverter(typeof(TitleLanguageConverter))]
        public TitleLanguage PreferenceTitleLanguage { get; set; }

        [JsonProperty("last_library_update")]
        public DateTime LastLibraryUpdate { get; set; }

        [JsonProperty("online")]
        public bool Online { get; set; }

        [JsonProperty("favorites")]
        public Favorite[] Favorites { get; set; }

        #endregion
    }
}