﻿using MoreCollections;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Data.HummingBirdV1;

namespace UnifiedAnime.Other.JsonConverters.AniList
{
    public class TitleLanguageMapper : TypeToTypeMapper<string, TitleLanguage>
    {
        protected override Map<string, TitleLanguage> Map { get; } = new Map<string, TitleLanguage>
        {
            { "japanese", TitleLanguage.Japanese },
            { "english", TitleLanguage.English },
            { "romaji", TitleLanguage.Romaji },
        };
    }
}