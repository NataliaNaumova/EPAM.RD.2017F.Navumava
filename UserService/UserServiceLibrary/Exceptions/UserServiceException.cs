using System;
using System.Runtime.Serialization;

namespace UserServiceLibrary.Exceptions
{
    /// <summary>
    /// The exception that is thrown when there was an exceptional situation during the work with user service layer.
    /// </summary>
    [Serializable]
    public class UserServiceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the UserServiceException class.
        /// </summary>
        public UserServiceException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the UserServiceException class with a specified message.
        /// </summary>
        /// <param name="message">Message of exception.</param>
        public UserServiceException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the UserServiceException class with a specified format and args.
        /// </summary>
        /// <param name="format">Format of exception.</param>
        /// <param name="args">Args of exception.</param>
        public UserServiceException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        /// <summary>
        /// Initializes a new instance of the UserServiceException class with a specified message and inner exception.
        /// </summary>
        /// <param name="message">Message of exception</param>
        /// <param name="innerException">Inner exception of exception</param>
        public UserServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the UserServiceException class with a specified format, args and inner exception.
        /// </summary>
        /// <param name="format">Format of exception.</param>
        /// <param name="innerException">Inner exception of exception.</param>
        /// <param name="args">Args of exception.</param>
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
