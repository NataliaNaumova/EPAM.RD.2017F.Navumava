using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UserServiceLibrary.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public void Add_NormalUser_UserWasAdded()
        {
            var user = new User();
            var userService = new UserService();
            userService.Add(user);
            Assert.AreEqual(1, userService.UsersCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullUser_ExceptionThrown()
        {
            var userService = new UserService();
            userService.Add(null);
        }

        [TestMethod]
        public void Add_FourUsers_UsersWereAdded()
        {
            var user1 = new User();
            var user2 = new User();
            var user3 = new User();
            var user4 = new User();
            var userService = new UserService();
            userService.Add(user1);
            userService.Add(user2);
            userService.Add(user3);
            userService.Add(user4);
            Assert.AreEqual(4, userService.UsersCount);
        }

        [TestMethod]
        public void Delete_ExistingUser_UserWasDeleted()
        {
            var user = new User();
            var userService = new UserService();
            userService.Add(user);
            userService.Delete(user.Id);
            Assert.AreEqual(0, userService.UsersCount);
        }

        [TestMethod]
        public void Delete_NotExistingUser_NothingChanged()
        {
            var user = new User();
            var userService = new UserService();
            userService.Add(user);
            userService.Delete(user.Id+1);
            Assert.AreEqual(1, userService.UsersCount);
        }

        [TestMethod]
        public void SearchByFirstName_ExistingFirstName_UserWasFound()
        {
            var user = new User()
            {
                FirstName = "Ivan"
            };
            var userService = new UserService();
            userService.Add(user);
            var users = userService.SearchByFirstName("Ivan");
            Assert.AreEqual(1, users.Count());
        }

        [TestMethod]
        public void SearchByFirstName_NotExistingFirstName_UserWasNotFound()
        {
            var userService = new UserService();
            var users = userService.SearchByFirstName("Ivan");
            Assert.AreEqual(0, users.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SearchByFirstName_NullFirstName_ExceptionThrown()
        {
            var userService = new UserService();
            var users = userService.SearchByFirstName(null);
        }

        [TestMethod]
        public void SearchByLastName_ExistingLastName_UserWasFound()
        {
            var user = new User()
            {
                LastName = "Ivanov"
            };
            var userService = new UserService();
            userService.Add(user);
            var users = userService.SearchByLastName("Ivanov");
            Assert.AreEqual(1, users.Count());
        }

        [TestMethod]
        public void SearchByLastName_NotExistingLastName_UserWasNotFound()
        {
            var userService = new UserService();
            var users = userService.SearchByLastName("Ivanov");
            Assert.AreEqual(0, users.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SearchByLastName_NullLastName_ExceptionThrown()
        {
            var userService = new UserService();
            var users = userService.SearchByLastName(null);
        }
    }
} 
