using UnifiedAnime.Data;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Clients
{
    public interface IAnimeBrowser
    {
        /// <summary>
        ///     Get information about an anime with a certain id.
        /// </summary>
        /// <param name="id">The id of the anime.</param>
        /// <returns>A response containing the anime infomation and relevant information for error handeling.</returns>
        Response<IAnimeInfo> Get(int id);

        /// <summary>
        ///     Search on for a specific anime.
        /// </summary>
        /// <param name="name">The name used to get the search results.</param>
        /// <returns>A response containing an array of anime information and relevant information for error handeling.</returns>
        Response<IAnimeInfo[]> Search(string name);
    }
}
