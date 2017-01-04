using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreCollections;
using Newtonsoft.Json;
using UnifiedAnime.Data.AniList;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Other.JsonConverters.AniList
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
