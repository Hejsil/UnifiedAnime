﻿using System;
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
    public class AniListBrowser : RestBasedAnimeClient
    {
        public override string Url => "https://anilist.co/api/";

        private readonly string _clientId;
        private readonly string _clientSecret;
        private Timer _clientCredentialsRefresher;
        private Credentials _credentials;

        public AniListBrowser(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public Response Authenticate()
        {
            var response = GrantClientCredentials();
            
            if (response.Status == UnifiedStatus.Success)
            {
                _credentials = response.Data;
                StartTimer();
            }

            return response;
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
        
        public Response<BigUserAnimeList> GetAnimelist(int id) => GetAnimelist(id.ToString());

        public Response<BigUserAnimeList> GetAnimelist(string displayName)
            => MakeAndExecute<BigUserAnimeList>($"user/{displayName}/animelist", Method.GET);

        public Response<BigUserMangaList> GetMangalist(int id) => GetMangalist(id.ToString());
        public Response<BigUserMangaList> GetMangalist(string displayName)
            => MakeAndExecute<BigUserMangaList>($"user/{displayName}/mangalist", Method.GET);

        public Response<SmallSeries[]> BrowseAnime(
            int? year = null,
            Season? season = null,
            MediaType? type = null,
            AnimeStatus? status = null,
            string[] genres = null, // TODO: Genre enum
            string[] excludedGenres = null, // TODO: Genre enum
            SortingMethod? sortingMethod = null,
            bool? airingData = null,
            bool? fullPage = null,
            int? page = null)
        {
            return Browse("anime", year, season, type, status, genres, excludedGenres, sortingMethod, airingData, fullPage, page);
        }

        public Response<SmallSeries[]> BrowseManga(
            int? year = null,
            Season? season = null,
            MediaType? type = null,
            AnimeStatus? status = null,
            string[] genres = null, // TODO: Genre enum
            string[] excludedGenres = null, // TODO: Genre enum
            SortingMethod? sortingMethod = null,
            bool? airingData = null,
            bool? fullPage = null,
            int? page = null)
        {
            return Browse("manga", year, season, type, status, genres, excludedGenres, sortingMethod, airingData, fullPage, page);
        }

        private Response<SmallSeries[]> Browse(
            string seriesType,
            int? year = null,
            Season? season = null,
            MediaType? type = null,
            AnimeStatus? status = null,
            string[] genres = null, // TODO: Genre enum
            string[] excludedGenres = null, // TODO: Genre enum
            SortingMethod? sortingMethod = null,
            bool? airingData = null,
            bool? fullPage = null,
            int? page = null)
        {
            var request = MakeRequest($"browse/{seriesType}", Method.GET);

            if (year != null)
                request.AddParameter("year", year);
            if (season != null)
                request.AddParameter("season", new SeasonMapper().Type2ToType1((Season)season));
            if (type != null)
                request.AddParameter("type", type);
            if (status != null)
                request.AddParameter("status", status);
            if (genres != null && genres.Length > 0)
                request.AddParameter("genres", string.Join(",", genres));
            if (excludedGenres != null && excludedGenres.Length > 0)
                request.AddParameter("genres_exclude", string.Join(",", excludedGenres));
            if (sortingMethod != null)
                request.AddParameter("sort", new SortMapper().Type2ToType1((SortingMethod)sortingMethod));
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
            => MakeAndExecute<Credentials>("auth/access_token", Method.POST,
                new Parameters
                {
                    { "grant_type", "client_credentials" },
                    { "client_id", _clientId },
                    { "client_secret", _clientSecret },
                });

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
