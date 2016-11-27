using System;
using UnifiedAnime.Clients.HummingBirdV1;

namespace UnifiedAnime.Samples.HummingBird
{
    class Program
    {

        static void Main(string[] args)
        {
            var hbClient = new HummingBirdV1Client();
            var animes = hbClient.GetUserActivityFeed("Hejsil");
        }
    }
}
