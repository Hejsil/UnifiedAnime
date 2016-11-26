using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedAnime.Clients
{
    public enum Method
    {
        LoginUsername,
        LoginEmail,
        AuthenticateKey,
        GetAnime,
        GetUserInfo,
        GetUserFeed,
        GetFavorite,
        GetLibrary,
        SearchAnime,
        AddAnime,
        UpdateAnime,
        RemoveAnime
    }
}
