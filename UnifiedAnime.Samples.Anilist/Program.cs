using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using UnifiedAnime.Clients.Browsers.AniList;
using UnifiedAnime.Clients.Profiles.AniList;
using UnifiedAnime.Samples.Anilist.Properties;

namespace UnifiedAnime.Samples.Anilist
{
    class Program
    {
        static void Main(string[] args)
        {
            var profile = new AniListProfile(Resources.AniListClientId, Resources.AniListClientSecret);
            var response = profile.AuthenticationLink;


            //var browser = new AniListBrowser(Resources.AniListClientId, Resources.AniListClientSecret);
            //browser.Authenticate();
            //var result = browser.GetMangalist("UnifiedAnimeTestUser");
        }
    }
}
