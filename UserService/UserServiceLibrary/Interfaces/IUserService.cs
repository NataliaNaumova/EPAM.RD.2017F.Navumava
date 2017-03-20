using System;
using System.Collections.Generic;

namespace UserServiceLibrary.Interfaces
{
    /// <summary>
    /// Provides read and write operations on user storage.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Add a new user to a user storage. Available only for services running in master mode.
        /// </summary>
        /// <param name="user">An instance of a User class to add.</param>
        void Add(User user);

        /// <summary>
        /// Delete a user from a user storage. Available only for services running in master mode.
        /// </summary>
        /// <param name="user">An instance of a User class to delete.</param>
        void Delete(User user);

        /// <summary>
        /// Search users by a predicate.
        /// </summary>
        /// <param name="predicate">Function that defines a criterion for search.</param>
        /// <returns>Users instances that meet the criterion of search.</returns>
        IEnumerable<User> Search(Predicate<User> predicate);
    }
}
