using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;
using UnifiedAnime.AniList.Converters;
using UnifiedAnime.AniList.Model;
using UnifiedAnime.Bases;
using UnifiedAnime.Model;

namespace UnifiedAnime.AniList
{
    /// <summary>
    /// A browser for the site https://anilist.co/.
    /// This browser can access everything from the site that is accessible without user authentication.
    /// For a full list, take a look at the official AniList API: https://anilist-api.readthedocs.io/en/latest/index.html
    /// </summary>
    public class AniListBrowser : RestBasedAnimeClient, IAnimeBrowser
    {
        /// <summary>
        /// The url used to access the API.
        /// </summary>
        private readonly string _clientId;
        private readonly string _clientSecret;

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
        /// Get all activities a user has on a page.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <param name="pageNumber">The page that should be returned.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public IRestResponse<Activity[]> GetActivity(int id, int pageNumber = 0) => GetActivity(id.ToString(), pageNumber);

        /// <summary>
        /// Get all activities a user has on a page.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <param name="pageNumber">The page that should be returned.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public IRestResponse<Activity[]> GetActivity(string displayName, int pageNumber = 0) =>
            MakeAndExecute<Activity[]>($"user/{displayName}/activity", Method.GET, "page", pageNumber);

        /// <summary>
        /// Get all the users that follow a user.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public IRestResponse<SmallUser[]> GetFollowers(int id) => GetFollowers(id.ToString());

        /// <summary>
        /// Get all the users that follow a user.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public IRestResponse<SmallUser[]> GetFollowers(string displayName) => MakeAndExecute<SmallUser[]>($"user/{displayName}/followers", Method.GET);

        /// <summary>
        /// Get all the users a user is following.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public IRestResponse<SmallUser[]> GetFollowing(int id) => GetFollowing(id.ToString());

        /// <summary>
        /// Get all the users a user is following.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public IRestResponse<SmallUser[]> GetFollowing(string displayName) => MakeAndExecute<SmallUser[]>($"user/{displayName}/following", Method.GET);

        /// <summary>
        /// Get a users favorite animes, mangas, characters and staff.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public IRestResponse<Favorites> GetFavourites(int id) => GetFavourites(id.ToString());

        /// <summary>
        /// Get a users favorite animes, mangas, characters and staff.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public IRestResponse<Favorites> GetFavourites(string displayName) => MakeAndExecute<Favorites>($"user/{displayName}/favourites", Method.GET);

        /// <summary>
        /// Search for a user.
        /// </summary>
        /// <param name="query">The query used to search.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public IRestResponse<SmallUser[]> SearchUser(string query) => MakeAndExecute<SmallUser[]>($"user/search/{query}", Method.GET);

        /// <summary>
        /// Get all user data including the users anime list.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public IRestResponse<BigUserAnimeList> GetAnimelist(int id) => GetAnimelist(id.ToString());

        /// <summary>
        /// Get all user data including the users anime list.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public IRestResponse<BigUserAnimeList> GetAnimelist(string displayName)
            => MakeAndExecute<BigUserAnimeList>($"user/{displayName}/animelist", Method.GET);

        /// <summary>
        /// Get all user data including the users manga list.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public IRestResponse<BigUserMangaList> GetMangalist(int id) => GetMangalist(id.ToString());

        /// <summary>
        /// Get all user data including the users manga list.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public IRestResponse<BigUserMangaList> GetMangalist(string displayName)
            => MakeAndExecute<BigUserMangaList>($"user/{displayName}/mangalist", Method.GET);

        public IRestResponse<SmallAnime[]> BrowseAnime(
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

        public IRestResponse<SmallManga[]> BrowseManga(
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

        private IRestResponse<T[]> Browse<T>(
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
                request.AddParameter("season", new SeasonMapper().T2ToT1((Season)season));
            if (type != null)
                request.AddParameter("type", type);
            if (status != null)
                request.AddParameter("status", status);
            if (genres != null)
            {
                var genresArray = genres as Genre[] ?? genres.ToArray();
                if (genresArray.Any())
                    request.AddParameter("genres", string.Join(",", genresArray.Select(genre => genreConverter.T2ToT1(genre))));

            }
            if (excludedGenres != null)
            {
                var genresArray = excludedGenres as Genre[] ?? excludedGenres.ToArray();
                if (genresArray.Any())
                    request.AddParameter("genres_exclude", string.Join(",", genresArray.Select(genre => genreConverter.T2ToT1(genre))));

            }
            if (sortingMethod != null)
                request.AddParameter("sort", new SortMapper().T2ToT1((SortingMethod)sortingMethod));
            if (airingData != null)
                request.AddParameter("airing_data", airingData);
            if (fullPage != null)
                request.AddParameter("full_page", fullPage);
            if (page != null)
                request.AddParameter("page", page);

            return Execute<T[]>(request);
        }


        /// <summary>
        /// Get a <see cref="User"/> by its id on AniList.
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public IRestResponse<User> GetUser(int id) => Get<User>(id.ToString());

        /// <summary>
        /// Get a <see cref="User"/> by its display name on AniList.
        /// </summary>
        /// <param name="displayName">The display name of the user.</param>
        /// <returns>A <see cref="Response"/> containing data for error handling, and the actual return data.</returns>
        public IRestResponse<User> GetUser(string displayName) => Get<User>(displayName);
        
        IRestResponse<IAnimeInfo> IAnimeBrowser.GetAnime(int id)
        {
            var response = GetAnime(id);
            return new Response<IAnimeInfo>(response, response.Data);
        }

        public IRestResponse<Anime> GetAnime(int id) => Get<Anime>(id.ToString());
        public IRestResponse<Manga> GetManga(int id) => Get<Manga>(id.ToString());
        private IRestResponse<T> Get<T>(string id) => MakeAndExecute<T>($"{typeof(T).Name.ToLower()}/{id}", Method.GET);

        public IRestResponse<Anime> GetAnimePage(int id) => GetPage<Anime>(id.ToString());
        public IRestResponse<Manga> GetMangaPage(int id) => GetPage<Manga>(id.ToString());
        private IRestResponse<T> GetPage<T>(string id) => MakeAndExecute<T>($"{typeof(T).Name.ToLower()}/{id}/page", Method.GET);

        public IRestResponse<SmallCharacter[]> GetAnimeCharacters(int id) => GetCharacters<SmallCharacter[]>("anime", id.ToString());
        public IRestResponse<SmallCharacter[]> GetMangaCharacters(int id) => GetCharacters<SmallCharacter[]>("manga", id.ToString());
        private IRestResponse<T> GetCharacters<T>(string resource, string id) => MakeAndExecute<T>($"{resource}/{id}/characters", Method.GET);

        public IRestResponse<SmallStaff[]> GetAnimeStaff(int id) => GetStaff<SmallStaff[]>("anime", id.ToString());
        public IRestResponse<SmallStaff[]> GetMangaStaff(int id) => GetStaff<SmallStaff[]>("manga", id.ToString());
        private IRestResponse<T> GetStaff<T>(string resource, string id) => MakeAndExecute<T>($"{resource}/{id}/staff", Method.GET);

        // GetAring

        IRestResponse<IAnimeInfo[]> IAnimeBrowser.SearchAnime(string query)
        {
            var response = SearchAnime(query);
            return new Response<IAnimeInfo[]>(response, response.Data.Cast<IAnimeInfo>().ToArray());
        }

        public IRestResponse<SmallAnime[]> SearchAnime(string query) => Search<SmallAnime>("anime", query);
        public IRestResponse<SmallManga[]> SearchManga(string query) => Search<SmallManga>("manga", query);
        public IRestResponse<SmallCharacter[]> SearchCharacter(string query) => Search<SmallCharacter>("character", query);
        public IRestResponse<SmallStaff[]> SearchStaff(string query) => Search<SmallStaff>("staff", query);
        public IRestResponse<Studio[]> SearchStudio(string query) => Search<Studio>("studio", query);

        public IRestResponse<AniListThread[]> SearchThread(string query)
        {
            var response = MakeAndExecute($"forum/search/{query}", Method.GET);
            var threads = JsonConvert.DeserializeObject<AniListThread[]>(response.Content, new ThreadSearchConverter());
            return new Response<AniListThread[]>(response, threads);
        }

        private IRestResponse<T[]> Search<T>(string seriesType, string query) where T : AniListObject =>
            MakeAndExecute<T[]>($"{seriesType}/search/{query}", Method.GET);

        public IRestResponse<Staff> GetStaff(int id) => MakeAndExecute<Staff>($"staff/{id}", Method.GET);
        public IRestResponse<Staff> GetStaffPage(int id) => MakeAndExecute<Staff>($"staff/{id}/page", Method.GET);

        public IRestResponse<Studio> GetStudio(int id) => MakeAndExecute<Studio>($"studio/{id}", Method.GET);
        public IRestResponse<Studio> GetStudioPage(int id) => MakeAndExecute<Studio>($"studio/{id}/page", Method.GET);

        public IRestResponse<AnimeReview> GetAnimeReview(int id) => MakeAndExecute<AnimeReview>($"anime/review/{id}", Method.GET);
        public IRestResponse<MangaReview> GetMangaReview(int id) => MakeAndExecute<MangaReview>($"manga/review/{id}", Method.GET);
        public IRestResponse<AnimeReview[]> GetAnimeReviews(int id) => MakeAndExecute<AnimeReview[]>($"anime/{id}/reviews", Method.GET);
        public IRestResponse<MangaReview[]> GetMangaReviews(int id) => MakeAndExecute<MangaReview[]>($"manga/{id}/reviews", Method.GET);
        public IRestResponse<UserReviews> GetUserReviews(int id) => GetUserReviews(id.ToString());
        public IRestResponse<UserReviews> GetUserReviews(string displayName) => MakeAndExecute<UserReviews>($"user/{displayName}/reviews", Method.GET);


        public IRestResponse<Feed> GetRecentThreads(int pageNumber)
            => MakeAndExecute<Feed>("forum/recent", Method.GET, "page", pageNumber);

        public IRestResponse<Feed> GetNewThreads(int pageNumber)
            => MakeAndExecute<Feed>("forum/new", Method.GET, "page", pageNumber);

        public IRestResponse<Feed> GetThreadsByTag(int pageNumber, 
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

        public IRestResponse<AniListThread> GetThread(int id)
            => MakeAndExecute<AniListThread>($"forum/thread/{id}", Method.GET);

        private void RefreshCredentials()
        {
            var response = GrantClientCredentials();
            var credentials = response.Data;

            var defaults = Client.DefaultParameters;
            defaults.Remove(defaults.FirstOrDefault(par => par.Name == "access_token"));
            Client.DefaultParameters.Add(new Parameter { Type = ParameterType.GetOrPost, Name = "access_token", Value = credentials?.AccessToken });
        }

        protected override bool HandleUnauthorized()
        {
            RefreshCredentials();
            return true;
        }

        /// <summary>
        /// https://anilist-api.readthedocs.io/en/latest/authentication.html
        /// </summary>
        private IRestResponse<Credentials> GrantClientCredentials()
        {
            var request = MakeRequest("auth/access_token", Method.POST);
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", _clientId);
            request.AddParameter("client_secret", _clientSecret);

            return Execute<Credentials>(request);
        }
    }
}
