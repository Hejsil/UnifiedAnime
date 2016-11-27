using System;
using Newtonsoft.Json;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Other;
using UnifiedAnime.Other.JsonConverters;
using UnifiedAnime.Other.JsonConverters.HummingBirdV1;

namespace UnifiedAnime.Data.HummingBirdV1
{
    /// <summary>
    /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Structures#anime-object
    /// </summary>
    public class Anime : IAnimeInfo
    {
        #region Properties


        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("mal_id")]
        public int MalId { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(AnimeStatusMapper))]
        public AnimeStatus Status { get; set; }

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
        public DateTime StartedAiring { get; set; }

        [JsonProperty("finished_airing")]
        public DateTime FinishedAiring { get; set; }


        [JsonProperty("age_rating")]
        public string AgeRating { get; set; }

        [JsonProperty("community_rating")]
        public double CommunityRating { get; set; }

        [JsonProperty("genres")]
        public Genre[] Genres { get; set; }

        [JsonIgnore]
        public double Score
        {
            get { return ScoreConverter.HummingBirdToUnified(CommunityRating); }
            set { CommunityRating = ScoreConverter.UnifiedToHummingBird(value); }
        }

        #endregion
    }
}