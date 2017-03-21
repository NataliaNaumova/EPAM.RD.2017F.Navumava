using System.Configuration;

namespace UserServiceLibrary.Configuration
{
    /// <summary>
    /// Represents user service storage settings section configuration class.
    /// </summary>
    public class UserServiceStorageSettings : ConfigurationSection
    {
        #region Constants and Fields

        private const string UserServiceStorageSettingsSectionName = "userServiceStorage";
        private const string FileNamePropertyName = "fileName";

        #endregion

        #region Properties

        private static UserServiceStorageSettings _settings =
            ConfigurationManager.GetSection(UserServiceStorageSettingsSectionName) as UserServiceStorageSettings;

        /// <summary>
        /// Gets user service storage settings section from an 'app.config'
        /// </summary>
        public static UserServiceStorageSettings Settings => _settings;

        /// <summary>
        /// Gets or sets file name of user service persistent storage.
        /// </summary>
        [ConfigurationProperty(FileNamePropertyName
             , IsRequired = true)]
        public string FileName
        {
            get { return (string)this[FileNamePropertyName]; }
            set { this[FileNamePropertyName] = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns value that indicates whether settings are readonly or not.
        /// </summary>
        /// <returns>Value that indicates whether settings are readonly or not.</returns>
        public override bool IsReadOnly()
        {
            return false;
        }

        #endregion
    }
}