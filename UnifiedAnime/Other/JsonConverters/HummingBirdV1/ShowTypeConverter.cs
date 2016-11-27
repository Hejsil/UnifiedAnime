using MoreCollections;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Other.JsonConverters.HummingBirdV1
{
    /// <summary>
    /// <remarks>
    /// Could have used StringEnumConverter, but this gives me the possibility to easily refactor
    /// all values of ShowType.
    /// </remarks>
    /// </summary>
    public class ShowTypeConverter : BaseStringToTypeConverter<ShowType>
    {
        protected override Map<string, ShowType> Map { get; } = new Map<string, ShowType>
        {
            { "TV", ShowType.TV },
            { "Movie", ShowType.Movie },
            { "OVA", ShowType.OVA },
            { "ONA", ShowType.ONA },
            { "Special", ShowType.Special },
            { "Music", ShowType.Music },
        };
    }
}