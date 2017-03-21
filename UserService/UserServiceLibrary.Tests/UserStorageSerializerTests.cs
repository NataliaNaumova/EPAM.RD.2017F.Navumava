using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserServiceLibrary.Configuration;
using System.Configuration;
using System.Reflection;

namespace UserServiceLibrary.Tests
{
    [TestClass]
    public class UserStorageSerializerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Serialiaze_NullUserServiceStorage_ArgumentNullExceptionThrown()
        {
            var userServiceStorageSerializer = new UserServiceStorageSerializer();
            userServiceStorageSerializer.Serialize(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Serialiaze_InvalidPersistentStorageFileName_InvalidOperationExceptionThrown()
        {
            //System.Configuration.Configuration configuration = ConfigurationManager.
            //    OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            //(configuration.Sections["userServiceStorage"] as UserServiceStorageSettings)
            //    .FileName = "invalid";
            //configuration.Save();
            //ConfigurationManager.RefreshSection("userServiceStorage");

            //UserServiceStorageSettings.Settings.FileName = "invalid";
            var userServiceStorage = new UserServiceStorage();
            var userServiceStorageSerializer = new UserServiceStorageSerializer();
            userServiceStorageSerializer.Serialize(userServiceStorage);
        }

        //[TestMethod]
        //public void Serialize_XmlFile()
        //{
        //    ICollection<User> users = new List<User>
        //    {
        //        new User(){FirstName = "Ivan"},
        //        new User(){LastName = "Ivanov"},
        //        new User(){FirstName = "Ivan", LastName = "Sidorov"}
        //    };
        //    var userStorageSerializer = new UserServiceStorageSerializer();
        //    userStorageSerializer.Serialize(users);
        //}
    }
}
