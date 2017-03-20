using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace UserServiceLibrary.Interfaces
{
    public interface IUserValidator
    {
        bool IsValid(User user);
    }
}
