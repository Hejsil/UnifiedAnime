using MoreCollections;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Data.HummingBirdV1;

namespace UnifiedAnime.Other.JsonConverters.HummingBirdV1
{
    public class TitleLanguageMapper : TypeToTypeMapper<string, TitleLanguage>
    {
        protected override Map<string, TitleLanguage> Map { get; } = new Map<string, TitleLanguage>
        {
            { "canonical", TitleLanguage.Japanese },
            { "english", TitleLanguage.English },
            { "romanized", TitleLanguage.Romaji },
        };
    }
}