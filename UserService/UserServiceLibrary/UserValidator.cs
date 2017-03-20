using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserServiceLibrary.Interfaces;

namespace UserServiceLibrary
{
    public class UserValidator : IUserValidator
    {
        public bool IsValid(User user)
        {
            return !((user.FirstName == null)
                     || (user.LastName == null)
                     || (user.Age <= 0));
        }
    }
}
