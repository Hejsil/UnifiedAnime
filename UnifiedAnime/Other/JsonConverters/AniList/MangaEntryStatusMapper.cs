using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreCollections;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Other.JsonConverters.AniList
{
    public class MangaEntryStatusMapper : TypeToTypeMapper<string, MangaEntryStatus>
    {
        protected override Map<string, MangaEntryStatus> Map { get; } = new Map<string, MangaEntryStatus>
        {
            { "reading", MangaEntryStatus.Reading},
            { "plan to read", MangaEntryStatus.PlanToRead },
            { "on-hold", MangaEntryStatus.OnHold },
            { "completed", MangaEntryStatus.Completed },
            { "dropped", MangaEntryStatus.Dropped }
        };
    }
}
