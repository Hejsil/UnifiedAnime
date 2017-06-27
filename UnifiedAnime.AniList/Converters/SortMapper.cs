using UnifiedAnime.AniList.Model;
using UnifiedAnime.Collections;
using UnifiedAnime.Converters;

namespace UnifiedAnime.AniList.Converters
{
    public class SortMapper : TypeToTypeMapper<string, SortingMethod>
    {
        protected override Map<string, SortingMethod> Map { get; } = new Map<string, SortingMethod>
        {
            { "id", SortingMethod.Id },
            { "id-desc", SortingMethod.IdDescending },
            { "score", SortingMethod.Score },
            { "score-desc", SortingMethod.ScoreDescending },
            { "popularity", SortingMethod.Popularity },
            { "popularity-desc", SortingMethod.PopularityDescending },
            { "start_date", SortingMethod.StartDate },
            { "start_date-desc", SortingMethod.StartDateDescending },
            { "end_date", SortingMethod.EndDate },
            { "end_date-desc", SortingMethod.EndDateDescending },
        };
    }
}
