using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedAnime.Data;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Clients
{
    public interface IAnimeProfile
    {
        //Response Authenticate(string username, string password);

        /// <summary>
        ///     Add an anime entry to a users library.
        /// </summary>
        /// <param name="entry">The entry to add.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        //Response Add(IAnimeEntry entry);

        /// <summary>
        ///     Update an anime entry from a users library.
        /// </summary>
        /// <param name="entry">The entry to update.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        //Response Update(IAnimeEntry entry);

        /// <summary>
        ///     Remove an anime entry from a users library.
        /// </summary>
        /// <param name="entry">The the entry to remove.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        //Response Remove(IAnimeEntry entry);

        //Response<IAnimeEntry[]> Get();
    }
}
