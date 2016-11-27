using System;
using RestSharp;
using UnifiedAnime.Clients.Bases;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Data.HummingBirdV1;
using UnifiedAnime.Other;
using UnifiedAnime.Other.JsonConverters.HummingBirdV1;

namespace UnifiedAnime.Clients.HummingBirdV1
{
    /// <summary>
    /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Methods
    /// </summary>
    public class HummingBirdV1Client : RestBasedAnimeClient
    {
        #region Constants

        public override string Url => "https://hummingbird.me/api/v1";

        #endregion

        #region Fields

        protected string Token { get; set; }

        #endregion

        /// <summary>
        /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Methods#anime--get-metadata-by-id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Tuple<Response, Anime> GetAnimeById(int id)
        {
            var request = MakeRequest($"/anime/{id}", Method.GET);
            var result = Execute<Anime>(request);

            return new Tuple<Response, Anime>(new Response(result.Item1), result.Item2);
        }

        /// <summary>
        /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Methods#anime--search-by-title
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Tuple<Response, Anime[]> SearchForAnime(string query)
        {
            var request = MakeRequest($"/search/anime", Method.GET);
            request.AddParameter("query", query);

            var result = Execute<Anime[]>(request);

            return new Tuple<Response, Anime[]>(new Response(result.Item1), result.Item2);
        }

        /// <summary>
        /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Methods#library--get-all-entries
        /// </summary>
        /// <param name="username"></param>
        /// <param name="animeStatus"></param>
        /// <returns></returns>
        public Tuple<Response, LibraryEntry[]> GetUserLibrary(string username, EntryStatus? animeStatus = null)
        {
            var request = MakeRequest($"/users/{username}/library", Method.GET);

            if (animeStatus != null)
            {
                var result = new EntryStatusConverter().TypeToString((EntryStatus)animeStatus);
                request.AddParameter("status", result);
            }

            var response = Execute<LibraryEntry[]>(request);

            return new Tuple<Response, LibraryEntry[]>(new Response(response.Item1), response.Item2);
        }

        /// <summary>
        /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Methods#library--addupdate-entry
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="privacy"></param>
        /// <param name="rating"></param>
        /// <param name="saneRatingUpdate"></param>
        /// <param name="rewatching"></param>
        /// <param name="rewatchedTimes"></param>
        /// <param name="notes"></param>
        /// <param name="episodesWatched"></param>
        /// <param name="incrementEpisodes"></param>
        /// <returns></returns>
        public Tuple<Response, LibraryEntry> AddOrUpdateLibraryEntry(
                int id, // required
                EntryStatus? status = null, // optional
                bool? privacy = null, // optional
                double? rating = null, // optional
                double? saneRatingUpdate = null, // optional
                bool? rewatching = null, // optional
                int? rewatchedTimes = null, // optional
                string notes = null, // optional
                int? episodesWatched = null, // optional
                bool? incrementEpisodes = null // optional
            )
        {
            var request = MakeRequest($"/libraries/{id}", Method.POST);
            request.AddParameter("auth_token", Token);

            if (status != null)
            {
                var result = new EntryStatusConverter().TypeToString((EntryStatus)status);
                request.AddParameter("status", result);
            }

            if (privacy != null)
                request.AddParameter("privacy", (bool)privacy ? "private" : "public");

            if (rating != null)
                request.AddParameter("rating", rating);

            if (saneRatingUpdate != null)
                request.AddParameter("sane_rating_update", saneRatingUpdate);

            if (rewatching != null)
                request.AddParameter("rewatching", rewatching);
            
            if (rewatchedTimes != null)
                request.AddParameter("rewatched_times", rewatchedTimes);

            if (rewatchedTimes != null)
                request.AddParameter("rewatched_times", rewatchedTimes);

            if (notes != null)
                request.AddParameter("notes", notes);

            if (episodesWatched != null)
                request.AddParameter("episodes_watched", episodesWatched);

            if (incrementEpisodes != null)
                request.AddParameter("increment_episodes", incrementEpisodes);

            var response = Execute<LibraryEntry>(request);

            return new Tuple<Response, LibraryEntry>(new Response(response.Item1), response.Item2);
        }

        /// <summary>
        /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Methods#library--remove-entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Response RemoveLibraryEntry(int id)
        {
            var request = MakeRequest($"/libraries/{id}/remove", Method.POST);
            request.AddParameter("auth_token", Token);

            var restResponse = Execute(request);

            return new Response(restResponse);
        }

        /// <summary>
        /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Methods#user--authenticate
        /// </summary>
        /// <param name="password"></param>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public Response Authenticate(
                string password, // required
                string username = null, // optional
                string email = null // optional
            )
        {
            var request = MakeRequest("/users/authenticate", Method.POST);

            if (string.IsNullOrEmpty(username))
                request.AddParameter("username", username);

            if (string.IsNullOrEmpty(username))
                request.AddParameter("email", email);

            request.AddParameter("password", password);

            var response = Execute(request);

            Token = response.Content;

            return new Response(response);
        }

        /// <summary>
        /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Methods#user--get-information
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Tuple<Response, User> GetUserInfomation(string username)
        {
            var request = MakeRequest($"/users/{username}", Method.GET);
            var restResponse = Execute<User>(request);

            return new Tuple<Response, User>(new Response(restResponse.Item1), restResponse.Item2);
        }

        /// <summary>
        /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Methods#user--get-activity-feed
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Tuple<Response, Story[]> GetUserActivityFeed(string username)
        {
            var request = MakeRequest($"/users/{username}/feed", Method.GET);
            var restResponse = Execute<Story[]>(request);

            return new Tuple<Response, Story[]>(new Response(restResponse.Item1), restResponse.Item2);
        }

        /// <summary>
        /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Methods#user--get-favorite-anime
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Tuple<Response, Favorite[]> GetUserFavoriteAmime(string username)
        {
            var request = MakeRequest($"/users/{username}/favorite_anime", Method.GET);
            var restResponse = Execute<Favorite[]>(request);

            return new Tuple<Response, Favorite[]>(new Response(restResponse.Item1), restResponse.Item2);
        }
    }
}