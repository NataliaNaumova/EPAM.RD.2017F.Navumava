using System.Configuration;

namespace UserServiceLibrary.Configuration
{
    public class UserServiceStorageSettings : ConfigurationSection
    {
        private const string UserServiceStorageSettingsSectionName = "userServiceStorage";
        private const string FileNamePropertyName = "fileName";

        private static UserServiceStorageSettings _settings =
            ConfigurationManager.GetSection(UserServiceStorageSettingsSectionName) as UserServiceStorageSettings;

        public static UserServiceStorageSettings Settings => _settings;

        [ConfigurationProperty(FileNamePropertyName
             , IsRequired = true)]
        [StringValidator(InvalidCharacters = "\\/:*?\"<>|"
             , MinLength = 1
             , MaxLength = 256)]
        public string FileName
        {
            get { return (string)this[FileNamePropertyName]; }
            set { this[FileNamePropertyName] = value; }
        }
    }
}