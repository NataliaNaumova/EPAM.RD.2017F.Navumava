using System.Configuration;

namespace UserServiceLibrary.Configuration
{
    /// <summary>
    /// Represents logging section configuration class.
    /// </summary>
    public class LoggingSettings : ConfigurationSection
    {
        #region Fields and Constants

        private const string LoggingSettingsSectionName = "loggingSettings";
        private const string IsEnabledPropertyName = "isEnabled";

        private static LoggingSettings _settings = 
            ConfigurationManager.GetSection(LoggingSettingsSectionName) as LoggingSettings;

        #endregion

        #region Properties

        /// <summary>
        /// Gets logging settings section from an 'app.config'
        /// </summary>
        public static LoggingSettings Settings => _settings;

        /// <summary>
        /// Gets or sets value indicating whether logging is enabled.
        /// </summary>
        [ConfigurationProperty(IsEnabledPropertyName
            , DefaultValue = false
            , IsRequired = false)]
        public bool IsEnabled
        {
            get { return (bool)this[IsEnabledPropertyName]; }
            set { this[IsEnabledPropertyName] = value; }
        }

        #endregion


    }
}
