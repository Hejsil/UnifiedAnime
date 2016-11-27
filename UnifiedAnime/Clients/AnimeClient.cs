using System;
using UnifiedAnime.Clients.HummingBirdV1;

namespace UnifiedAnime.Clients
{
    public static class AnimeClient
    {

        #region Other Members

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
                    return new UnifiedHummingBirdV1Client();
                case ImplementedClient.HummingBirdV2:
                    break;
                case ImplementedClient.MyAnimeList:
                    break;
            }

            throw new NotImplementedException();
        }

        #endregion
    }
}