using System;
using UnifiedAnime.Data;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Clients
{
    /// <summary>
    ///     An interface for clients that can communicate with an anime database site.
    /// </summary>
    public interface IAnimeClient
    {
        #region Other Members

        #region LibraryRemove

        /// <summary>
        ///     Add an anime entry to a users library.
        /// </summary>
        /// <param name="entry">The entry to add.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        Response LibraryAdd(IAnimeEntry entry);

        /// <summary>
        ///     Update an anime entry from a users library.
        /// </summary>
        /// <param name="entry">The entry to update.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        Response LibraryUpdate(IAnimeEntry entry);

        /// <summary>
        ///     Remove an anime entry from a users library.
        /// </summary>
        /// <param name="entry">The the entry to remove.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        Response LibraryRemove(IAnimeEntry entry);


        #endregion

        #region Browse


        /// <summary>
        ///     Get information about an anime with a certain id.
        /// </summary>
        /// <param name="id">The id of the anime.</param>
        /// <returns>A response containing the anime infomation and relevant information for error handeling.</returns>
        Tuple<Response, IAnimeInfo> BrowseGet(int id);


        /// <summary>
        ///     Get a users favorite anime.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A response containing an array of anime information and relevant information for error handeling.</returns>
        Tuple<Response, IAnimeInfo[]> BrowseUserFavorites(string username);

        /// <summary>
        ///     Get a users anime library.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A response containing an array of anime information and relevant information for error handeling.</returns>
        Tuple<Response, IAnimeEntry[]> BrowseUserLibrary(string username);

        /// <summary>
        ///     Get all animes in a users library, which has a specific status.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="entryStatus">The status of the animes to get.</param>
        /// <returns>A response containing an array of anime information and relevant information for error handeling.</returns>
        Tuple<Response, IAnimeEntry[]> BrowseUserLibrary(string username, EntryStatus entryStatus);

        /// <summary>
        ///     Get a users feed.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A response containing an array of feed entries and relevant information for error handeling.</returns>
        Tuple<Response, IFeedEntry[]> BrowseUserFeed(string username);

        /// <summary>
        ///     Get information on a user.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A response containing the user data and relevant information for error handeling.</returns>
        Tuple<Response, IUserInfo> BrowseUserInfo(string username);

        /// <summary>
        ///     Search on for a specific anime.
        /// </summary>
        /// <param name="query">The query string used to get the search results.</param>
        /// <returns>A response containing an array of anime information and relevant information for error handeling.</returns>
        Tuple<Response, IAnimeInfo[]> BrowseAnime(string query);

        #endregion

        #region Authenticate

        /// <summary>
        ///     Authenticate using a valid email and password.
        /// </summary>
        /// <param name="email">The email of the profile you want to login to.</param>
        /// <param name="password">The password of the profile you want to login to.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        Response AuthenticateEmail(string email, string password);

        /// <summary>
        ///     Authenticate using a valid username and password.
        /// </summary>
        /// <param name="username">The username of the profile you want to login to.</param>
        /// <param name="password">The password of the profile you want to login to.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        Response AuthenticateUsername(string username, string password);

        /// <summary>
        ///     Authenticate using a valid key.
        /// </summary>
        /// <param name="key">The key used to authenticate the client.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        Response AuthenticateKey(string key);
        #endregion


        /// <summary>
        ///     Returns whether a method is supported by this client.
        /// </summary>
        /// <param name="animeClientMethod">The animeClientMethod that needs to be checked.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        bool IsMethodSupported(AnimeClientMethod animeClientMethod);

        #endregion
    }
}