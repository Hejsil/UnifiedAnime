using System;
using UnifiedAnime.Clients;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Samples.SimpleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var username = args[0];
            var password = args[1];
            var site = args[2];

            switch (site)
            {
                default:
                    Console.WriteLine($"Site not supported: {site}");
                    return;
            }
            
        }
    }
}
