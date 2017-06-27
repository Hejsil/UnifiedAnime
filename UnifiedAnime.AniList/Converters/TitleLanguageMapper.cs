using UnifiedAnime.Collections;
using UnifiedAnime.Converters;
using UnifiedAnime.Model;

namespace UnifiedAnime.AniList.Converters
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