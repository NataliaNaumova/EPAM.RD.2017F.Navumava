using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserServiceLibrary
{
    public interface IUserService
    {
        int UsersCount { get; }
        void Add(User user);
        void Delete(int userId);
        IEnumerable<User> SearchByFirstName(string firstName);
        IEnumerable<User> SearchByLastName(string lastName);
    }
}
