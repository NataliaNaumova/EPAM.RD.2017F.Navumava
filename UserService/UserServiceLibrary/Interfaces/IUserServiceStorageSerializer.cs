namespace UserServiceLibrary.Interfaces
{
    /// <summary>
    /// Provides methods for working with persistent user storage.
    /// </summary>
    public interface IUserServiceStorageSerializer
    {
        /// <summary>
        /// Serializes an instance of a UserServiceStorage class to a persistent storage.
        /// </summary>
        /// <param name="userServiceStorage">An instance of a UserServiceStorage class to serialize.</param>
        void Serialize(UserServiceStorage userServiceStorage);

        /// <summary>
        /// Deserializes an instance of a UserServiceStorage class from a persistent storage.
        /// </summary>
        /// <returns>An instance of a UserServiceStorage class deserialized  from a persistent storage.
        /// </returns>
        UserServiceStorage Deserialize();
    }
}
