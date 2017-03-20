using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserServiceLibrary.Exceptions
{
    [Serializable]
    public class UserNotFoundException : UserServiceException
    {
        public UserNotFoundException()
            : base()
        {
        }

        public UserNotFoundException(string message)
            : base(message)
        {
        }

        public UserNotFoundException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public UserNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public UserNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }
    }
}
