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

        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly Timer _clientCredentialsRefresher;
        private Credentials _credentials;

        public AniListBrowser(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;

            _clientCredentialsRefresher = new Timer();
            _clientCredentialsRefresher.Elapsed += (sender, e) => RefreshCredentials();
            
            if (GrantClientCredentials())
                StartTimer();
        }

        public Response<User> GetUser(int id) => GetUser(id.ToString());
        public Response<User> GetUser(string displayName) => MakeAndExecute<User>($"user/{displayName}", Method.GET);

        public Response<Activity[]> GetActivity(int id) => GetActivity(id.ToString());
        public Response<Activity[]> GetActivity(string displayName) => MakeAndExecute<Activity[]>($"user/{displayName}/activity", Method.GET);

        public Response<SmallUser[]> GetFollowers(int id) => GetFollowers(id.ToString());
        public Response<SmallUser[]> GetFollowers(string displayName) => MakeAndExecute<SmallUser[]>($"user/{displayName}/followers", Method.GET);

        public Response<SmallUser[]> GetFollowing(int id) => GetFollowing(id.ToString());
        public Response<SmallUser[]> GetFollowing(string displayName) => MakeAndExecute<SmallUser[]>($"user/{displayName}/following", Method.GET);

        public Response<Series[]> GetFavourites(int id) => GetFavourites(id.ToString());
        public Response<Series[]> GetFavourites(string displayName) => MakeAndExecute<Series[]>($"user/{displayName}/following", Method.GET);

        public Response<SmallUser[]> SearchUser(string query) => MakeAndExecute<SmallUser[]>($"user/search/{query}", Method.GET);

        public Response<Anime[]> GetAnimelist(int id) => GetAnimelist(id.ToString());
        public Response<Anime[]> GetAnimelist(string displayName) => MakeAndExecute<Anime[]>($"user/{displayName}/animelist", Method.GET);

        public Response<Manga[]> GetMangalist(int id) => GetMangalist(id.ToString());
        public Response<Manga[]> GetMangalist(string displayName) => MakeAndExecute<Manga[]>($"user/{displayName}/mangalist", Method.GET);

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

        public Response<Anime[]> SearchAnime(string query) => SearchSeries<Anime>("anime", query);
        public Response<Manga[]> SearchManga(string query) => SearchSeries<Manga>("manga", query);
        public Response<Character[]> SearchCharacter(string query) => SearchSeries<Character>("character", query); // TODO: Small Character
        private Response<T[]> SearchSeries<T>(string seriesType, string query) => MakeAndExecute<T[]>($"{seriesType}/search/{query}", Method.GET);



        /// <summary>
        /// https://anilist-api.readthedocs.io/en/latest/authentication.html
        /// </summary>
        private bool GrantClientCredentials()
        {
            var request = MakeRequest("auth/access_token", Method.POST);
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", _clientId);
            request.AddParameter("client_secret", _clientSecret);

            var response = Execute<Credentials>(request);

            // TODO: We might need better checks, but this is fine for now.
            if (response.Status == ResponseStatus.Success)
            {
                _credentials = response.Data;
                return true;
            }
            return false;
        }

        private void RefreshCredentials()
        {
            _clientCredentialsRefresher.Stop();

            if (GrantClientCredentials())
                StartTimer();
        }

        private void StartTimer()
        {
            // We refresh 10 seconds before our credentials expire, just to make sure
            // that our cridentials are always valid.
            _clientCredentialsRefresher.Interval = TimeSpan.FromSeconds(_credentials.ExpiresIn - 10).TotalMilliseconds;
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
