using Newtonsoft.Json;

namespace UnifiedAnime.Data.HummingBirdV1
{
    public class LibraryEntryRating
    {
        #region Constants

        private const int Scores = 11;
        private const int Splits = Scores + 1;
        private const double SplitSize = 100.0/Splits;

        #endregion

        #region Properties

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        #endregion

        #region Other Members

        public static double ConvertToHummingBirdRating(double value)
        {
            for (var i = 1; i < Splits; i++)
                if (((i - 1)*SplitSize <= value) && (value <= i*SplitSize))
                    return (i - 1)*0.5;

            return 0.0;
        }

        public static double ConvertToUnifiedAnimeScore(double value)
        {
            return value*20;
        }

        #endregion
    }
}