using UnifiedAnime.Collections;
using UnifiedAnime.Converters;
using UnifiedAnime.Model;

namespace UnifiedAnime.AniList.Converters
{
    public class AnimeSourceMapper : TypeToTypeMapper<string, AnimeSource>
    {
        protected override Map<string, AnimeSource> Map { get; } = new Map<string, AnimeSource>
        {
            { "Original", AnimeSource.Original },
            { "Manga", AnimeSource.Manga },
            { "LightNovel", AnimeSource.LightNovel },
            { "VisualNovel", AnimeSource.VisualNovel },
            { "VideoGame", AnimeSource.VideoGame },
            { "Other", AnimeSource.Other }
        };
    }
}
