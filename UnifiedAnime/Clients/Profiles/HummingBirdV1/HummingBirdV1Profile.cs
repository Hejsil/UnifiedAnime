using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using UnifiedAnime.Clients.Bases;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Data.HummingBirdV1;
using UnifiedAnime.Other.JsonConverters.HummingBirdV1;
using System.Security;
using System.Text.RegularExpressions;

namespace UnifiedAnime.Clients.Profiles.HummingBirdV1
{
    public class HummingBirdV1Profile : RestBasedAnimeClient
    {
        public override string Url => "https://hummingbird.me/api/v1";

        /// <summary>
        /// http://regexr.com/38okv
        /// </summary>
        private readonly Regex _isEmail = new Regex(@"[a-zA-Z0-9]+(?:(\.|_)[A-Za-z0-9!#$%&'*+/=?^`{|}~-]+)*@(?!([a-zA-Z0-9]*\.[a-zA-Z0-9]*\.[a-zA-Z0-9]*\.))(?:[A-Za-z0-9](?:[a-zA-Z0-9-]*[A-Za-z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?");
        private string _token;

        /// <summary>
        /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Methods#library--addupdate-entry
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="privacy"></param>
        /// <param name="rating"></param>
        /// <param name="saneRatingUpdate"></param>
        /// <param name="rewatching"></param>
        /// <param name="rewatchedTimes"></param>
        /// <param name="notes"></param>
        /// <param name="episodesWatched"></param>
        /// <param name="incrementEpisodes"></param>
        /// <returns></returns>
        public Response<LibraryEntry> AddOrUpdateEntry(
                int id, // required
                AnimeEntryStatus? status = null, // optional
                bool? privacy = null, // optional
                double? rating = null, // optional
                double? saneRatingUpdate = null, // optional
                bool? rewatching = null, // optional
                int? rewatchedTimes = null, // optional
                string notes = null, // optional
                int? episodesWatched = null, // optional
                bool? incrementEpisodes = null // optional
            )
        {
            var request = MakeRequest($"/libraries/{id}", Method.POST);
            request.AddParameter("auth_token", _token);

            if (status != null)
            {
                var result = new AnimeEntryStatusMapper().Type2ToType1((AnimeEntryStatus)status);
                request.AddParameter("status", result);
            }

            if (privacy != null)
                request.AddParameter("privacy", (bool)privacy ? "private" : "public");

            if (rating != null)
                request.AddParameter("rating", rating);

            if (saneRatingUpdate != null)
                request.AddParameter("sane_rating_update", saneRatingUpdate);

            if (rewatching != null)
                request.AddParameter("rewatching", rewatching);

            if (rewatchedTimes != null)
                request.AddParameter("rewatched_times", rewatchedTimes);

            if (rewatchedTimes != null)
                request.AddParameter("rewatched_times", rewatchedTimes);

            if (notes != null)
                request.AddParameter("notes", notes);

            if (episodesWatched != null)
                request.AddParameter("episodes_watched", episodesWatched);

            if (incrementEpisodes != null)
                request.AddParameter("increment_episodes", incrementEpisodes);

            return Execute<LibraryEntry>(request);
        }

        /// <summary>
        /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Methods#library--remove-entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Response RemoveEntry(int id)
        {
            var request = MakeRequest($"/libraries/{id}/remove", Method.POST);
            request.AddParameter("auth_token", _token);

            return Execute(request);
        }

        /// <summary>
        /// https://github.com/hummingbird-me/hummingbird/wiki/API-v1-Methods#user--authenticate
        /// </summary>
        /// <param name="usernameOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Response Login(string usernameOrEmail, string password)
        {
            var request = MakeRequest("/users/authenticate", Method.POST);
            
            request.AddParameter(_isEmail.IsMatch(usernameOrEmail) ? "username" : "email", usernameOrEmail);
            request.AddParameter("password", password);

            var response = RestExecute(request);
            _token = response.Content;

            return new Response(response);
        }
    }
}
