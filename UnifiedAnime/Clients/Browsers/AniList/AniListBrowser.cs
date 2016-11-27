using System;
using System.Collections.Generic;
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
            _clientCredentialsRefresher.Elapsed += (sender, e) => RefreshHandler();
            
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

        private void RefreshHandler()
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

        private new Response<T> MakeAndExecute<T>(string resource, Method method)
        {
            var request = MakeRequest(resource, method);
            request.AddParameter("access_token", _credentials?.AccessToken);
            return Execute<T>(request);
        }

        private new Response MakeAndExecute(string resource, Method method)
        {
            var request = MakeRequest(resource, method);
            request.AddParameter("access_token", _credentials?.AccessToken);
            return Execute(request);
        }

        private new IRestResponse MakeAndRestExecute(string resource, Method method)
        {
            var request = MakeRequest(resource, method);
            request.AddParameter("access_token", _credentials?.AccessToken);
            return RestExecute(request);
        }
    }
}
