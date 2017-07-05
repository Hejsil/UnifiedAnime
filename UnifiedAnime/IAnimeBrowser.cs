using RestSharp;
using UnifiedAnime.Bases;
using UnifiedAnime.Model;

namespace UnifiedAnime
{
    public interface IAnimeBrowser
    {
        IRestResponse<IAnimeInfo> GetAnime(int id);
        IRestResponse<IAnimeInfo[]> SearchAnime(string query);
        IRestResponse<IAnimeEntry[]> GetAnimelist(int id);
    }
}
