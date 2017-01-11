using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Timers;
using RestSharp;
using UnifiedAnime.Clients.Bases;
using UnifiedAnime.Data.AniList;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Other.JsonConverters.AniList;

namespace UnifiedAnime.Clients.Profiles.AniList
{
    public class AniListProfile : RestBasedAnimeClient
    {
        public override string Url => "https://anilist.co/api/";

        public string AuthenticationLink
            => $@"{Url}auth/authorize?grant_type=authorization_pin&client_id={_clientId}&response_type=pin";

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
            var response = MakeAndExecute<Credentials>("auth/access_token", Method.POST,
                new Parameters
                {
                    { "grant_type", "authorization_pin" },
                    { "client_id", _clientId },
                    { "client_secret", _clientSecret },
                    { "code", authorizationPin }
                });

            if (response.Status == UnifiedStatus.Success)
            {
                _credentials = response.Data;
                StartTimer();
            }

            return response;
        }

        public Response CreateActivityStatus(string text) => MakeAndExecute("user/activity", Method.POST, "text", text);
        public Response CreateActivityMessage(string text, int userId) => MakeAndExecute("user/activity", Method.POST,
            new Parameters
            {
                { "text", text },
                { "messenger_id", userId }
            });

        public Response CreateActivityReply(string text, int activityId) => MakeAndExecute("user/activity", Method.POST,
            new Parameters
            {
                { "text", text },
                { "reply_id", activityId },
            });

        public Response RemoveActivity(int activityId) => MakeAndExecute("user/activity", Method.DELETE, "id", activityId);
        public Response RemoveActivityReply(int replyId) => MakeAndExecute("user/activity/reply", Method.DELETE, "id", replyId);
        public Response ToggleFollow(int userId) => MakeAndExecute("user/follow", Method.POST, "id", userId);
        public Response<Anime[]> GetAiringAnimes(int limit) => MakeAndExecute<Anime[]>("user/airing", Method.GET, "limit", limit);

        public Response CreateAnimeEntry(
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

        public Response EditAnimeEntry(
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

        private Response CreateOrEditAnimeEntry(
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
                    { "list_status", new AnimeEntryStatusMapper().Type2ToType1(listStatus) },
                    { "score_raw", score },
                    { "episodes_watched", episodesWatched },
                    { "rewatched", Convert.ToInt32(rewatched) },
                    { "notes", notes },
                    { "advanced_rating_scores", string.Join(",", advancedRatingScores) },
                    { "custom_lists", string.Join(",", customLists.Select(Convert.ToInt32)) },
                    { "hidden_default", Convert.ToInt32(hiddenDefault) },
                });
        }

        public Response CreateMangaEntry(
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

        public Response EditAnimeEntry(
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

        private Response CreateOrEditMangaEntry(
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
                    { "list_status", new MangaEntryStatusMapper().Type2ToType1(listStatus) },
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
        
        public Response RemoveAnimeEntry(int id) => MakeAndExecute($"animelist/{id}", Method.DELETE);
        public Response RemoveMangaEntry(int id) => MakeAndExecute($"mangalist/{id}", Method.DELETE);
        public Response ToggleFavouriteAnime(int id) => MakeAndExecute("anime/favourite", Method.POST, "id", id);
        public Response ToggleFavouriteManga(int id) => MakeAndExecute("manga/favourite", Method.POST, "id", id);
        public Response ToggleFavouriteCharacter(int id) => MakeAndExecute("character/favourite", Method.POST, "id", id);
        public Response ToggleFavouriteStaff(int id) => MakeAndExecute("staff/favourite", Method.POST, "id", id);

        public Response RateAnimeReview(int id, ReviewRating rating) => MakeAndExecute("anime/review/rate", Method.POST,
            new Parameters
            {
                { "id", id },
                { "rating", (int)rating },
            });

        public Response RateMangaReview(int id, ReviewRating rating)
        {
            var request = MakeRequest("manga/review/rate", Method.POST);
            request.AddParameter("id", id);
            request.AddParameter("rating", (int)rating);

            return Execute(request);
        }

        public Response CreateAnimeReview(int id, string text, string summary, bool isPrivate, int score) => CreateOrEditAnimeReview(Method.POST, id, text, summary, isPrivate, score);
        public Response EditAnimeReview(int id, string text, string summary, bool isPrivate, int score) => CreateOrEditAnimeReview(Method.PUT, id, text, summary, isPrivate, score);

        private Response CreateOrEditAnimeReview(Method restMethod, int id, string text, string summary, bool isPrivate,
            int score) => MakeAndExecute("anime/review", restMethod,
                new Parameters
                {
                    { "anime_id", id },
                    { "text", text },
                    { "summary", summary },
                    { "private", Convert.ToInt32(isPrivate) },
                    { "score", score },
                });

        public Response CreateMangaReview(int id, string text, string summary, bool isPrivate, int score) => CreateOrEditMangaReview(Method.POST, id, text, summary, isPrivate, score);
        public Response EditMangaReview(int id, string text, string summary, bool isPrivate, int score) => CreateOrEditMangaReview(Method.PUT, id, text, summary, isPrivate, score);

        private Response CreateOrEditMangaReview(Method restMethod, int id, string text, string summary, bool isPrivate, 
            int score) => MakeAndExecute("manga/review", restMethod,
                new Parameters
                {
                    { "manga_id", id },
                    { "text", text },
                    { "summary", summary },
                    { "private", Convert.ToInt32(isPrivate) },
                    { "score", score },
                });

        public Response RemoveAnimeReview(int id) => MakeAndExecute("anime/review", Method.DELETE, "id", id);
        public Response RemoveMangaReview(int id) => MakeAndExecute("manga/review", Method.DELETE, "id", id);

        public Response CreateThread(string title, string body, IEnumerable<int> tags, IEnumerable<int> tagsAnime,
            IEnumerable<int> tagsManga) => MakeAndExecute("forum/thread", Method.POST,
            new Parameters
            {
                { "title", title },
                { "body", body },
                { "tags", string.Join(",", tags) },
                { "tags_anime", string.Join(",", tagsAnime) },
                { "tags_manga", string.Join(",", tagsManga) }
            });

        public Response EditThread(int id, string title, string body, IEnumerable<int> tags, IEnumerable<int> tagsAnime,
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

        public Response DeleteThread(int id) => MakeAndExecute($"forum/thread/{id}", Method.DELETE);

        public Response ToggleThreadSubscription(int id)
            => MakeAndExecute("forum/comment/subscribe", Method.POST, "thread_id", id);

        public Response CreateThreadComment(int threadId, string comment, int replyId)
            => MakeAndExecute("forum/comment", Method.POST,
                new Parameters
                {
                    { "thread_id", threadId },
                    { "comment", comment },
                    { "reply_id", replyId }
                });

        public Response EditThreadComment(int id, string comment) => MakeAndExecute("forum/comment", Method.PUT,
            new Parameters
            {
                { "id", id },
                { "comment", comment }
            });



        private void RefreshCredentials(string refreshToken)
        {
            _clientCredentialsRefresher.Stop();
            _clientCredentialsRefresher.Enabled = false;

            var response = MakeAndExecute<Credentials>("auth/access_token", Method.POST,
                new Parameters
                {
                    { "grant_type", "refresh_token" },
                    { "client_id", _clientId },
                    { "client_secret", _clientSecret },
                    { "code", refreshToken },
                });

            if (response.Status == UnifiedStatus.Success)
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
