using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnifiedAnime.Data.HummingBirdV1
{
    public class User : IUserInfo
    {
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
        public object About { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("karma")]
        public int Karma { get; set; }

        [JsonProperty("life_spent_on_anime")]
        public int LifeSpentOnAnime { get; set; }

        [JsonProperty("show_adult_content")]
        public bool ShowAdultContent { get; set; }

        [JsonProperty("title_language_preference")]
        public string TitleLanguagePreference { get; set; }

        [JsonProperty("last_library_update")]
        public DateTime LastLibraryUpdate { get; set; }

        [JsonProperty("online")]
        public bool Online { get; set; }

        [JsonProperty("favorites")]
        public IList<Favorite> Favorites { get; set; }
    }

}
