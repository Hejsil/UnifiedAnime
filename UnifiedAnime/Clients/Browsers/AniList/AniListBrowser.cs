using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using RestSharp;
using UnifiedAnime.Clients.Bases;
using UnifiedAnime.Data.AniList;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Other.JsonConverters.AniList;

namespace UnifiedAnime.Clients.Browsers.AniList
{
    /// <summary>
    /// A browser for the site https://anilist.co/.
    /// This browser can access everything from the site that is accessible without user authentication.
    /// For a full list, take a look at the official AniList API: https://anilist-api.readthedocs.io/en/latest/index.html
    /// </summary>
    public class AniListBrowser : RestBasedAnimeClient
    {
        /// <summary>
        /// The url used to access the API.
        /// </summary>
        public override string Url => "https://anilist.co/api/";

        private readonly string _clientId;
        private readonly string _clientSecret;
        private Timer _clientCredentialsRefresher;
        private Credentials _credentials;

        /// <summary>
        /// Initialize a new instance of <see cref="AniListBrowser"/> with a client id and secret.
        /// The id and secret can be optained by creating a client: https://anilist.co/settings/developer.
        /// <remarks>
        /// Do not share your Client Secret.
        /// </remarks>
        /// </summary>
        /// <param name="clientId">The Client ID</param>
        /// <param name="clientSecret">The Client Secret</param>
        public AniListBrowser(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        /// <summary>
        /// Authorize the <see cref="AniListBrowser"/>. This will give access to all the methods this class provides.
        /// The <see cref="AniListBrowser"/> automaticly re-authorize when needed, so this method should only ever be called
        /// once, unless it fails.
        /// </summary>
        /// <returns>A <see cref="Response"/> containing data for error handling.</returns>
        public Response Authorize()
        {
            var response = GrantClientCredentials();
            
            if (response.Status == UnifiedStatus.Success)
            {
                _credentials = response.Data;
                StartTimer();
            }

            return response;
        }

        /// <summary>
        /// Get a <see cref="User"/> by its id on AniList.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public Response<User> GetUser(int id) => GetUser(id.ToString());

        /// <summary>
        /// Get a <see cref="User"/> by its display name on AniList.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public Response<User> GetUser(string displayName) => MakeAndExecute<User>($"user/{displayName}", Method.GET);

        /// <summary>
        /// Get all activities a user has on a page.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <param name="pageNumber">The page that should be returned.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public Response<Activity[]> GetActivity(int id, int pageNumber = 0) => GetActivity(id.ToString(), pageNumber);

        /// <summary>
        /// Get all activities a user has on a page.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <param name="pageNumber">The page that should be returned.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public Response<Activity[]> GetActivity(string displayName, int pageNumber = 0) => 
            MakeAndExecute<Activity[]>($"user/{displayName}/activity", Method.GET, "page", pageNumber);
        
        /// <summary>
        /// Get all the users that follow a user.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public Response<SmallUser[]> GetFollowers(int id) => GetFollowers(id.ToString());

        /// <summary>
        /// Get all the users that follow a user.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public Response<SmallUser[]> GetFollowers(string displayName) => MakeAndExecute<SmallUser[]>($"user/{displayName}/followers", Method.GET);

        /// <summary>
        /// Get all the users a user is following.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public Response<SmallUser[]> GetFollowing(int id) => GetFollowing(id.ToString());

        /// <summary>
        /// Get all the users a user is following.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public Response<SmallUser[]> GetFollowing(string displayName) => MakeAndExecute<SmallUser[]>($"user/{displayName}/following", Method.GET);

        /// <summary>
        /// Get a users favorite animes, mangas, characters and staff.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public Response<Favorites> GetFavourites(int id) => GetFavourites(id.ToString());

        /// <summary>
        /// Get a users favorite animes, mangas, characters and staff.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public Response<Favorites> GetFavourites(string displayName) => MakeAndExecute<Favorites>($"user/{displayName}/favourites", Method.GET);

        /// <summary>
        /// Search for a user.
        /// </summary>
        /// <param name="query">The query used to search.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public Response<SmallUser[]> SearchUser(string query) => MakeAndExecute<SmallUser[]>($"user/search/{query}", Method.GET);

        /// <summary>
        /// Get all user data including the users anime list.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public Response<BigUserAnimeList> GetAnimelist(int id) => GetAnimelist(id.ToString());

        /// <summary>
        /// Get all user data including the users anime list.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public Response<BigUserAnimeList> GetAnimelist(string displayName)
            => MakeAndExecute<BigUserAnimeList>($"user/{displayName}/animelist", Method.GET);
        
        /// <summary>
        /// Get all user data including the users manga list.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public Response<BigUserMangaList> GetMangalist(int id) => GetMangalist(id.ToString());

        /// <summary>
        /// Get all user data including the users manga list.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public Response<BigUserMangaList> GetMangalist(string displayName)
            => MakeAndExecute<BigUserMangaList>($"user/{displayName}/mangalist", Method.GET);

        public Response<SmallAnime[]> BrowseAnime(
            int? year = null,
            Season? season = null,
            MediaType? type = null,
            AnimeStatus? status = null,
            IEnumerable<Genre> genres = null,
            IEnumerable<Genre> excludedGenres = null,
            SortingMethod? sortingMethod = null,
            bool? airingData = null,
            bool? fullPage = null,
            int? page = null)
        {
            return Browse<SmallAnime>("anime", year, season, type, status, genres, excludedGenres, sortingMethod, airingData, fullPage, page);
        }

        public Response<SmallManga[]> BrowseManga(
            int? year = null,
            Season? season = null,
            MediaType? type = null,
            AnimeStatus? status = null,
            IEnumerable<Genre> genres = null,
            IEnumerable<Genre> excludedGenres = null,
            SortingMethod? sortingMethod = null,
            bool? airingData = null,
            bool? fullPage = null,
            int? page = null)
        {
            return Browse<SmallManga>("manga", year, season, type, status, genres, excludedGenres, sortingMethod, airingData, fullPage, page);
        }

        private Response<T[]> Browse<T>(
            string seriesType,
            int? year = null,
            Season? season = null,
            MediaType? type = null,
            AnimeStatus? status = null,
            IEnumerable<Genre> genres = null, // TODO: Genre enum
            IEnumerable<Genre> excludedGenres = null, // TODO: Genre enum
            SortingMethod? sortingMethod = null,
            bool? airingData = null,
            bool? fullPage = null,
            int? page = null)
        {
            var request = MakeRequest($"browse/{seriesType}", Method.GET);
            var genreConverter = new GenreMapper();

            if (year != null)
                request.AddParameter("year", year);
            if (season != null)
                request.AddParameter("season", new SeasonMapper().Type2ToType1((Season)season));
            if (type != null)
                request.AddParameter("type", type);
            if (status != null)
                request.AddParameter("status", status);
            if (genres != null)
            {
                var genresArray = genres as Genre[] ?? genres.ToArray();
                if (genresArray.Any())
                    request.AddParameter("genres", string.Join(",", genresArray.Select(genre => genreConverter.Type2ToType1(genre))));
                
            }
            if (excludedGenres != null)
            {
                var genresArray = excludedGenres as Genre[] ?? excludedGenres.ToArray();
                if (genresArray.Any())
                    request.AddParameter("genres_exclude", string.Join(",", genresArray.Select(genre => genreConverter.Type2ToType1(genre))));

            }
            if (sortingMethod != null)
                request.AddParameter("sort", new SortMapper().Type2ToType1((SortingMethod)sortingMethod));
            if (airingData != null)
                request.AddParameter("airing_data", airingData);
            if (fullPage != null)
                request.AddParameter("full_page", fullPage);
            if (page != null)
                request.AddParameter("page", page);

            return Execute<T[]>(request);
        }

        public Response<SmallAnime[]> SearchAnime(string query) => Search<SmallAnime>("anime", query);
        public Response<SmallManga[]> SearchManga(string query) => Search<SmallManga>("manga", query);
        public Response<SmallCharacter[]> SearchCharacter(string query) => Search<SmallCharacter>("character", query);
        public Response<SmallStaff[]> SearchStaff(string query) => Search<SmallStaff>("staff", query);
        public Response<Studio[]> SearchStudio(string query) => Search<Studio>("studio", query);

        public Response<AniListThread[]> SearchThread(string query)
        {
            var response = MakeAndRestExecute($"forum/search/{query}", Method.GET);
            return new Response<AniListThread[]>(response, JsonConvert.DeserializeObject<AniListThread[]>(response.Content, new ThreadSearchConverter()));
        }

        private Response<T[]> Search<T>(string seriesType, string query) where T : AniListObject =>
            MakeAndExecute<T[]>($"{seriesType}/search/{query}", Method.GET);

        public Response<Staff> GetStaff(int id) => MakeAndExecute<Staff>($"staff/{id}", Method.GET);
        public Response<Staff> GetStaffPage(int id) => MakeAndExecute<Staff>($"staff/{id}/page", Method.GET);

        public Response<Studio> GetStudio(int id) => MakeAndExecute<Studio>($"studio/{id}", Method.GET);
        public Response<Studio> GetStudioPage(int id) => MakeAndExecute<Studio>($"studio/{id}/page", Method.GET);

        public Response<AnimeReview> GetAnimeReview(int id) => MakeAndExecute<AnimeReview>($"anime/review/{id}", Method.GET);
        public Response<MangaReview> GetMangaReview(int id) => MakeAndExecute<MangaReview>($"manga/review/{id}", Method.GET);
        public Response<AnimeReview[]> GetAnimeReviews(int id) => MakeAndExecute<AnimeReview[]>($"anime/{id}/reviews", Method.GET);
        public Response<MangaReview[]> GetMangaReviews(int id) => MakeAndExecute<MangaReview[]>($"manga/{id}/reviews", Method.GET);
        public Response<UserReviews> GetUserReviews(int id) => GetUserReviews(id.ToString());
        public Response<UserReviews> GetUserReviews(string displayName) => MakeAndExecute<UserReviews>($"user/{displayName}/reviews", Method.GET);


        public Response<Feed> GetRecentThreads(int pageNumber)
            => MakeAndExecute<Feed>("forum/recent", Method.GET, "page", pageNumber);

        public Response<Feed> GetNewThreads(int pageNumber)
            => MakeAndExecute<Feed>("forum/new", Method.GET, "page", pageNumber);

        public Response<Feed> GetThreadsByTag(int pageNumber, 
            int[] tags = null, 
            int[] animes = null, 
            int[] mangas = null)
        {
            var request = MakeRequest("forum/new", Method.GET);
            request.AddParameter("page", pageNumber);

            if (tags != null)
                request.AddParameter("tags", string.Join(",", tags));
            if (animes != null)
                request.AddParameter("anime", string.Join(",", animes));
            if (mangas != null)
                request.AddParameter("manga", string.Join(",", mangas));

            return Execute<Feed>(request);
        }

        public Response<AniListThread> GetThread(int id)
            => MakeAndExecute<AniListThread>($"forum/thread/{id}", Method.GET);

        /// <summary>
        /// https://anilist-api.readthedocs.io/en/latest/authentication.html
        /// </summary>
        private Response<Credentials> GrantClientCredentials()
        {
            // NOTE: We use the base.MakeRequest here, because no access token should be added, when requesting and access token
            var request = base.MakeRequest("auth/access_token", Method.POST);
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", _clientId);
            request.AddParameter("client_secret", _clientSecret);

            return Execute<Credentials>(request);
        }

        private void RefreshCredentials()
        {
            _clientCredentialsRefresher.Stop();
            _clientCredentialsRefresher.Enabled = false;
            var response = GrantClientCredentials();

            if (response.Status == UnifiedStatus.Success)
            {
                _credentials = response.Data;
                StartTimer();
            }
        }

        private void StartTimer()
        {
            _clientCredentialsRefresher = new Timer();
            _clientCredentialsRefresher.Elapsed += (sender, e) => RefreshCredentials();
            // We refresh 10 seconds before our credentials expire, just to make sure
            // that our cridentials are always valid.
            _clientCredentialsRefresher.Interval = TimeSpan.FromSeconds(_credentials.ExpiresIn - 10).TotalMilliseconds;
            _clientCredentialsRefresher.Enabled = true;
            _clientCredentialsRefresher.Start();
        }

        protected override IRestRequest MakeRequest(string resource, Method method)
        {
            var request = base.MakeRequest(resource, method);
            request.AddParameter("access_token", _credentials?.AccessToken);
            return request;
        }
    }
}
