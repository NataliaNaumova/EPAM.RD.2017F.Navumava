using System;
using System.Configuration;

namespace UserServiceLibrary.Configuration
{
    /// <summary>
    /// Represents settings configuration collection of slave user services. 
    /// </summary>
    [ConfigurationCollection(typeof(UserServiceConfiguration))]
    public class SlaveServiceConfigurationCollection : ConfigurationElementCollection
    {
        #region Fields
        private const string SlavePropertyName = "slave";
        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the type of the ConfigurationElementCollection.
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the SlaveServiceConfigurationCollection is read-only.
        /// </summary>
        /// <returns>true if the SlaveServiceConfigurationCollection is read-only; otherwise, false.</returns>
        public override bool IsReadOnly()
        {
            return false;
        }

        /// <summary>
        /// Gets the UserServiceConfiguration at the specified index location.
        /// </summary>
        /// <param name="index">Specified index.</param>
        /// <returns>UserServiceConfiguration at the specified index location.</returns>
        public UserServiceConfiguration this[int index]
        {
            get
            {
                return (UserServiceConfiguration)BaseGet(index);
            }
        }

        #endregion

        #region Protected Methods

        protected override ConfigurationElement CreateNewElement()
        {
            return new UserServiceConfiguration();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((UserServiceConfiguration)(element)).Id;
        }

        protected override string ElementName
        {
            get
            {
                return SlavePropertyName;
            }
        }

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(SlavePropertyName, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion




    }
}
