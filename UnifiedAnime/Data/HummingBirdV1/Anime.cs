using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnifiedAnime.Data.HummingBirdV1
{
    public class Anime : IAnimeInfo
    {
        public double Rating
        {
            get { return LibraryEntryRating.ConvertToUnifiedAnimeScore(CommunityRating); }
            set { CommunityRating = LibraryEntryRating.ConvertToHummingBirdRating(value); }
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("mal_id")]
        public int MalId { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("alternate_title")]
        public string AlternateTitle { get; set; }

        [JsonProperty("episode_count")]
        public int EpisodeCount { get; set; }

        [JsonProperty("episode_length")]
        public int EpisodeLength { get; set; }

        [JsonProperty("cover_image")]
        public string ImageUrl { get; set; }

        [JsonProperty("synopsis")]
        public string Synopsis { get; set; }

        [JsonProperty("show_type")]
        public string ShowType { get; set; }

        [JsonProperty("started_airing")]
        public string StartedAiring { get; set; }

        [JsonProperty("finished_airing")]
        public string FinishedAiring { get; set; }

        [JsonProperty("community_rating")]
        public double CommunityRating { get; set; }

        [JsonProperty("age_rating")]
        public string AgeRating { get; set; }

        [JsonProperty("genres")]
        public IList<Genre> Genres { get; set; }
    }
}
