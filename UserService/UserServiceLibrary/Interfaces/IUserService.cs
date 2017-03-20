using System;
using System.Collections.Generic;

namespace UserServiceLibrary.Interfaces
{
    public interface IUserService
    {
        void Add(User user);
        void Delete(User user);
        IEnumerable<User> Search(Predicate<User> predicate);
    }
}
