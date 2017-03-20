using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserServiceLibrary.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a requested user was not found in the user storage.  
    /// </summary>
    [Serializable]
    public class UserNotFoundException : UserServiceException
    {
        /// <summary>
        /// Initializes a new instance of the UserNotFoundException class.
        /// </summary>
        public UserNotFoundException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the UserNotFoundException class with a specified message.
        /// </summary>
        /// <param name="message">Message of exception.</param>
        public UserNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the UserNotFoundException class with a specified format and args.
        /// </summary>
        /// <param name="format">Format of exception.</param>
        /// <param name="args">Args of exception.</param>
        public UserNotFoundException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        /// <summary>
        /// Initializes a new instance of the UserNotFoundException class with a specified message and inner exception.
        /// </summary>
        /// <param name="message">Message of exception</param>
        /// <param name="innerException">Inner exception of exception</param>
        public UserNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the UserNotFoundException class with a specified format, args and inner exception.
        /// </summary>
        /// <param name="format">Format of exception.</param>
        /// <param name="innerException">Inner exception of exception.</param>
        /// <param name="args">Args of exception.</param>
        public UserNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }
    }
}
