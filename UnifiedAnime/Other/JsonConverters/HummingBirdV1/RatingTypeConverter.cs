using MoreCollections;
using UnifiedAnime.Data.HummingBirdV1;

namespace UnifiedAnime.Other.JsonConverters.HummingBirdV1
{
    public class RatingTypeConverter : BaseStringToTypeConverter<RatingType>
    {
        protected override Map<string, RatingType> Map { get; } = new Map<string, RatingType>
        {
            { "simple", RatingType.Simple },
            { "advanced", RatingType.Advanced },
        };
    }
}