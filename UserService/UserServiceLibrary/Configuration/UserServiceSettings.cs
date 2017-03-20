using System.Configuration;

namespace UserServiceLibrary.Configuration
{
    /// <summary>
    /// Represents configuration section describing the whole structure of user service layer. 
    /// Provides information about master and slave individual service instances.
    /// </summary>
    public class UserServiceSettings : ConfigurationSection
    {
        #region Constants and Fields

        private const string UserServiceSettingsSectionName = "userService";
        private const string SlavesPropertyName = "slaves";
        private const string MasterPropertyName = "master";

        private static UserServiceSettings _settings =
            ConfigurationManager.GetSection(UserServiceSettingsSectionName) as UserServiceSettings;

        #endregion

        #region Properties

        /// <summary>
        /// Gets user service layer configuration settings.
        /// </summary>
        public static UserServiceSettings Settings => _settings;

        /// <summary>
        /// Gets or sets collection of all slave services' settings.
        /// </summary>
        [ConfigurationProperty(SlavesPropertyName
             , IsRequired = true)]
        public SlaveServiceConfigurationCollection Slaves
        {
            get { return (SlaveServiceConfigurationCollection)this[SlavesPropertyName]; }
            set { this[SlavesPropertyName] = value; }
        }

        /// <summary>
        /// Gets or sets master service settings.
        /// </summary>
        [ConfigurationProperty(MasterPropertyName, IsRequired = true)]
        public UserServiceConfiguration Master
        {
            get { return (UserServiceConfiguration) this[MasterPropertyName]; }
            set { this[MasterPropertyName] = value; }
        }

        #endregion
    }
}
