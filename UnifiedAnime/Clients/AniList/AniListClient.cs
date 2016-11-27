using System;
using UnifiedAnime.Data;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Clients.AniList
{
    public class AniListClient : IAnimeClient
    {
        private const string ClientBaseUrl = "https://anilist.co/api";

        public Response LibraryAdd(IAnimeEntry entry)
        {
            throw new NotImplementedException();
        }

        public Response AuthenticateKey(string key)
        {
            throw new NotImplementedException();
        }

        public Tuple<Response, IAnimeInfo> BrowseGet(int id)
        {
            throw new NotImplementedException();
        }

        public Tuple<Response, IAnimeInfo[]> BrowseUserFavorites(string username)
        {
            throw new NotImplementedException();
        }

        public Tuple<Response, IAnimeEntry[]> BrowseUserLibrary(string username)
        {
            throw new NotImplementedException();
        }

        public Tuple<Response, IAnimeEntry[]> BrowseUserLibrary(string username, EntryStatus entryStatus)
        {
            throw new NotImplementedException();
        }

        public Tuple<Response, IFeedEntry[]> BrowseUserFeed(string username)
        {
            throw new NotImplementedException();
        }

        public Tuple<Response, IUserInfo> BrowseUserInfo(string username)
        {
            throw new NotImplementedException();
        }

        public bool IsMethodSupported(AnimeClientMethod animeClientMethod)
        {
            throw new NotImplementedException();
        }

        public Response AuthenticateEmail(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Response AuthenticateUsername(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Response UnifiedRemoveEntry(int id)
        {
            throw new NotImplementedException();
        }

        public Response LibraryRemove(IAnimeEntry entry)
        {
            throw new NotImplementedException();
        }

        public Tuple<Response, IAnimeInfo[]> BrowseAnime(string query)
        {
            throw new NotImplementedException();
        }

        public Response LibraryUpdate(IAnimeEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}
