using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserServiceLibrary.Interfaces;

namespace UserServiceLibrary
{
    /// <summary>
    /// Provides methods for validating instances of the User class.
    /// </summary>
    [Serializable]
    public class UserValidator : IUserValidator
    {
        /// <summary>
        /// Checks if user has valid fields.
        /// </summary>
        /// <param name="user">User to validate.</param>
        /// <returns>Value indicating whether user is valid.</returns>
        public bool IsValid(User user)
        {
            return !((user.FirstName == null)
                     || (user.LastName == null)
                     || (user.Age <= 0));
        }
    }
}
