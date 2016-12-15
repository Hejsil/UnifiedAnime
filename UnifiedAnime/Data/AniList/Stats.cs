using Newtonsoft.Json;

namespace UnifiedAnime.Data.AniList
{
    public class Stats
    {

        [JsonProperty("status_distribution")]
        public StatusDistributions StatusDistributions { get; set; }

        [JsonProperty("score_distribution")]
        public ScoreDistribution ScoreDistributions { get; set; }

        // TODO: How do we do this? Im not sure...
        // {
        //    "Comedy": 4,
        //    "Drama": 4,
        //    ...
        //}
        //[JsonProperty("favourite_genres")]
        //public FavouriteGenres FavouriteGenres { get; set; }
    }
}