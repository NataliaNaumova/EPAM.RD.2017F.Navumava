using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace UserServiceLibrary.Exceptions
{
    [Serializable]
    public class UserServiceException : Exception
    {
        public UserServiceException()
            : base()
        {
        }

        public UserServiceException(string message)
            : base(message)
        {
        }

        public UserServiceException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public UserServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public UserServiceException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected UserServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
