using System;
using UnifiedAnime.Clients.Bases;
using UnifiedAnime.Clients.Browsers.AniList;
using UnifiedAnime.Clients.Browsers.HummingBirdV1;

namespace UnifiedAnime.Samples.HummingBird
{
    class Program
    {

        static void Main(string[] args)
        {
            var browser = new AniListBrowser("***REMOVED***", "***REMOVED***");
            var response1 = browser.GetFavourites("RogueTofu");
        }
    }
}
