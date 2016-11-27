using System;
using System.Linq;
using UnifiedAnime.Data;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Other;

namespace UnifiedAnime.Clients.HummingBirdV1
{
    public class UnifiedHummingBirdV1Client : HummingBirdV1Client, IAnimeClient
    {
        #region Interface Implementaions

        public Tuple<Response, IAnimeInfo> BrowseGet(int id)
        {
            var result = GetAnimeById(id);
            return new Tuple<Response, IAnimeInfo>(result.Item1, result.Item2);
        }

        public Tuple<Response, IAnimeInfo[]> BrowseUserFavorites(string username)
        {
            var result = GetUserFavoriteAmime(username);
            return new Tuple<Response, IAnimeInfo[]>(result.Item1,
                result.Item2?.Select(item => BrowseGet(item.Id).Item2).ToArray());
        }

        public Tuple<Response, IAnimeEntry[]> BrowseUserLibrary(string username)
        {
            var result = GetUserLibrary(username);
            return new Tuple<Response, IAnimeEntry[]>(result.Item1, result.Item2?.OfType<IAnimeEntry>().ToArray());
        }

        public Tuple<Response, IAnimeEntry[]> BrowseUserLibrary(string username, EntryStatus entryStatus)
        {
            var result = GetUserLibrary(username, entryStatus);
            return new Tuple<Response, IAnimeEntry[]>(result.Item1, result.Item2?.OfType<IAnimeEntry>().ToArray());
        }

        public Tuple<Response, IFeedEntry[]> BrowseUserFeed(string username)
        {
            var result = GetUserActivityFeed(username);
            return new Tuple<Response, IFeedEntry[]>(result.Item1, result.Item2?.OfType<IFeedEntry>().ToArray());
        }

        public Tuple<Response, IUserInfo> BrowseUserInfo(string username)
        {
            var result = GetUserInfomation(username);
            return new Tuple<Response, IUserInfo>(result.Item1, result.Item2);
        }

        public Tuple<Response, IAnimeInfo[]> BrowseAnime(string query)
        {
            var result = SearchForAnime(query);
            return new Tuple<Response, IAnimeInfo[]>(result.Item1, result.Item2?.OfType<IAnimeInfo>().ToArray());
        }

        public Response LibraryAdd(IAnimeEntry entry) => AddOrUpdateLibraryEntry(entry.Id).Item1;

        public Response LibraryRemove(IAnimeEntry entry) => RemoveLibraryEntry(entry.Id);

        public Response LibraryUpdate(IAnimeEntry entry) =>
            AddOrUpdateLibraryEntry(
                id: entry.Id, 
                status: entry.Status, 
                privacy: entry.Private,
                saneRatingUpdate: ScoreConverter.UnifiedToHummingBird(entry.Score),
                rewatching: entry.Rewatching,
                rewatchedTimes: entry.RewatchTimes,
                notes: entry.Notes,
                episodesWatched: entry.EpisodesWatched).Item1;

        public Response AuthenticateEmail(string email, string password)
            => Authenticate(email: email, password: password);

        public Response AuthenticateUsername(string username, string password)
            => Authenticate(username: username, password: password);

        public Response AuthenticateKey(string key) => new Response { Status = ResponseStatus.MethodNotSupported };

        public bool IsMethodSupported(AnimeClientMethod animeClientMethod)
        {
            switch (animeClientMethod)
            {
                case AnimeClientMethod.LoginUsername:
                case AnimeClientMethod.LoginEmail:
                case AnimeClientMethod.GetAnime:
                case AnimeClientMethod.GetUserInfo:
                case AnimeClientMethod.GetUserFeed:
                case AnimeClientMethod.GetFavorite:
                case AnimeClientMethod.GetLibrary:
                case AnimeClientMethod.SearchAnime:
                case AnimeClientMethod.AddAnime:
                case AnimeClientMethod.UpdateAnime:
                case AnimeClientMethod.RemoveAnime:
                    return true;
                case AnimeClientMethod.AuthenticateKey:
                default:
                    return false;
            }
        }

        #endregion
    }
}