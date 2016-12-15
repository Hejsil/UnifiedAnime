using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreCollections;
using Newtonsoft.Json;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Other.JsonConverters.AniList
{
    /// <summary>
    /// Because most anime sites will probably have some reprensentation of a season,
    /// we can't just hardcode <see cref="Season"/> to be the numbers of AniList
    /// </summary>
    public class SeasonMapper : TypeToTypeMapper<int, Season>
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
