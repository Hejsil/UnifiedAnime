using MoreCollections;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Other.JsonConverters.HummingBirdV1
{
    public class EntryStatusMapper : TypeToTypeMapper<string, EntryStatus>
    {
        protected override Map<string, EntryStatus> Map { get; } = new Map<string, EntryStatus>
        {
            { "currently-watching", EntryStatus.Watching },
            { "plan-to-watch", EntryStatus.PlanToWatch },
            { "completed", EntryStatus.Completed },
            { "on-hold", EntryStatus.OnHold },
            { "dropped", EntryStatus.Dropped }
        };
    }
}
