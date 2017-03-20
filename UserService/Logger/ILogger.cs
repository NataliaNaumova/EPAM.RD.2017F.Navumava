using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    /// <summary>
    /// Provides methods for logging.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs a warn-level message.
        /// </summary>
        /// <param name="message">Message to log</param>
        void Warn(string message);

        /// <summary>
        /// Logs a error-level message.
        /// </summary>
        /// <param name="message">Message to log</param>
        void Error(string message);

        /// <summary>
        /// Logs a warn-level exception and message.
        /// </summary>
        /// <param name="e">An exception to log</param>
        /// <param name="message">Message to log</param>
        void Error(Exception e, string message);

        /// <summary>
        /// Logs a info-level message.
        /// </summary>
        /// <param name="message">Message to log</param>
        void Info(string message);
    }
}
