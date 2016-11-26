using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using UnifiedAnime.Data;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Data.HummingBirdV1;
using UnifiedAnime.Json;
using ResponseStatus = UnifiedAnime.Data.Common.ResponseStatus;

namespace UnifiedAnime.Clients.HummingBirdV1
{
    /// <summary>
    /// A client for communicating with https://hummingbird.me.
    /// This client uses HummingBird's API V1, which is about to be replaced.
    /// HummingBirdClientV2 will be implemented when API V2 
    /// </summary>
    public class HummingBirdV1Client : IAnimeClient
    {
        private const string ClientBaseUrl = "https://hummingbird.me/api/v1";

        private string _token = "";

        /// <summary>
        /// Login to HummingBird using a valid username and password
        /// </summary>
        /// <param name="username">The username of the profile you want to login to.</param>
        /// <param name="password">The password of the profile you want to login to.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        public Response LoginUsername(string username, string password)
        {
            var request = new RestRequest($"/users/authenticate");
            request.AddParameter("username", username);
            request.AddParameter("password", password);

            var client = new RestClient(ClientBaseUrl);
            var response = client.Post(request);

            _token = response.Content;

            return new Response { Status = ResponseStatus.Success, RestResponse = response };
        }

        /// <summary>
        /// Login to HummingBird using a valid email and password
        /// </summary>
        /// <param name="email">The email of the profile you want to login to.</param>
        /// <param name="password">The password of the profile you want to login to.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        public Response LoginEmail(string email, string password)
        {
            var request = new RestRequest($"/users/authenticate");
            request.AddParameter("email", email);
            request.AddParameter("password", password);

            var client = new RestClient(ClientBaseUrl);
            var response = client.Post(request);

            _token = response.Content;

            return new Response { Status = ResponseStatus.Success, RestResponse = response };
        }

        /// <summary>
        /// Not supported by HummingBirdV1Client.
        /// </summary>
        public Response AuthenticateKey(string key) => new Response { Status = ResponseStatus.NotSupportMethod };

        /// <summary>
        /// Get information about an anime with a certain id.
        /// </summary>
        /// <param name="id">The id of the anime.</param>
        /// <returns>A response containing the anime infomation and relevant information for error handeling.</returns>
        public Response<IAnimeInfo> GetAnime(int id)
        {
            var request = new RestRequest($"/anime/{id}");
            var client = new RestClient(ClientBaseUrl);
            var response = client.Get<Anime>(request);

            return new Response<IAnimeInfo> { Status = ResponseStatus.Success, Data = response.Data, RestResponse = response };
        }

        /// <summary>
        /// Get information on a user.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A response containing the user data and relevant information for error handeling.</returns>
        public Response<IUserInfo> GetUserInfo(string username)
        {
            var request = new RestRequest($"/users/{username}");
            var client = new RestClient(ClientBaseUrl);
            var response = client.Get<User>(request);

            return new Response<IUserInfo> { Status = ResponseStatus.Success, Data = response.Data, RestResponse = response };
        }
        
        /// <summary>
        /// Get a users feed.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A response containing an array of feed entries and relevant information for error handeling.</returns>
        public Response<IFeedEntry[]> GetUserFeed(string username)
        {
            var request = new RestRequest($"/users/{username}/feed");
            var client = new RestClient(ClientBaseUrl);
            var response = client.Get(request);
            var result = JsonConvert.DeserializeObject<IFeedEntry[]>(response.Content, new InterfaceInstatiator<Story, IFeedEntry>());

            return new Response<IFeedEntry[]> { Status = ResponseStatus.Success, Data = result, RestResponse = response };
        }

        /// <summary>
        /// Get a users favorite anime.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A response containing an array of anime information and relevant information for error handeling.</returns>
        public Response<IAnimeInfo[]> GetFavorite(string username)
        {
            var request = new RestRequest($"/users/{username}/favorite_anime");
            var client = new RestClient(ClientBaseUrl);
            var response = client.Get<List<Favorite>>(request);
            var result = response.Data?.Select(item => GetAnime(item.ItemId).Data).ToArray();

            return new Response<IAnimeInfo[]> { Status = ResponseStatus.Success, Data = result, RestResponse = response };
        }
    
        /// <summary>
        /// Get a users anime library.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A response containing an array of anime information and relevant information for error handeling.</returns>
        public Response<IAnimeInfo[]> GetLibrary(string username)
        {
            var request = new RestRequest($"/users/{username}/library");
            var client = new RestClient(ClientBaseUrl);
            var response = client.Get(request);
            var result = JsonConvert.DeserializeObject<IAnimeInfo[]>(response.Content, new InterfaceInstatiator<Anime, IAnimeInfo>());

            return new Response<IAnimeInfo[]> { Status = ResponseStatus.Success, Data = result, RestResponse = response };
        }

        /// <summary>
        /// Get all animes in a users library, which has a specific status.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="animeStatus">The status of the animes to get.</param>
        /// <returns>A response containing an array of anime information and relevant information for error handeling.</returns>
        public Response<IAnimeInfo[]> GetLibrary(string username, AnimeStatus animeStatus)
        {
            var request = new RestRequest($"/users/{username}/library");

            var status = LibraryEntry.ConvertToHummingBirdStatus(animeStatus);
            if (!string.IsNullOrEmpty(status))
                request.AddParameter("status", status);

            var client = new RestClient(ClientBaseUrl);
            var response = client.Get(request);
            var result = JsonConvert.DeserializeObject<IAnimeInfo[]>(response.Content, new InterfaceInstatiator<Anime, IAnimeInfo>());

            return new Response<IAnimeInfo[]> { Status = ResponseStatus.Success, Data = result, RestResponse = response };
        }

        /// <summary>
        /// Search on HummingBird for specific anime.
        /// <remarks>
        /// Supports fuzzy search.
        /// </remarks>
        /// </summary>
        /// <param name="query">The query string used to get the search results.</param>
        /// <returns>A response containing an array of anime information and relevant information for error handeling.</returns>
        public Response<IAnimeInfo[]> SearchAnime(string query)
        {
            var request = new RestRequest($"/search/anime");
            request.AddParameter("query", query);

            var client = new RestClient(ClientBaseUrl);
            var response = client.Get(request);
            var result = JsonConvert.DeserializeObject<IAnimeInfo[]>(response.Content, new InterfaceInstatiator<Anime, IAnimeInfo>());

            return new Response<IAnimeInfo[]> { Status = ResponseStatus.Success, Data = result, RestResponse = response };
        }

        /// <summary>
        /// Add or Update an anime entry from the logged in users library.
        /// <remarks>
        /// Requires that the client is logged in to a HummingBird user.
        /// </remarks>
        /// </summary>
        /// <param name="entry">The entry and information to update.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        public Response AddAnime(IAnimeEntry entry) => AddUpdateAnime(entry);

        /// <summary>
        /// Add/Update an anime entry to/from the logged in users library..
        /// <remarks>
        /// Requires that the client is logged in to a HummingBird user.
        /// </remarks>
        /// </summary>
        /// <param name="entry">The entry and information to update.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        public Response UpdateAnime(IAnimeEntry entry) => AddUpdateAnime(entry);

        private Response AddUpdateAnime(IAnimeEntry entry)
        {
            var request = new RestRequest($"/libraries/{entry.Id}");
            request.AddParameter("auth_token", _token);

            var status = LibraryEntry.ConvertToHummingBirdStatus(entry.Status);
            if (!string.IsNullOrEmpty(status))
                request.AddParameter("status", status);

            request.AddParameter("privacy", entry.Private ? "private" : "public");
            request.AddParameter("sane_rating_update", LibraryEntryRating.ConvertToHummingBirdRating(entry.Score));
            request.AddParameter("rewatching", entry.Rewatching);
            request.AddParameter("rewatched_times", entry.RewatchTimes);
            request.AddParameter("notes", entry.Notes);
            request.AddParameter("episodes_watched", entry.EpisodesWatched);

            var client = new RestClient(ClientBaseUrl);
            var response = client.Post<List<Anime>>(request);

            return new Response { Status = ResponseStatus.Success, RestResponse = response };
        }

        /// <summary>
        /// Remove an anime entry from the logged in users library.
        /// <remarks>
        /// Requires that the client is logged in to a HummingBird user.
        /// </remarks>
        /// </summary>
        /// <param name="id">The id of the entry to remove.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        public Response RemoveAnime(int id)
        {
            var request = new RestRequest($"/libraries/{id}/remove");
            request.AddParameter("auth_token", _token);
            var client = new RestClient(ClientBaseUrl);
            var response = client.Post(request);

            return new Response { Status = ResponseStatus.Success, RestResponse = response };
        }

        /// <summary>
        /// Remove an anime entry from the logged in users library.
        /// <remarks>
        /// Requires that the client is logged in to a HummingBird user.
        /// </remarks>
        /// </summary>
        /// <param name="entry">The the entry to remove.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        public Response RemoveAnime(IAnimeEntry entry) => RemoveAnime(entry.Id);

        /// <summary>
        /// Returns whether a method is supported by this client.
        /// <remarks>
        /// Supported methods:
        /// - LoginUsername
        /// - LoginEmail
        /// - GetAnime
        /// - GetUserInfo
        /// - GetUserFeed
        /// - GetFavorite
        /// - GetLibrary
        /// - SearchAnime
        /// - AddAnime
        /// - UpdateAnime
        /// - RemoveAnime
        /// </remarks>
        /// </summary>
        /// <param name="method">The method that needs to be checked.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        public bool IsSupported(Method method)
        {
            switch (method)
            {
                case Method.LoginUsername:
                case Method.LoginEmail:
                case Method.GetAnime:
                case Method.GetUserInfo:
                case Method.GetUserFeed:
                case Method.GetFavorite:
                case Method.GetLibrary:
                case Method.SearchAnime:
                case Method.AddAnime:
                case Method.UpdateAnime:
                case Method.RemoveAnime:
                    return true;
                case Method.AuthenticateKey:
                    return false;
            }

            return false;
        }
    }
}
