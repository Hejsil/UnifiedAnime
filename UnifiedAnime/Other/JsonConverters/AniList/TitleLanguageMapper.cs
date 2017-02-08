using MoreCollections;
using UnifiedAnime.Data.Common;

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