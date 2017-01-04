using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreCollections;
using UnifiedAnime.Data.AniList;

namespace UnifiedAnime.Other.JsonConverters.AniList
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
