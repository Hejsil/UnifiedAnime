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
        private readonly string _clientId;
        private readonly string _clientSecret;
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
            : base("https://anilist.co/api/")
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        /// <summary>
        /// Get a <see cref="User"/> by its id on AniList.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public (User User, IRestResponse RestResponse) GetUser(int id) => GetUser(id.ToString());

        /// <summary>
        /// Get a <see cref="User"/> by its display name on AniList.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public (User User, IRestResponse RestResponse) GetUser(string displayName) => MakeAndExecute<User>($"user/{displayName}", Method.GET);

        /// <summary>
        /// Get all activities a user has on a page.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <param name="pageNumber">The page that should be returned.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public (Activity[] Activities, IRestResponse RestResponse) GetActivity(int id, int pageNumber = 0) => GetActivity(id.ToString(), pageNumber);

        /// <summary>
        /// Get all activities a user has on a page.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <param name="pageNumber">The page that should be returned.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public (Activity[] Activities, IRestResponse RestResponse) GetActivity(string displayName, int pageNumber = 0) =>
            MakeAndExecute<Activity[]>($"user/{displayName}/activity", Method.GET, "page", pageNumber);

        /// <summary>
        /// Get all the users that follow a user.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public (SmallUser[] Users, IRestResponse RestResponse) GetFollowers(int id) => GetFollowers(id.ToString());

        /// <summary>
        /// Get all the users that follow a user.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public (SmallUser[] Users, IRestResponse RestResponse) GetFollowers(string displayName) => MakeAndExecute<SmallUser[]>($"user/{displayName}/followers", Method.GET);

        /// <summary>
        /// Get all the users a user is following.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public (SmallUser[] Users, IRestResponse RestResponse) GetFollowing(int id) => GetFollowing(id.ToString());

        /// <summary>
        /// Get all the users a user is following.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public (SmallUser[] Users, IRestResponse RestResponse) GetFollowing(string displayName) => MakeAndExecute<SmallUser[]>($"user/{displayName}/following", Method.GET);

        /// <summary>
        /// Get a users favorite animes, mangas, characters and staff.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public (Favorites Favorites, IRestResponse RestResponse) GetFavourites(int id) => GetFavourites(id.ToString());

        /// <summary>
        /// Get a users favorite animes, mangas, characters and staff.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public (Favorites Favorites, IRestResponse RestResponse) GetFavourites(string displayName) => MakeAndExecute<Favorites>($"user/{displayName}/favourites", Method.GET);

        /// <summary>
        /// Search for a user.
        /// </summary>
        /// <param name="query">The query used to search.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public (SmallUser[] Users, IRestResponse RestResponse) SearchUser(string query) => MakeAndExecute<SmallUser[]>($"user/search/{query}", Method.GET);

        /// <summary>
        /// Get all user data including the users anime list.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public (BigUserAnimeList UserWithAnimeList, IRestResponse RestResponse) GetAnimelist(int id) => GetAnimelist(id.ToString());

        /// <summary>
        /// Get all user data including the users anime list.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public (BigUserAnimeList UserWithAnimeList, IRestResponse RestResponse) GetAnimelist(string displayName)
            => MakeAndExecute<BigUserAnimeList>($"user/{displayName}/animelist", Method.GET);

        /// <summary>
        /// Get all user data including the users manga list.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public (BigUserMangaList UserWithMangaList, IRestResponse RestResponse) GetMangalist(int id) => GetMangalist(id.ToString());

        /// <summary>
        /// Get all user data including the users manga list.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public (BigUserMangaList UserWithMangaList, IRestResponse RestResponse) GetMangalist(string displayName)
            => MakeAndExecute<BigUserMangaList>($"user/{displayName}/mangalist", Method.GET);

        public (SmallAnime[] Animes, IRestResponse RestResponse) BrowseAnime(
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

        public (SmallManga[] Mangas, IRestResponse RestResponse) BrowseManga(
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

        private (T[], IRestResponse) Browse<T>(
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

        public (SmallAnime[] Animes, IRestResponse RestResponse) SearchAnime(string query) => Search<SmallAnime>("anime", query);
        public (SmallManga[] Mangas, IRestResponse RestResponse) SearchManga(string query) => Search<SmallManga>("manga", query);
        public (SmallCharacter[] Characters, IRestResponse RestResponse) SearchCharacter(string query) => Search<SmallCharacter>("character", query);
        public (SmallStaff[] Staff, IRestResponse RestResponse) SearchStaff(string query) => Search<SmallStaff>("staff", query);
        public (Studio[] Studios, IRestResponse RestResponse) SearchStudio(string query) => Search<Studio>("studio", query);

        public (AniListThread[] Threads, IRestResponse RestResponse) SearchThread(string query)
        {
            var response = MakeAndExecute($"forum/search/{query}", Method.GET);
            var threads = JsonConvert.DeserializeObject<AniListThread[]>(response.Content, new ThreadSearchConverter());
            return (threads, response);
        }

        private (T[], IRestResponse) Search<T>(string seriesType, string query) where T : AniListObject =>
            MakeAndExecute<T[]>($"{seriesType}/search/{query}", Method.GET);

        public (Staff Staff, IRestResponse RestResponse) GetStaff(int id) => MakeAndExecute<Staff>($"staff/{id}", Method.GET);
        public (Staff Staff, IRestResponse RestResponse) GetStaffPage(int id) => MakeAndExecute<Staff>($"staff/{id}/page", Method.GET);

        public (Studio Studio, IRestResponse RestResponse) GetStudio(int id) => MakeAndExecute<Studio>($"studio/{id}", Method.GET);
        public (Studio Studio, IRestResponse RestResponse) GetStudioPage(int id) => MakeAndExecute<Studio>($"studio/{id}/page", Method.GET);

        public (AnimeReview Review, IRestResponse RestResponse) GetAnimeReview(int id) => MakeAndExecute<AnimeReview>($"anime/review/{id}", Method.GET);
        public (MangaReview Review, IRestResponse RestResponse) GetMangaReview(int id) => MakeAndExecute<MangaReview>($"manga/review/{id}", Method.GET);
        public (AnimeReview[] Reviews, IRestResponse RestResponse) GetAnimeReviews(int id) => MakeAndExecute<AnimeReview[]>($"anime/{id}/reviews", Method.GET);
        public (MangaReview[] Reviews, IRestResponse RestResponse) GetMangaReviews(int id) => MakeAndExecute<MangaReview[]>($"manga/{id}/reviews", Method.GET);
        public (UserReviews Reviews, IRestResponse RestResponse) GetUserReviews(int id) => GetUserReviews(id.ToString());
        public (UserReviews Review, IRestResponse RestResponse) GetUserReviews(string displayName) => MakeAndExecute<UserReviews>($"user/{displayName}/reviews", Method.GET);


        public (Feed Feed, IRestResponse RestResponse) GetRecentThreads(int pageNumber)
            => MakeAndExecute<Feed>("forum/recent", Method.GET, "page", pageNumber);

        public (Feed Feed, IRestResponse RestResponse) GetNewThreads(int pageNumber)
            => MakeAndExecute<Feed>("forum/new", Method.GET, "page", pageNumber);

        public (Feed Feed, IRestResponse RestResponse) GetThreadsByTag(int pageNumber, 
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

        public (AniListThread Feed, IRestResponse RestResponse) GetThread(int id)
            => MakeAndExecute<AniListThread>($"forum/thread/{id}", Method.GET);

        public IRestResponse RefreshCredentials()
        {
            var response = GrantClientCredentials();
            _credentials = response.Credentials;

            return response.RestResponse;
        }

        /// <summary>
        /// https://anilist-api.readthedocs.io/en/latest/authentication.html
        /// </summary>
        private (Credentials Credentials, IRestResponse RestResponse) GrantClientCredentials()
        {
            // NOTE: We use the base.MakeRequest here, because no access token should be added, when requesting and access token
            var request = base.MakeRequest("auth/access_token", Method.POST);
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", _clientId);
            request.AddParameter("client_secret", _clientSecret);

            return Execute<Credentials>(request);
        }
        
        protected override IRestRequest MakeRequest(string resource, Method method)
        {
            var request = base.MakeRequest(resource, method);
            request.AddParameter("access_token", _credentials?.AccessToken);
            return request;
        }
    }
}
