using RestSharp;
using UnifiedAnime.Clients.Bases;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Data.HummingBirdV1;
using UnifiedAnime.Other.JsonConverters.HummingBirdV1;

namespace UnifiedAnime.Clients.Browsers.HummingBirdV1
{
    /// <summary>
    /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Methods
    /// </summary>
    public class HummingBirdV1Browser : RestBasedAnimeClient
    {
        #region Constants

        public override string Url => "https://hummingbird.me/api/v1";

        #endregion

        /// <summary>
        /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Methods#anime--get-metadata-by-id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Response<Anime> GetAnime(int id) => MakeAndExecute<Anime>($"/anime/{id}", Method.GET);

        /// <summary>
        /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Methods#anime--search-by-title
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Response<Anime[]> GetSearchAnime(string query)
        {
            var request = MakeRequest($"/search/anime", Method.GET);
            request.AddParameter("query", query);

            return Execute<Anime[]>(request);
        }

        /// <summary>
        /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Methods#library--get-all-entries
        /// </summary>
        /// <param name="username"></param>
        /// <param name="animeStatus"></param>
        /// <returns></returns>
        public Response<LibraryEntry[]> GetLibrary(string username, EntryStatus? animeStatus = null)
        {
            var request = MakeRequest($"/users/{username}/library", Method.GET);

            if (animeStatus != null)
            {
                var result = new EntryStatusMapper().Type2ToType1((EntryStatus)animeStatus);
                request.AddParameter("status", result);
            }

            return Execute<LibraryEntry[]>(request);
        }

        /// <summary>
        /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Methods#user--get-information
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Response<User> GetUser(string username) => MakeAndExecute<User>($"/users/{username}", Method.GET);

        /// <summary>
        /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Methods#user--get-activity-feed
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Response<Story[]> GetUserFeed(string username)
            => MakeAndExecute<Story[]>($"/users/{username}/feed", Method.GET);

        /// <summary>
        /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Methods#user--get-favorite-anime
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Response<Favorite[]> GetUserFavoriteAmime(string username)
            => MakeAndExecute<Favorite[]>($"/users/{username}/favorite_anime", Method.GET);
    }
}