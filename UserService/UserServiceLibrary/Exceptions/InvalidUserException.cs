using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace UserServiceLibrary.Exceptions
{
    [Serializable]
    public class InvalidUserException : UserServiceException
    {
        public InvalidUserException()
            : base()
        {
        }

        public InvalidUserException(string message)
            : base(message)
        {
        }

        public InvalidUserException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public InvalidUserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public InvalidUserException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected InvalidUserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
