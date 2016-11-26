using Newtonsoft.Json;

namespace UnifiedAnime.Data.HummingBirdV1
{
    public class Anime : IAnimeInfo
    {
        #region Properties

        [JsonProperty("age_rating")]
        public string AgeRating { get; set; }

        [JsonProperty("alternate_title")]
        public string AlternateTitle { get; set; }

        [JsonProperty("community_rating")]
        public double CommunityRating { get; set; }

        [JsonProperty("episode_count")]
        public int EpisodeCount { get; set; }

        [JsonProperty("episode_length")]
        public int EpisodeLength { get; set; }

        [JsonProperty("finished_airing")]
        public string FinishedAiring { get; set; }

        [JsonProperty("genres")]
        public Genre[] Genres { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("cover_image")]
        public string ImageUrl { get; set; }

        [JsonProperty("mal_id")]
        public int MalId { get; set; }

        [JsonIgnore]
        public double Score
        {
            get { return LibraryEntryRating.ConvertToUnifiedAnimeScore(CommunityRating); }
            set { CommunityRating = LibraryEntryRating.ConvertToHummingBirdRating(value); }
        }

        [JsonProperty("show_type")]
        public string ShowType { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("started_airing")]
        public string StartedAiring { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("synopsis")]
        public string Synopsis { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        #endregion
    }
}