using MoreCollections;
using UnifiedAnime.Data.HummingBirdV1;

namespace UnifiedAnime.Other.JsonConverters.HummingBirdV1
{
    public class ItemTypeMapper : TypeToTypeMapper<string, ItemType>
    {
        protected override Map<string, ItemType> Map { get; } = new Map<string, ItemType>
        {
            { "Anime", ItemType.Anime },
        };
    }
}