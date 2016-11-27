using UnifiedAnime.Data;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Clients.Browsers.HummingBirdV1
{
    public class UnifiedHummingBirdV1Browser : HummingBirdV1Browser, IAnimeBrowser
    {
        public Response<IAnimeInfo> Get(int id)
        {
            var result = GetAnime(id);
            return new Response<IAnimeInfo>(result, result.Data);
        }

        public Response<IAnimeInfo[]> Search(string name)
        {
            var result = GetSearchAnime(name);

            // HACK: Using C# Co-variant. Not sure this is the best thing to do here.
            //       It is the most elegant solution however.
            return new Response<IAnimeInfo[]>(result, result.Data);
        }
    }
}