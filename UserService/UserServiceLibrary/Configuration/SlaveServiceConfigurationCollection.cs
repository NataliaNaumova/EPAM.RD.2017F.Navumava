using System;
using System.Configuration;

namespace UserServiceLibrary.Configuration
{
    [ConfigurationCollection(typeof(UserServiceConfiguration))]
    public class SlaveServiceConfigurationCollection : ConfigurationElementCollection
    {
        private const string SlavePropertyName = "slave";

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
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


        public override bool IsReadOnly()
        {
            return false;
        }


        protected override ConfigurationElement CreateNewElement()
        {
            return new UserServiceConfiguration();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((UserServiceConfiguration)(element)).Id;
        }

        public UserServiceConfiguration this[int idx]
        {
            get
            {
                return (UserServiceConfiguration)BaseGet(idx);
            }
        }

    }
}
