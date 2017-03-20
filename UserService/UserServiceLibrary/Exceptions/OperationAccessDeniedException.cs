using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace UserServiceLibrary.Exceptions
{
    [Serializable]
    public class OperationAccessDeniedException : Exception
    {
        public OperationAccessDeniedException()
            : base()
        {
        }

        public OperationAccessDeniedException(string message)
            : base(message)
        {
        }

        public OperationAccessDeniedException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public OperationAccessDeniedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public OperationAccessDeniedException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected OperationAccessDeniedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
