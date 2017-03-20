using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using UserServiceLibrary.Configuration;
using UserServiceLibrary.Interfaces;

namespace UserServiceLibrary
{
    public class UserServiceStorageSerializer : IUserServiceStorageSerializer
    {
        #region Constants

        private const string XmlExtension = ".xml";

        #endregion

        #region Public Methods

        public void Serialize(UserServiceStorage userServiceStorage)
        {
            if (userServiceStorage == null)
            {
                throw new ArgumentNullException(nameof(userServiceStorage));
            }
            string fileName = UserServiceStorageSettings.Settings.FileName;
            CheckXmlFileName(fileName);
            XmlSerializer formatter = new XmlSerializer(typeof (UserServiceStorage));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, userServiceStorage);
            }
        }

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
