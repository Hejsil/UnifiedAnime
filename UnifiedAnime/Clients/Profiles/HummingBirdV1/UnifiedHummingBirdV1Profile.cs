using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedAnime.Clients.Browsers.HummingBirdV1;
using UnifiedAnime.Data;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Clients.Profiles.HummingBirdV1
{
    public class UnifiedHummingBirdV1Profile : HummingBirdV1Profile, IAnimeProfile
    {
        private readonly HummingBirdV1Browser _browser;
        private string _username;

        public UnifiedHummingBirdV1Profile()
            : this(new HummingBirdV1Browser())
        { }

        public UnifiedHummingBirdV1Profile(HummingBirdV1Browser browser)
        {
            _browser = browser;
        }

        public Response Authenticate(string username, string password)
        {
            _username = username;
            return Login(username, password);
        }

        public Response Add(IAnimeEntry entry) => AddOrUpdateEntry(entry.Id);

        public Response<IAnimeEntry[]> Get()
        {
            var response = _browser.GetLibrary(_username);
            return new Response<IAnimeEntry[]>(response, response.Data);
        }

        public Response Remove(IAnimeEntry entry) => RemoveEntry(entry.Id);

        public Response Update(IAnimeEntry entry) => AddOrUpdateEntry(entry.Id);
    }
}
