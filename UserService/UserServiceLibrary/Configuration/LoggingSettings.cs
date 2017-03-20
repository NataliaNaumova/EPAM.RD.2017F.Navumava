using System.Configuration;

namespace UserServiceLibrary.Configuration
{
    public class LoggingSettings : ConfigurationSection
    {
        private const string LoggingSettingsSectionName = "loggingSettings";
        private const string IsEnabledPropertyName = "isEnabled";
        private const string LogFileNamePropertyName = "logFileName";

        private static LoggingSettings _settings = 
            ConfigurationManager.GetSection(LoggingSettingsSectionName) as LoggingSettings;

        public static LoggingSettings Settings => _settings;

        [ConfigurationProperty(IsEnabledPropertyName
             , DefaultValue = false
             , IsRequired = false)]
        public bool IsEnabled
        {
            get { return (bool) this[IsEnabledPropertyName]; }
            set { this[IsEnabledPropertyName] = value; }
        }

        [ConfigurationProperty(LogFileNamePropertyName
             , IsRequired = true)]
        [StringValidator(InvalidCharacters = "\\/:*?\"<>|" 
             , MinLength = 1
             , MaxLength = 256)]
        public string LogFileName
        {
            get { return (string) this[LogFileNamePropertyName]; }
            set { this[LogFileNamePropertyName] = value; }
        }
    }
}
