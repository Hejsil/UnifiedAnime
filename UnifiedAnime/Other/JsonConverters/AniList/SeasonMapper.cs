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
