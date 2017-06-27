using Newtonsoft.Json;

namespace UnifiedAnime.AniList.Model
{
    public class ScoreDistribution
    {
        [JsonProperty("10")]
        public int Score10 { get; set; }

        [JsonProperty("20")]
        public int Score20 { get; set; }

        [JsonProperty("30")]
        public int Score30 { get; set; }

        [JsonProperty("40")]
        public int Score40 { get; set; }

        [JsonProperty("50")]
        public int Score50 { get; set; }

        [JsonProperty("60")]
        public int Score60 { get; set; }

        [JsonProperty("70")]
        public int Score70 { get; set; }

        [JsonProperty("80")]
        public int Score80 { get; set; }

        [JsonProperty("90")]
        public int Score90 { get; set; }

        [JsonProperty("100")]
        public int Score100 { get; set; }
    }
}