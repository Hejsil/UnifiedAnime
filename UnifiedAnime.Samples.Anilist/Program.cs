using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using UnifiedAnime.Clients.Browsers.AniList;
using UnifiedAnime.Samples.Anilist.Properties;

namespace UnifiedAnime.Samples.Anilist
{
    class Program
    {
        static void Main(string[] args)
        {
            var browser = new AniListBrowser();
            browser.Authenticate(Resources.AniListClientId, Resources.AniListClientSecret);
            var result = browser.GetMangalist("UnifiedAnimeTestUser");
        }
    }
}
