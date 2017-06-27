using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
{
    public class Review : AniListObject
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }

        [JsonProperty("rating_amount")]
        public int RatingAmount { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("private")]
        public int Private { get; set; }

        [JsonProperty("user_rating")]
        public int UserRating { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("user")]
        public SmallUser User { get; set; }
    }
}