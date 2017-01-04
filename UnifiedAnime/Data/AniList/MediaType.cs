using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedAnime.Data.AniList
{
    /// <summary>
    /// https://anilist-api.readthedocs.io/en/latest/series.html#media-types
    /// </summary>
    public enum MediaType
    {
        TV = 0,
        TVShort = 1,
        Movie = 2,
        Special = 3,
        OVA = 4,
        ONA = 5,
        Music = 6,
        Manga = 7,
        Novel = 8,
        OneShot = 9,
        Doujin = 10,
        Manhua = 11,
        Manhwa = 12
    }
}
