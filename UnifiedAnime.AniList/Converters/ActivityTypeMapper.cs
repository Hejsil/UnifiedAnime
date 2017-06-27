using UnifiedAnime.AniList.Model;
using UnifiedAnime.Collections;
using UnifiedAnime.Converters;

namespace UnifiedAnime.AniList.Converters
{
    public class ActivityTypeMapper : TypeToTypeMapper<string, ActivityType>
    {
        protected override Map<string, ActivityType> Map { get; } = new Map<string, ActivityType>()
        {
            { "list", ActivityType.List },
            { "text", ActivityType.Text }
        };
    }
}
