using System.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace UserServiceLibrary.Configuration
{
    public class UserServiceConfiguration : ConfigurationElement
    {
        private const string AddressPropertyName = "address";
        private const string PortPropertyName = "port";
        private const string IdPropertyName = "id";

        [ConfigurationProperty(IdPropertyName, IsKey = true, IsRequired = true)]
        public int Id
        {
            get { return (int) (base[IdPropertyName]); }
            set { base[IdPropertyName] = value; }
        }

        [ConfigurationProperty(AddressPropertyName, IsRequired = true)]
        public string Address
        {
            get { return ((string)(base[AddressPropertyName])); }
            set { base[AddressPropertyName] = value; }
        }

        [ConfigurationProperty(PortPropertyName, IsRequired = true)]
        public int Port
        {
            get { return ((int)(base[PortPropertyName])); }
            set { base[PortPropertyName] = value; }
        }
    }
}
