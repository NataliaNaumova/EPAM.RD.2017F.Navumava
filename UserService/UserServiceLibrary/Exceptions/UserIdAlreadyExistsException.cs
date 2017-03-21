using System;
using System.Runtime.Serialization;

namespace UserServiceLibrary.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a generated user identifier already exists.  
    /// </summary>
    [Serializable]
    public class UserIdAlreadyExistsException : UserServiceException
    {
        /// <summary>
        /// Initializes a new instance of the UserIdAlreadyExistsException class.
        /// </summary>
        public UserIdAlreadyExistsException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the UserIdAlreadyExistsException class with a specified message.
        /// </summary>
        /// <param name="message">Message of exception.</param>
        public UserIdAlreadyExistsException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the UserIdAlreadyExistsException class with a specified format and args.
        /// </summary>
        /// <param name="format">Format of exception.</param>
        /// <param name="args">Args of exception.</param>
        public UserIdAlreadyExistsException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        /// <summary>
        /// Initializes a new instance of the UserIdAlreadyExistsException class with a specified message and inner exception.
        /// </summary>
        /// <param name="message">Message of exception</param>
        /// <param name="innerException">Inner exception of exception</param>
        public UserIdAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the UserIdAlreadyExistsException class with a specified format, args and inner exception.
        /// </summary>
        /// <param name="format">Format of exception.</param>
        /// <param name="innerException">Inner exception of exception.</param>
        /// <param name="args">Args of exception.</param>
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
