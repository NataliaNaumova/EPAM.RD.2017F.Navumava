using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace UserServiceLibrary.Exceptions
{
    [Serializable]
    public class UserIdAlreadyExistsException : UserServiceException
    {
        public UserIdAlreadyExistsException()
            : base()
        {
        }

        public UserIdAlreadyExistsException(string message)
            : base(message)
        {
        }

        public UserIdAlreadyExistsException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public UserIdAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public UserIdAlreadyExistsException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected UserIdAlreadyExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
