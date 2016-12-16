using MoreCollections;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Other.JsonConverters.HummingBirdV1
{
    public class AnimeEntryStatusMapper : TypeToTypeMapper<string, AnimeEntryStatus>
    {
        protected override Map<string, AnimeEntryStatus> Map { get; } = new Map<string, AnimeEntryStatus>
        {
            { "currently-watching", AnimeEntryStatus.Watching },
            { "plan-to-watch", AnimeEntryStatus.PlanToWatch },
            { "completed", AnimeEntryStatus.Completed },
            { "on-hold", AnimeEntryStatus.OnHold },
            { "dropped", AnimeEntryStatus.Dropped }
        };
    }
}
