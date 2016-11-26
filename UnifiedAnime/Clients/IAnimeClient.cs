using System.Collections;
using System.Collections.Generic;
using RestSharp;
using UnifiedAnime.Data;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Clients
{
    /// <summary>
    /// An interface for clients that can communicate with an anime database site.
    /// </summary>
    public interface IAnimeClient
    {
        /// <summary>
        /// Login using a valid username and password.
        /// </summary>
        /// <param name="username">The username of the profile you want to login to.</param>
        /// <param name="password">The password of the profile you want to login to.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        Response LoginUsername(string username, string password);

        /// <summary>
        /// Login using a valid email and password.
        /// </summary>
        /// <param name="email">The email of the profile you want to login to.</param>
        /// <param name="password">The password of the profile you want to login to.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        Response LoginEmail(string email, string password);

        /// <summary>
        /// Use a key to authenticate the client, granting it extra permissions.
        /// </summary>
        /// <param name="key">The key used to authenticate the client.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        Response AuthenticateKey(string key);

        /// <summary>
        /// Get information about an anime with a certain id.
        /// </summary>
        /// <param name="id">The id of the anime.</param>
        /// <returns>A response containing the anime infomation and relevant information for error handeling.</returns>
        Response<IAnimeInfo> GetAnime(int id);

        /// <summary>
        /// Get information on a user.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A response containing the user data and relevant information for error handeling.</returns>
        Response<IUserInfo> GetUserInfo(string username);
        
        /// <summary>
        /// Get a users feed.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A response containing an array of feed entries and relevant information for error handeling.</returns>
        Response<IFeedEntry[]> GetUserFeed(string username);
        
        /// <summary>
        /// Get a users favorite anime.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A response containing an array of anime information and relevant information for error handeling.</returns>
        Response<IAnimeInfo[]> GetFavorite(string username);

        /// <summary>
        /// Get a users anime library.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A response containing an array of anime information and relevant information for error handeling.</returns>
        Response<IAnimeInfo[]> GetLibrary(string username);

        /// <summary>
        /// Get all animes in a users library, which has a specific status.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="animeStatus">The status of the animes to get.</param>
        /// <returns>A response containing an array of anime information and relevant information for error handeling.</returns>
        Response<IAnimeInfo[]> GetLibrary(string username, AnimeStatus animeStatus);
        
        /// <summary>
        /// Search on for a specific anime.
        /// </summary>
        /// <param name="query">The query string used to get the search results.</param>
        /// <returns>A response containing an array of anime information and relevant information for error handeling.</returns>
        Response<IAnimeInfo[]> SearchAnime(string query);
        
        /// <summary>
        /// Add an anime entry to a users library.
        /// </summary>
        /// <param name="entry">The entry to add.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        Response AddAnime(IAnimeEntry entry);
        
        /// <summary>
        /// Update an anime entry from a users library.
        /// </summary>
        /// <param name="entry">The entry to update.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        Response UpdateAnime(IAnimeEntry entry);
        
        /// <summary>
        /// Remove an anime entry from a users library.
        /// </summary>
        /// <param name="id">The id of the entry to remove.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        Response RemoveAnime(int id);

        /// <summary>
        /// Remove an anime entry from a users library.
        /// </summary>
        /// <param name="entry">The the entry to remove.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        Response RemoveAnime(IAnimeEntry entry);


        /// <summary>
        /// Returns whether a method is supported by this client.
        /// </summary>
        /// <param name="method">The method that needs to be checked.</param>
        /// <returns>A response containing relevant information for error handeling.</returns>
        bool IsSupported(Method method);
    }
}
