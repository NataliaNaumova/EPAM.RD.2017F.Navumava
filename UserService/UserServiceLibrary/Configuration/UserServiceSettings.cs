using System.Configuration;

namespace UserServiceLibrary.Configuration
{
    public class UserServiceSettings : ConfigurationSection
    {
        private const string UserServiceSettingsSectionName = "userService";
        private const string SlavesPropertyName = "slaves";
        private const string MasterPropertyName = "master";

        private static UserServiceSettings _settings =
            ConfigurationManager.GetSection(UserServiceSettingsSectionName) as UserServiceSettings;

        public static UserServiceSettings Settings => _settings;

        [ConfigurationProperty(SlavesPropertyName
             , IsRequired = true)]
        public SlaveServiceConfigurationCollection Slaves
        {
            get { return (SlaveServiceConfigurationCollection)this[SlavesPropertyName]; }
            set { this[SlavesPropertyName] = value; }
        }

        [ConfigurationProperty(MasterPropertyName, IsRequired = true)]
        public UserServiceConfiguration Master
        {
            get { return (UserServiceConfiguration) this[MasterPropertyName]; }
            set { this[MasterPropertyName] = value; }
        }
    }
}
