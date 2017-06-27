using UnifiedAnime.Collections;
using UnifiedAnime.Converters;
using UnifiedAnime.Model;

namespace UnifiedAnime.AniList.Converters
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
