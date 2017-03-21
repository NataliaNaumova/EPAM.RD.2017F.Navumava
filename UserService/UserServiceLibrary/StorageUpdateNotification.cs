using System;
using System.Collections.Generic;

namespace UserServiceLibrary
{
    /// <summary>
    /// Provides information about an update operation on user storage.
    /// </summary>
    [Serializable]
    public class StorageUpdateNotification
    {
        /// <summary>
        /// Gets or sets user participating in an update operation.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets user collection participating in an update operation.
        /// </summary>
        public List<User> UserCollection { get; set; }

        /// <summary>
        /// Gets or sets an update operation type.
        /// </summary>
        public UpdateType UpdateType { get; set; }
    }

    /// <summary>
    /// Represents an update operation type.
    /// </summary>
    public enum UpdateType
    {
        /// <summary>
        /// Represents an add operation type.
        /// </summary>
        Add = 0,
        /// <summary>
        /// Represents an delete operation type.
        /// </summary>
        Delete = 1,
        /// <summary>
        /// Represents an restore operation type. This operation means whole storage replacement.
        /// </summary>
        Restore = 2
    }
}
