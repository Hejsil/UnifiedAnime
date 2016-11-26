using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedAnime.Clients.HummingBirdV1;

namespace UnifiedAnime.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HummingBirdV1Client();

            var result = client.SearchAnime("Flip Flappers");
            foreach (var anime in result.Data)
            {
                Console.WriteLine(anime.Title);
            }

            Console.ReadKey();
        }
    }
}
