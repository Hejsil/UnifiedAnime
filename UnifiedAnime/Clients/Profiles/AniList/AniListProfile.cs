using System;
using UnifiedAnime.Clients.Bases;

namespace UnifiedAnime.Clients.Profiles.AniList
{
    public class AniListProfile : RestBasedAnimeClient
    {
        public override string Url => "https://anilist.co/api/";

        public Response<string> Authorize()
    }
}
