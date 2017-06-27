using UnifiedAnime.AniList.Model;
using UnifiedAnime.Collections;
using UnifiedAnime.Converters;

namespace UnifiedAnime.AniList.Converters
{
    public class MediaTypeMapper : TypeToTypeMapper<string, MediaType>
    {
        protected override Map<string, MediaType> Map { get; } = new Map<string, MediaType>
        {
            { "TV", MediaType.TV },
            { "TV Short", MediaType.TVShort },
            { "Movie", MediaType.Movie },
            { "Special", MediaType.Special },
            { "OVA", MediaType.OVA },
            { "ONA", MediaType.ONA },
            { "Music", MediaType.Music },
            { "Manga", MediaType.Manga },
            { "Novel", MediaType.Novel },
            { "One Shot", MediaType.OneShot },
            { "Doujin", MediaType.Doujin },
            { "Manhua", MediaType.Manhua },
            { "Manhwa", MediaType.Manhwa }
        };
    }
}
