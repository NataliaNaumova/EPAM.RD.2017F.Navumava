using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UserServiceLibrary.Tests
{
    [TestClass]
    public class UserStorageSerializerTest
    {
        [TestMethod]
        public void Serialize_XmlFile()
        {
            ICollection<User> users = new List<User>
            {
                new User(){FirstName = "Ivan"},
                new User(){LastName = "Ivanov"},
                new User(){FirstName = "Ivan", LastName = "Sidorov"}
            };
            var userStorageSerializer = new UserServiceStorageSerializer();
            userStorageSerializer.Serialize(users);
        }
    }
}
