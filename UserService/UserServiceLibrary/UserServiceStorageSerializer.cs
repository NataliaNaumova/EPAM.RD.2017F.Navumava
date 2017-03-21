using System;
using System.Xml.Serialization;
using System.IO;
using UserServiceLibrary.Configuration;
using UserServiceLibrary.Interfaces;

namespace UserServiceLibrary
{
    /// <summary>
    /// Provides methods for working with persistent user storage.
    /// </summary>
    [Serializable]
    public class UserServiceStorageSerializer : IUserServiceStorageSerializer
    {
        #region Constants

        private const string XmlExtension = ".xml";

        #endregion

        #region Public Methods

        /// <summary>
        /// Serializes an instance of a UserServiceStorage class to a persistent storage.
        /// </summary>
        /// <param name="userServiceStorage">An instance of a UserServiceStorage class to serialize.</param>
        public void Serialize(UserServiceStorage userServiceStorage)
        {
            if (userServiceStorage == null)
            {
                throw new ArgumentNullException(nameof(userServiceStorage));
            }
            string fileName = UserServiceStorageSettings.Settings.FileName;
            CheckXmlFileName(fileName);
            XmlSerializer formatter = new XmlSerializer(typeof(UserServiceStorage));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, userServiceStorage);
            }
        }


        /// <summary>
        /// Deserializes an instance of a UserServiceStorage class from a persistent storage.
        /// </summary>
        /// <returns>An instance of a UserServiceStorage class deserialized  from a persistent storage.
        /// </returns>
        public UserServiceStorage Deserialize()
        {
            string fileName = UserServiceStorageSettings.Settings.FileName;
            CheckXmlFileName(fileName);
            XmlSerializer formatter = new XmlSerializer(typeof(UserServiceStorage));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                return formatter.Deserialize(fs) as UserServiceStorage;
            }
        }

        #endregion

        #region Private Methods

        private void CheckXmlFileName(string fileName)
        {
            bool possiblePath = fileName.IndexOfAny(Path.GetInvalidPathChars()) == -1;
            if (!(possiblePath &&
                  string.Equals(Path.GetExtension(fileName), XmlExtension, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new InvalidOperationException("Invalid file name.");
            }
        }

        #endregion
    }
}