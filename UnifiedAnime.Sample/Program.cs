using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedAnime.AniList;

namespace UnifiedAnime.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var browser = new AniListBrowser();
            var anime = browser.GetAnime(21196);
        }
    }
}
