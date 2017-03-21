using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UserServiceLibrary.Tests
{
    [TestClass]
    public class UserValidatorTests
    {
        [TestMethod]
        public void IsValid_UserWithNullFirstName_FalseResult()
        {
            User user = new User {FirstName = null, LastName = "Egorov", Age = 18};
            UserValidator userValidator = new UserValidator();
            bool expected = false;
            bool actual = userValidator.IsValid(user);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsValid_UserWithNullLastName_FalseResult()
        {
            User user = new User { FirstName = "Egor", LastName = null, Age = 18 };
            UserValidator userValidator = new UserValidator();
            bool expected = false;
            bool actual = userValidator.IsValid(user);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsValid_UserWithNegativeAge_FalseResult()
        {
            User user = new User { FirstName = "Egor", LastName = "Egorov", Age = -5 };
            UserValidator userValidator = new UserValidator();
            bool expected = false;
            bool actual = userValidator.IsValid(user);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsValid_UserWithZeroAge_FalseResult()
        {
            User user = new User { FirstName = "Egor", LastName = "Egorov", Age = 0 };
            UserValidator userValidator = new UserValidator();
            bool expected = false;
            bool actual = userValidator.IsValid(user);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsValid_NullUser_ArgumentNullExceptionThrown()
        {
            UserValidator userValidator = new UserValidator();
            bool actual = userValidator.IsValid(null);
        }

        [TestMethod]
        public void IsValid_UserWithValidFields_TrueResult()
        {
            User user = new User { FirstName = "Egor", LastName = "Egorov", Age = 18 };
            UserValidator userValidator = new UserValidator();
            bool expected = true;
            bool actual = userValidator.IsValid(user);
            Assert.AreEqual(expected, actual);
        }
    }
}
