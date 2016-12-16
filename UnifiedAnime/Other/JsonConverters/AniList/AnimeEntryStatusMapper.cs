using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreCollections;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Other.JsonConverters.AniList
{
    public class AnimeEntryStatusMapper : TypeToTypeMapper<string, AnimeEntryStatus>
    {
        protected override Map<string, AnimeEntryStatus> Map { get; } = new Map<string, AnimeEntryStatus>
        {
            { "watching", AnimeEntryStatus.Watching },
            { "plan to watch", AnimeEntryStatus.PlanToWatch },
            { "completed", AnimeEntryStatus.Completed },
            { "on-hold", AnimeEntryStatus.OnHold },
            { "dropped", AnimeEntryStatus.Dropped }
        };
    }
}
