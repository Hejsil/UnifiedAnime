using System;
using System.Timers;
using RestSharp;
using UnifiedAnime.Clients.Bases;
using UnifiedAnime.Data.AniList;
using UnifiedAnime.Data.Common;
using ResponseStatus = UnifiedAnime.Data.Common.ResponseStatus;

namespace UnifiedAnime.Clients.Profiles.AniList
{
    public class AniListProfile : RestBasedAnimeClient
    {
        public override string Url => "https://anilist.co/api/";

        public string AuthenticationLink
            => $@"{Url}auth/authorize?client_id={_clientId}&grant_type=authorization_pin&response_type=pin";

        private readonly string _clientId;
        private readonly string _clientSecret;
        private Timer _clientCredentialsRefresher;
        private Credentials _credentials;

        public AniListProfile(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public Response Authenticate(string authorizationPin)
        {
            var request = MakeRequest("auth/access_token", Method.POST);
            request.AddParameter("grant_type", "authorization_pin");
            request.AddParameter("client_id", _clientId);
            request.AddParameter("client_secret", _clientSecret);
            request.AddParameter("code", authorizationPin);

            var response = Execute<Credentials>(request);

            if (response.Status == ResponseStatus.Success)
            {
                _credentials = response.Data;
                StartTimer();
            }

            return response;
        }
        
        public Response CreateActivityStatus(string text)
        {
            var request = MakeRequest("user/activity", Method.POST);
            request.AddParameter("text", text);

            return Execute(request);
        }

        public Response CreateActivityMessage(string text, int userId)
        {
            var request = MakeRequest("user/activity", Method.POST);
            request.AddParameter("text", text);
            request.AddParameter("messenger_id", userId);

            return Execute(request);
        }

        public Response CreateActivityReply(string text, int activityId)
        {
            var request = MakeRequest("user/activity", Method.POST);
            request.AddParameter("text", text);
            request.AddParameter("reply_id", activityId);

            return Execute(request);
        }

        public Response RemoveActivity(int activityId)
        {
            var request = MakeRequest("user/activity", Method.DELETE);
            request.AddParameter("id", activityId);

            return Execute(request);
        }

        public Response RemoveActivityReply(int replyId)
        {
            var request = MakeRequest("user/activity/reply", Method.DELETE);
            request.AddParameter("id", replyId);

            return Execute(request);
        }

        public Response ToggleFollow(int userId)
        {
            var request = MakeRequest("user/follow", Method.POST);
            request.AddParameter("id", userId);

            return Execute(request);
        }

        public Response CreateAnimeEntry(
            int animeId,
            AnimeEntryStatus listStatus,
            int score,
            int episodesWatched,
            int rewatched,
            string notes,
            int[] advancedRatingScores,
            bool[] customLists,
            bool hiddenDefault)
        {
            return ChangeAnimeEntry(Method.POST, animeId, listStatus, 
                score, episodesWatched, rewatched, notes,
                advancedRatingScores, customLists, hiddenDefault);
        }

        private Response ChangeAnimeEntry(Method restMethod, int animeId, AnimeEntryStatus listStatus, int score, int episodesWatched,
            int rewatched, string notes, int[] advancedRatingScores, bool[] customLists, bool hiddenDefault)
        {
            throw new NotImplementedException();
        }












        private void RefreshCredentials(string refreshToken)
        {
            _clientCredentialsRefresher.Stop();
            _clientCredentialsRefresher.Enabled = false;
            
            var request = MakeRequest("auth/access_token", Method.POST);
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("client_id", _clientId);
            request.AddParameter("client_secret", _clientSecret);
            request.AddParameter("code", refreshToken);

            var response = Execute<Credentials>(request);

            if (response.Status == ResponseStatus.Success)
            {
                _credentials = response.Data;
                StartTimer();
            }
        }

        private void StartTimer()
        {
            _clientCredentialsRefresher = new Timer();
            _clientCredentialsRefresher.Elapsed += (sender, e) => RefreshCredentials(_credentials.RefreshToken);
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
