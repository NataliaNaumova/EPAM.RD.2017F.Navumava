using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace UserServiceLibrary.Exceptions
{
    /// <summary>
    /// The exception that is thrown when there was an attempt of commiting a service operation that is inavailable in current mode.  
    /// </summary>
    [Serializable]
    public class OperationAccessDeniedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the OperationAccessDeniedException class.
        /// </summary>
        public OperationAccessDeniedException()
            : base()
        {
        }


        /// <summary>
        /// Initializes a new instance of the OperationAccessDeniedException class with a specified message.
        /// </summary>
        /// <param name="message">Message of exception.</param>
        public OperationAccessDeniedException(string message)
            : base(message)
        {
        }


        /// <summary>
        /// Initializes a new instance of the OperationAccessDeniedException class with a specified format and args.
        /// </summary>
        /// <param name="format">Format of exception.</param>
        /// <param name="args">Args of exception.</param>
        public OperationAccessDeniedException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }


        /// <summary>
        /// Initializes a new instance of the OperationAccessDeniedException class with a specified message and inner exception.
        /// </summary>
        /// <param name="message">Message of exception</param>
        /// <param name="innerException">Inner exception of exception</param>
        public OperationAccessDeniedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the OperationAccessDeniedException class with a specified format, args and inner exception.
        /// </summary>
        /// <param name="format">Format of exception.</param>
        /// <param name="innerException">Inner exception of exception.</param>
        /// <param name="args">Args of exception.</param>
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
