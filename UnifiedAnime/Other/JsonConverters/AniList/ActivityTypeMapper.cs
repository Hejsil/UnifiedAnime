using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreCollections;
using UnifiedAnime.Data.AniList;

namespace UnifiedAnime.Other.JsonConverters.AniList
{
    public class ActivityTypeMapper : TypeToTypeMapper<string, ActivityType>
    {
        protected override Map<string, ActivityType> Map { get; } = new Map<string, ActivityType>()
        {
            { "list", ActivityType.List },
            { "text", ActivityType.Text }
        };
    }
}
