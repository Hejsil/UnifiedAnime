using MoreCollections;
using UnifiedAnime.Data.HummingBirdV1;

namespace UnifiedAnime.Other.JsonConverters.HummingBirdV1
{
    public class TitleLanguageConverter : BaseStringToTypeConverter<TitleLanguage>
    {
        protected override Map<string, TitleLanguage> Map { get; } = new Map<string, TitleLanguage>
        {
            { "canonical", TitleLanguage.Canonical },
            { "english", TitleLanguage.English },
            { "romanized", TitleLanguage.Romanized },
        };
    }
}