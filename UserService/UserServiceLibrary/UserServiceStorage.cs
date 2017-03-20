using System;
using System.Collections.Generic;


namespace UserServiceLibrary
{
    /// <summary>
    /// Class containing user service storage data
    /// </summary>
    [Serializable]
    public class UserServiceStorage
    {
        /// <summary>
        /// Gets or sets list of users.
        /// </summary>
        public List<User> UserCollection { get; set; }

        /// <summary>
        /// Gets or sets last generated user id.
        /// </summary>
        public int CurrentId { get; set; }
    }
}
