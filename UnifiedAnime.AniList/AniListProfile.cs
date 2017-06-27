using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using RestSharp.Authenticators;
using UnifiedAnime.AniList.Converters;
using UnifiedAnime.AniList.Model;
using UnifiedAnime.Bases;
using UnifiedAnime.Model;

namespace UnifiedAnime.AniList
{
    public class AniListProfile : RestBasedAnimeClient
    {
        private const string Url = "https://anilist.co/api/";

        public string AuthenticationPinLink
            => $@"{Url}auth/authorize?grant_type=authorization_pin&client_id={_clientId}&response_type=pin";
        public string AuthenticationCodeLink
            => $@"{Url}auth/authorize?grant_type=authorization_code&client_id={_clientId}&redirect_uri={_redirectUri}&response_type=code";

        private Credentials _credentials;
        public Credentials Credentials
        {
            get => _credentials;
            set
            {
                Client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(value.AccessToken, value.TokenType);
                _credentials = value;
            }
        }

        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _redirectUri;

        public AniListProfile(string clientId, string clientSecret, string redirectUri)
            : base(Url)
        {
            _clientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
            _clientSecret = clientSecret ?? throw new ArgumentNullException(nameof(clientSecret));
            _redirectUri = redirectUri ?? throw new ArgumentNullException(nameof(redirectUri));

            Client.AddDefaultHeader("Content-Type", "application/x-www-form-urlencoded");
        }

        public IRestResponse AuthenticateWithPin(string pin)
        {
            var request = MakeRequest("auth/access_token", Method.POST);
            request.AddParameter("grant_type", "authorization_pin");
            request.AddParameter("client_id", _clientId);
            request.AddParameter("client_secret", _clientSecret);
            request.AddParameter("code", pin);

            var response = Execute<Credentials>(request);
            Credentials = response.Data;

            return response;
        }

        public IRestResponse AuthenticateWithCode(string code)
        {
            var request = MakeRequest("auth/access_token", Method.POST);
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("client_id", _clientId);
            request.AddParameter("client_secret", _clientSecret);
            request.AddParameter("redirect_uri", _redirectUri);
            request.AddParameter("code", code);

            var response = Execute<Credentials>(request);
            Credentials = response.Data;

            return response;
        }

        public void LoadCredentials(string path)
        {
            var formatter = new BinaryFormatter();

            using(var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                Credentials = (Credentials)formatter.Deserialize(stream);
        }

        public void SaveCredentials(string path)
        {
            var formatter = new BinaryFormatter();

            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                formatter.Serialize(stream, Credentials);
        }

        private void RefreshCredentials()
        {
            var response = MakeAndExecute<Credentials>("auth/access_token", Method.POST,
                new Parameters
                {
                    { "grant_type", "refresh_token" },
                    { "client_id", _clientId },
                    { "client_secret", _clientSecret },
                    { "code", _credentials.RefreshToken },
                });

            Credentials = response.Data;
        }
        
        protected override bool HandleUnauthorized()
        {
            RefreshCredentials();
            return true;
        }

        public IRestResponse CreateActivityStatus(string text) => MakeAndExecute("user/activity", Method.POST, "text", text);
        public IRestResponse CreateActivityMessage(string text, int userId) => MakeAndExecute("user/activity", Method.POST,
            new Parameters
            {
                { "text", text },
                { "messenger_id", userId }
            });

        public IRestResponse CreateActivityReply(string text, int activityId) => MakeAndExecute("user/activity", Method.POST,
            new Parameters
            {
                { "text", text },
                { "reply_id", activityId },
            });

        public IRestResponse RemoveActivity(int activityId) => MakeAndExecute("user/activity", Method.DELETE, "id", activityId);
        public IRestResponse RemoveActivityReply(int replyId) => MakeAndExecute("user/activity/reply", Method.DELETE, "id", replyId);
        public IRestResponse ToggleFollow(int userId) => MakeAndExecute("user/follow", Method.POST, "id", userId);
        public IRestResponse<Anime[]> GetAiringAnimes(int limit) => MakeAndExecute<Anime[]>("user/airing", Method.GET, "limit", limit);

        public IRestResponse CreateAnimeEntry(
            int id,
            AnimeEntryStatus listStatus,
            int score, // TODO: Maybe change to some more advanced class, or something
            int episodesWatched,
            bool rewatched,
            string notes,
            IEnumerable<int> advancedRatingScores,
            IEnumerable<bool> customLists,
            bool hiddenDefault)
        {
            return CreateOrEditAnimeEntry(Method.POST, id, listStatus, 
                score, episodesWatched, rewatched, notes,
                advancedRatingScores, customLists, hiddenDefault);
        }

        public IRestResponse EditAnimeEntry(
            int id,
            AnimeEntryStatus listStatus,
            int score, // TODO: Maybe change to some more advanced class, or something
            int episodesWatched,
            bool rewatched,
            string notes,
            IEnumerable<int> advancedRatingScores,
            IEnumerable<bool> customLists,
            bool hiddenDefault)
        {
            return CreateOrEditAnimeEntry(Method.PUT, id, listStatus,
                score, episodesWatched, rewatched, notes,
                advancedRatingScores, customLists, hiddenDefault);
        }

        private IRestResponse CreateOrEditAnimeEntry(
            Method restMethod, 
            int id, 
            AnimeEntryStatus listStatus, 
            int score, 
            int episodesWatched,
            bool rewatched, 
            string notes, 
            IEnumerable<int> advancedRatingScores, 
            IEnumerable<bool> customLists, 
            bool hiddenDefault)
        {
            return MakeAndExecute("animelist", restMethod,
                new Parameters
                {
                    { "id", id },
                    { "list_status", new AnimeEntryStatusMapper().T2ToT1(listStatus) },
                    { "score_raw", score },
                    { "episodes_watched", episodesWatched },
                    { "rewatched", Convert.ToInt32(rewatched) },
                    { "notes", notes },
                    { "advanced_rating_scores", string.Join(",", advancedRatingScores) },
                    { "custom_lists", string.Join(",", customLists.Select(Convert.ToInt32)) },
                    { "hidden_default", Convert.ToInt32(hiddenDefault) },
                });
        }

        public IRestResponse CreateMangaEntry(
            int id,
            MangaEntryStatus listStatus,
            int score, // TODO: Maybe change to some more advanced class, or something
            int volumesRead,
            int chaptersRead,
            bool reread,
            string notes,
            IEnumerable<int> advancedRatingScores,
            IEnumerable<bool> customLists,
            bool hiddenDefault)
        {
            return CreateOrEditMangaEntry(Method.POST, id, listStatus, score, volumesRead, chaptersRead, reread,
                notes, advancedRatingScores, customLists, hiddenDefault);
        }

        public IRestResponse EditAnimeEntry(
            int id,
            MangaEntryStatus listStatus,
            int score, // TODO: Maybe change to some more advanced class, or something
            int volumesRead,
            int chaptersRead,
            bool reread,
            string notes,
            IEnumerable<int> advancedRatingScores,
            IEnumerable<bool> customLists,
            bool hiddenDefault)
        {
            return CreateOrEditMangaEntry(Method.PUT, id, listStatus, score, volumesRead, chaptersRead, reread,
                notes, advancedRatingScores, customLists, hiddenDefault);
        }

        private IRestResponse CreateOrEditMangaEntry(
            Method restMethod,
            int id,
            MangaEntryStatus listStatus,
            int score, // TODO: Maybe change to some more advanced class, or something
            int volumesRead,
            int chaptersRead,
            bool reread,
            string notes,
            IEnumerable<int> advancedRatingScores,
            IEnumerable<bool> customLists,
            bool hiddenDefault)
        {
            return MakeAndExecute("animelist", restMethod,
                new Parameters
                {
                    { "id", id },
                    { "list_status", new MangaEntryStatusMapper().T2ToT1(listStatus) },
                    { "score_raw", score },
                    { "volumes_read", volumesRead },
                    { "chapters_read", chaptersRead },
                    { "reread", Convert.ToInt32(reread) },
                    { "notes", notes },
                    { "advanced_rating_scores", string.Join(",", advancedRatingScores) },
                    { "custom_lists", string.Join(",", customLists.Select(Convert.ToInt32)) },
                    { "hidden_default", Convert.ToInt32(hiddenDefault) },
                });
        }
        
        public IRestResponse RemoveAnimeEntry(int id) => MakeAndExecute($"animelist/{id}", Method.DELETE);
        public IRestResponse RemoveMangaEntry(int id) => MakeAndExecute($"mangalist/{id}", Method.DELETE);

        public IRestResponse ToggleFavouriteAnime(int id) => ToggleFavourite("anime", id);
        public IRestResponse ToggleFavouriteManga(int id) => ToggleFavourite("manga", id);
        public IRestResponse ToggleFavouriteCharacter(int id) => ToggleFavourite("character", id);
        public IRestResponse ToggleFavouriteStaff(int id) => ToggleFavourite("staff", id);
        private IRestResponse ToggleFavourite(string resource, int id) => MakeAndExecute($"{resource}/favourite", Method.POST, "id", id);

        public IRestResponse RateAnimeReview(int id, ReviewRating rating) => MakeAndExecute("anime/review/rate", Method.POST,
            new Parameters
            {
                { "id", id },
                { "rating", (int)rating },
            });

        public IRestResponse RateMangaReview(int id, ReviewRating rating) => MakeAndExecute("manga/review/rate", Method.POST,
            new Parameters
            {
                { "id", id },
                { "rating", (int)rating },
            });

        public IRestResponse CreateAnimeReview(int id, string text, string summary, bool isPrivate, int score) 
            => CreateOrEditReview("anime", Method.POST, id, text, summary, isPrivate, score);

        public IRestResponse EditAnimeReview(int id, string text, string summary, bool isPrivate, int score) 
            => CreateOrEditReview("anime", Method.PUT, id, text, summary, isPrivate, score);

        public IRestResponse CreateMangaReview(int id, string text, string summary, bool isPrivate, int score)
            => CreateOrEditReview("manga", Method.POST, id, text, summary, isPrivate, score);

        public IRestResponse EditMangaReview(int id, string text, string summary, bool isPrivate, int score)
            => CreateOrEditReview("manga", Method.PUT, id, text, summary, isPrivate, score);

        private IRestResponse CreateOrEditReview(string resource, Method restMethod, int id, string text, string summary, bool isPrivate, int score) 
            => MakeAndExecute($"{resource}/review", restMethod,
                new Parameters
                {
                    { "anime_id", id },
                    { "text", text },
                    { "summary", summary },
                    { "private", Convert.ToInt32(isPrivate) },
                    { "score", score },
                });


        public IRestResponse RemoveAnimeReview(int id) => MakeAndExecute("anime/review", Method.DELETE, "id", id);
        public IRestResponse RemoveMangaReview(int id) => MakeAndExecute("manga/review", Method.DELETE, "id", id);

        public IRestResponse CreateThread(string title, string body, IEnumerable<int> tags, IEnumerable<int> tagsAnime,
            IEnumerable<int> tagsManga) => MakeAndExecute("forum/thread", Method.POST,
            new Parameters
            {
                { "title", title },
                { "body", body },
                { "tags", string.Join(",", tags) },
                { "tags_anime", string.Join(",", tagsAnime) },
                { "tags_manga", string.Join(",", tagsManga) }
            });

        public IRestResponse EditThread(int id, string title, string body, IEnumerable<int> tags, IEnumerable<int> tagsAnime,
            IEnumerable<int> tagsManga) => MakeAndExecute("forum/thread", Method.PUT,
            new Parameters
            {
                { "id", id },
                { "title", title },
                { "body", body },
                { "tags", string.Join(",", tags) },
                { "tags_anime", string.Join(",", tagsAnime) },
                { "tags_manga", string.Join(",", tagsManga) }
            });

        public IRestResponse DeleteThread(int id) => MakeAndExecute($"forum/thread/{id}", Method.DELETE);

        public IRestResponse ToggleThreadSubscription(int id)
            => MakeAndExecute("forum/comment/subscribe", Method.POST, "thread_id", id);

        public IRestResponse CreateThreadComment(int threadId, string comment, int replyId)
            => MakeAndExecute("forum/comment", Method.POST,
                new Parameters
                {
                    { "thread_id", threadId },
                    { "comment", comment },
                    { "reply_id", replyId }
                });

        public IRestResponse EditThreadComment(int id, string comment) => MakeAndExecute("forum/comment", Method.PUT,
            new Parameters
            {
                { "id", id },
                { "comment", comment }
            });
    }
}
