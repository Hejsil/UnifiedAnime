using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedAnime.Clients.HummingBirdV1;

namespace UnifiedAnime.Clients
{
    public static class Client
    {
        public static IAnimeClient Make(ImplementedClient client)
        {
            switch (client)
            {
                case ImplementedClient.AniDB:
                    break;
                case ImplementedClient.AniList:
                    break;
                case ImplementedClient.AnimePlanet:
                    break;
                case ImplementedClient.HummingBirdV1:
                    return new HummingBirdV1Client();
                case ImplementedClient.HummingBirdV2:
                    break;
                case ImplementedClient.MyAnimeList:
                    break;
            }

            throw new NotImplementedException();
        }
    }
}
