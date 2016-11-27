using MoreCollections;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Data.HummingBirdV1;

namespace UnifiedAnime.Other.JsonConverters.HummingBirdV1
{
    public class StoryTypeMapper : TypeToTypeMapper<string, StoryType>
    {
        protected override Map<string, StoryType> Map { get; } = new Map<string, StoryType>
        {
            { "comment", StoryType.Comment },
            { "media_story", StoryType.MediaStory },
            { "followed", StoryType.Followed },
        };
    }
}