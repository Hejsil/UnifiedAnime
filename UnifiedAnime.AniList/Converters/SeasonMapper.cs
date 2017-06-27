using UnifiedAnime.Collections;
using UnifiedAnime.Converters;
using UnifiedAnime.Model;

namespace UnifiedAnime.AniList.Converters
{
    public class SeasonMapper : TypeToTypeMapper<string, Season>
    {
        protected override Map<string, Season> Map { get; } = new Map<string, Season>
        {
            { "winter", Season.Winter },
            { "spring", Season.Spring },
            { "summer", Season.Summer },
            { "fall", Season.Fall },
        };
    }
}
