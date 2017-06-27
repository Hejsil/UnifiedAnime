using UnifiedAnime.AniList.Model;
using UnifiedAnime.Collections;
using UnifiedAnime.Converters;

namespace UnifiedAnime.AniList.Converters
{
    public class GenreMapper : TypeToTypeMapper<string, Genre>
    {
        protected override Map<string, Genre> Map { get; } = new Map<string, Genre>()
        {
            { "Action", Genre.Action },
            { "Adventure", Genre.Adventure },
            { "Comedy", Genre.Comedy },
            { "Drama", Genre.Drama },
            { "Ecchi", Genre.Ecchi },
            { "Fantasy", Genre.Fantasy },
            { "Horror", Genre.Horror },
            { "Mahou Shoujo", Genre.MahouShoujo },
            { "Mecha", Genre.Mecha },
            { "Music", Genre.Music },
            { "Mystery", Genre.Mystery },
            { "Psychological", Genre.Psychological },
            { "Romance", Genre.Romance },
            { "Sci-Fi", Genre.SciFi },
            { "Slice of Life", Genre.SliceOfLife },
            { "Sports", Genre.Sports },
            { "Supernatural", Genre.Supernatural },
            { "Thriller", Genre.Thriller }
        };
    }
}
