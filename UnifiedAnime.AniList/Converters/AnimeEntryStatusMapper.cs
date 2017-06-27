using UnifiedAnime.Collections;
using UnifiedAnime.Converters;
using UnifiedAnime.Model;

namespace UnifiedAnime.AniList.Converters
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
