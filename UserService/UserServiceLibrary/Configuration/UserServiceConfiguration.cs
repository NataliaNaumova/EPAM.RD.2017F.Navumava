using System.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace UserServiceLibrary.Configuration
{
    /// <summary>
    /// Represents settings configuration of an individual user service instance.
    /// </summary>
    public class UserServiceConfiguration : ConfigurationElement
    {
        #region Constants

        private const string AddressPropertyName = "address";
        private const string PortPropertyName = "port";
        private const string IdPropertyName = "id";

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets id of an user service instance.
        /// </summary>
        [ConfigurationProperty(IdPropertyName, IsKey = true, IsRequired = true)]
        public int Id
        {
            get { return (int)(base[IdPropertyName]); }
            set { base[IdPropertyName] = value; }
        }

        /// <summary>
        /// Gets or sets address of an user service instance.
        /// </summary>
        [ConfigurationProperty(AddressPropertyName, IsRequired = true)]
        public string Address
        {
            get { return ((string)(base[AddressPropertyName])); }
            set { base[AddressPropertyName] = value; }
        }

        /// <summary>
        /// Gets or sets port of an user service instance.
        /// </summary>
        [ConfigurationProperty(PortPropertyName, IsRequired = true)]
        public int Port
        {
            get { return ((int)(base[PortPropertyName])); }
            set { base[PortPropertyName] = value; }
        }

        #endregion

    }
}
