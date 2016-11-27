using MoreCollections;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Data.HummingBirdV1;

namespace UnifiedAnime.Other.JsonConverters.HummingBirdV1
{
    public class SubStoryTypeConverter : BaseStringToTypeConverter<SubStoryType>
    {
        protected override Map<string, SubStoryType> Map { get; } = new Map<string, SubStoryType>
        {
            { "followed", SubStoryType.Followed },
            { "comment", SubStoryType.Comment },
            { "watched_episode", SubStoryType.WatchedEpisode },
            { "watchlist_status_update", SubStoryType.WatchlistStatusUpdate },
        };
    }
}