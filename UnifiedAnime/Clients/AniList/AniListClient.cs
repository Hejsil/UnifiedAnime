using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedAnime.Data;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Clients.AniList
{
    public class AniListClient : IAnimeClient
    {
        private const string ClientBaseUrl = "https://anilist.co/api";

        public Response AddAnime(IAnimeEntry entry)
        {
            throw new NotImplementedException();
        }

        public Response AuthenticateKey(string key)
        {
            throw new NotImplementedException();
        }

        public Response<IAnimeInfo> GetAnime(int id)
        {
            throw new NotImplementedException();
        }

        public Response<IAnimeInfo[]> GetFavorite(string username)
        {
            throw new NotImplementedException();
        }

        public Response<IAnimeInfo[]> GetLibrary(string username)
        {
            throw new NotImplementedException();
        }

        public Response<IAnimeInfo[]> GetLibrary(string username, AnimeStatus animeStatus)
        {
            throw new NotImplementedException();
        }

        public Response<IFeedEntry[]> GetUserFeed(string username)
        {
            throw new NotImplementedException();
        }

        public Response<IUserInfo> GetUserInfo(string username)
        {
            throw new NotImplementedException();
        }

        public bool IsSupported(Method method)
        {
            throw new NotImplementedException();
        }

        public Response LoginEmail(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Response LoginUsername(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Response RemoveAnime(int id)
        {
            throw new NotImplementedException();
        }

        public Response RemoveAnime(IAnimeEntry entry)
        {
            throw new NotImplementedException();
        }

        public Response<IAnimeInfo[]> SearchAnime(string query)
        {
            throw new NotImplementedException();
        }

        public Response UpdateAnime(IAnimeEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}
