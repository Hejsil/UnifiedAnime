using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedAnime.Clients;
using UnifiedAnime.Clients.AniList;
using UnifiedAnime.Clients.HummingBirdV1;
using UnifiedAnime.Clients.MyAnimeList;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Samples.SimpleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IAnimeClient client;
            var username = args[0];
            var site = args[1];

            switch (site)
            {
                case "HummingBird":
                    client = new UnifiedHummingBirdV1Client();
                    break;
                case "MyAnimeList":
                    client = new UnifiedMyAnimeListClient();
                    break;
                case "AniList":
                    client = new UnifiedAniListClient();
                    break;
                default:
                    Console.WriteLine($"Site not supported: {site}");
                    return;
            }
            
            var response = client.BrowseUserLibrary(username);

            if (response.Item1.Status == ResponseStatus.Success)
            {
                var animeEntries = response.Item2;

                foreach (var entry in animeEntries)
                {
                    Console.WriteLine(entry.Info.Title);
                }
            }

            Console.ReadKey();
        }
    }
}
