using System;
using System.Runtime.Serialization;

namespace UserServiceLibrary.Exceptions
{
    /// <summary>
    /// The exception that is thrown when there was an attempt of commiting a service operation on an User instance that has invalid fields.  
    /// </summary>
    [Serializable]
    public class InvalidUserException : UserServiceException
    {
        /// <summary>
        /// Initializes a new instance of the InvalidUserException class.
        /// </summary>
        public InvalidUserException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the InvalidUserException class with a specified message.
        /// </summary>
        /// <param name="message">Message of exception.</param>
        public InvalidUserException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the InvalidUserException class with a specified format and args.
        /// </summary>
        /// <param name="format">Format of exception.</param>
        /// <param name="args">Args of exception.</param>
        public InvalidUserException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        /// <summary>
        /// Initializes a new instance of the InvalidUserException class with a specified message and inner exception.
        /// </summary>
        /// <param name="message">Message of exception</param>
        /// <param name="innerException">Inner exception of exception</param>
        public InvalidUserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the InvalidUserException class with a specified format, args and inner exception.
        /// </summary>
        /// <param name="format">Format of exception.</param>
        /// <param name="innerException">Inner exception of exception.</param>
        /// <param name="args">Args of exception.</param>
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
