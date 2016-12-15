using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreCollections;
using UnifiedAnime.Data.AniList;

namespace UnifiedAnime.Other.JsonConverters.AniList
{
    public class SeriesTypeMapper : TypeToTypeMapper<string, SeriesType>
    {
        protected override Map<string, SeriesType> Map { get; } = new Map<string, SeriesType>
        {
            { "anime", SeriesType.Anime },
            { "manga", SeriesType.Manga }
        };
    }
}
