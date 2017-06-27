using UnifiedAnime.Collections;
using UnifiedAnime.Converters;
using UnifiedAnime.Model;

namespace UnifiedAnime.AniList.Converters
{
    /// <summary>
    /// Because most anime sites will probably have some reprensentation of a season,
    /// we can't just hardcode <see cref="Season"/> to be the numbers of AniList
    /// </summary>
    public class IntSeasonMapper : TypeToTypeMapper<int, Season>
    {
        protected override Map<int, Season> Map { get; } = new Map<int, Season>
        {
            // TODO: Ensure the order is correct
            { 1, Season.Winter },
            { 2, Season.Spring },
            { 3, Season.Summer },
            { 4, Season.Fall },
        };
    }
}
