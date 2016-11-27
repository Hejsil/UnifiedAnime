using System;
using UnifiedAnime.Clients;
using UnifiedAnime.Clients.Profiles.HummingBirdV1;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Samples.SimpleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IAnimeProfile profile;
            var username = args[0];
            var password = args[1];
            var site = args[2];

            switch (site)
            {
                case "HummingBird":
                    profile = new UnifiedHummingBirdV1Profile();
                    break;
                case "AniList":
                    profile = new UnifiedAniListProfile();
                    break;
                default:
                    Console.WriteLine($"Site not supported: {site}");
                    return;
            }

            profile.Authenticate(username, password);
            var response = profile.Get();

            if (response.Status == ResponseStatus.Success)
            {
                var animeEntries = response.Data;

                foreach (var entry in animeEntries)
                {
                    Console.WriteLine(entry.Info.Title);
                }
            }

            Console.ReadKey();
        }
    }
}
