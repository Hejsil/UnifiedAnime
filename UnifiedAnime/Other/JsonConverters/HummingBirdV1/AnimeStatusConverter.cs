using MoreCollections;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Data.HummingBirdV1;

namespace UnifiedAnime.Other.JsonConverters.HummingBirdV1
{
    public class AnimeStatusConverter : BaseStringToTypeConverter<AnimeStatus>
    {
        protected override Map<string, AnimeStatus> Map { get; } = new Map<string, AnimeStatus>
        {
            { "Not Yet Aired", AnimeStatus.NotYetAired },
            { "Currently Airing", AnimeStatus.CurrentlyAiring },
            { "Finished Airing", AnimeStatus.FinishedAiring },
        };
    }
}