using UnifiedAnime.AniList.Model;
using UnifiedAnime.Collections;
using UnifiedAnime.Converters;

namespace UnifiedAnime.AniList.Converters
{
    public class SeriesTypeMapper : TypeToTypeMapper<string, SeriesType>
    {
        protected override Map<string, SeriesType> Map { get; } = new Map<string, SeriesType>
        {
            { "anime", SeriesType.Anime },
            { "manga", SeriesType.Manga }
        };
    }
}
