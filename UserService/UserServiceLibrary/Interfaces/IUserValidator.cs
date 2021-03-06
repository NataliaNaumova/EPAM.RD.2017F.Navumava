﻿namespace UserServiceLibrary.Interfaces
{
    /// <summary>
    /// Provides methods for validating instances of the User class.
    /// </summary>
    public interface IUserValidator
    {
        /// <summary>
        /// Checks if user has valid fields.
        /// </summary>
        /// <param name="user">User to validate.</param>
        /// <returns>Value indicating whether user is valid.</returns>
        bool IsValid(User user);
    }
}
