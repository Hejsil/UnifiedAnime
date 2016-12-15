using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using RestSharp;
using UnifiedAnime.Clients.Bases;
using UnifiedAnime.Data.AniList;
using UnifiedAnime.Data.Common;
using ResponseStatus = UnifiedAnime.Data.Common.ResponseStatus;

namespace UnifiedAnime.Clients.Browsers.AniList
{
    public class AniListBrowser : RestBasedAnimeClient
    {
        public override string Url => "https://anilist.co/api/";

        private string _clientId;
        private string _clientSecret;
        private Timer _clientCredentialsRefresher;
        private Credentials _credentials;

        public Response Authenticate(string clientId, string clientSecret)
        {
            if (_clientCredentialsRefresher != null &&
                _clientCredentialsRefresher.Enabled)
                return new Response(ResponseStatus.Unknown); // TODO: Make new status

            _clientId = clientId;
            _clientSecret = clientSecret;
            _clientCredentialsRefresher = new Timer();
            _clientCredentialsRefresher.Elapsed += (sender, e) => RefreshCredentials();
            return RefreshCredentials();
        }

        public Response<User> GetUser(int id) => GetUser(id.ToString());
        public Response<User> GetUser(string displayName) => MakeAndExecute<User>($"user/{displayName}", Method.GET);

        public Response<Activity[]> GetActivity(int id) => GetActivity(id.ToString());
        public Response<Activity[]> GetActivity(string displayName) => MakeAndExecute<Activity[]>($"user/{displayName}/activity", Method.GET);

        public Response<SmallUser[]> GetFollowers(int id) => GetFollowers(id.ToString());
        public Response<SmallUser[]> GetFollowers(string displayName) => MakeAndExecute<SmallUser[]>($"user/{displayName}/followers", Method.GET);

        public Response<SmallUser[]> GetFollowing(int id) => GetFollowing(id.ToString());
        public Response<SmallUser[]> GetFollowing(string displayName) => MakeAndExecute<SmallUser[]>($"user/{displayName}/following", Method.GET);

        public Response<Favorites> GetFavourites(int id) => GetFavourites(id.ToString());
        public Response<Favorites> GetFavourites(string displayName) => MakeAndExecute<Favorites>($"user/{displayName}/favourites", Method.GET);

        public Response<SmallUser[]> SearchUser(string query) => MakeAndExecute<SmallUser[]>($"user/search/{query}", Method.GET);

        public Response<BigUser> GetAnimelist(int id) => GetAnimelist(id.ToString());
        public Response<BigUser> GetAnimelist(string displayName) => MakeAndExecute<BigUser>($"user/{displayName}/animelist", Method.GET);

        public Response<BigUser> GetMangalist(int id) => GetMangalist(id.ToString());
        public Response<BigUser> GetMangalist(string displayName) => MakeAndExecute<BigUser>($"user/{displayName}/mangalist", Method.GET);

        public Response<SmallSeries[]> GetBrowseAnime(
            int? year = null,
            string season = null, // TODO: Season enum
            MediaTypes? type = null,
            AnimeStatus? status = null,
            string[] genres = null, // TODO: Genre enum
            string[] excludedGenres = null, // TODO: Genre enum
            string sort = null, // TODO: Sort enum
            bool? airingData = null,
            bool? fullPage = null,
            int? page = null)
        {
            return GetBrowse("anime", year, season, type, status, genres, excludedGenres, sort, airingData, fullPage, page);
        }

        public Response<SmallSeries[]> GetBrowseManga(
            int? year = null,
            string season = null, // TODO: Season enum
            MediaTypes? type = null,
            AnimeStatus? status = null,
            string[] genres = null, // TODO: Genre enum
            string[] excludedGenres = null, // TODO: Genre enum
            string sort = null, // TODO: Sort enum
            bool? airingData = null,
            bool? fullPage = null,
            int? page = null)
        {
            return GetBrowse("manga", year, season, type, status, genres, excludedGenres, sort, airingData, fullPage, page);
        }

        private Response<SmallSeries[]> GetBrowse(
            string seriesType,
            int? year = null,
            string season = null, // TODO: Season enum
            MediaTypes? type = null,
            AnimeStatus? status = null,
            string[] genres = null, // TODO: Genre enum
            string[] excludedGenres = null, // TODO: Genre enum
            string sort = null, // TODO: Sort enum
            bool? airingData = null,
            bool? fullPage = null,
            int? page = null)
        {
            var request = MakeRequest($"browse/{seriesType}", Method.GET);

            if (year != null)
                request.AddParameter("year", year);
            if (!string.IsNullOrEmpty(season))
                request.AddParameter("season", season);
            if (type != null)
                request.AddParameter("type", type);
            if (status != null)
                request.AddParameter("status", status);
            if (genres != null && genres.Length > 0)
                request.AddParameter("genres", string.Join(",", genres));
            if (excludedGenres != null && excludedGenres.Length > 0)
                request.AddParameter("genres_exclude", string.Join(",", excludedGenres));
            if (sort != null)
                request.AddParameter("sort", sort);
            if (airingData != null)
                request.AddParameter("airing_data", airingData);
            if (fullPage != null)
                request.AddParameter("full_page", fullPage);
            if (page != null)
                request.AddParameter("page", page);

            return Execute<SmallSeries[]>(request);
        }

        public Response<SmallAnime[]> SearchAnime(string query) => Search<SmallAnime>("anime", query);
        public Response<SmallManga[]> SearchManga(string query) => Search<SmallManga>("manga", query);
        public Response<SmallCharacter[]> SearchCharacter(string query) => Search<SmallCharacter>("character", query);
        public Response<SmallStaff[]> SearchStaff(string query) => Search<SmallStaff>("staff", query);
        public Response<Studio[]> SearchStudio(string query) => Search<Studio>("studio", query);
        public Response<AniListThread[]> SearchThread(string query) => Search<AniListThread>("forum", query);
        private Response<T[]> Search<T>(string seriesType, string query) where T : AniListObject =>
            MakeAndExecute<T[]>($"{seriesType}/search/{query}", Method.GET);

        public Response<Staff> GetStaff(int id) => MakeAndExecute<Staff>($"staff/{id}", Method.GET);
        public Response<Staff> GetStaffPage(int id) => MakeAndExecute<Staff>($"staff/{id}/page", Method.GET);

        public Response<Studio> GetStudio(int id) => MakeAndExecute<Studio>($"studio/{id}", Method.GET);
        public Response<Studio> GetStudioPage(int id) => MakeAndExecute<Studio>($"studio/{id}/page", Method.GET);

        public Response<Review> GetReview(int id) => MakeAndExecute<Review>($"anime/{id}/reviews", Method.GET);
        public Response<Review[]> GetAnimeReviews(int id) => MakeAndExecute<Review[]>($"anime/review/{id}", Method.GET);
        public Response<Review[]> GetMangaReviews(int id) => MakeAndExecute<Review[]>($"manga/review/{id}", Method.GET);
        public Response<Review[]> GetUserReviews(int id) => GetUserReviews(id.ToString());
        public Response<Review[]> GetUserReviews(string displayName) => MakeAndExecute<Review[]>($"user/{displayName}/reviews", Method.GET);


        public Response<Feed> GetRecentThreads(int pageNumber)
        {
            var request = MakeRequest("forum/recent", Method.GET);
            request.AddParameter("page", pageNumber);

            return Execute<Feed>(request);
        }

        public Response<Feed> GetNewThreads(int pageNumber)
        {
            var request = MakeRequest("forum/new", Method.GET);
            request.AddParameter("page", pageNumber);

            return Execute<Feed>(request);
        }

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
            var request = MakeRequest("auth/access_token", Method.POST);
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", _clientId);
            request.AddParameter("client_secret", _clientSecret);

            return Execute<Credentials>(request);
        }

        private Response RefreshCredentials()
        {
            _clientCredentialsRefresher.Stop();
            _clientCredentialsRefresher.Enabled = false;
            var response = GrantClientCredentials();

            if (response.Status == ResponseStatus.Success)
            {
                _credentials = response.Data;
                StartTimer();
            }

            return response;
        }

        private void StartTimer()
        {
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
