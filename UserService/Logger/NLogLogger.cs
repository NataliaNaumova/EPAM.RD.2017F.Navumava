using System;
using NLog;

namespace Logger
{
    /// <summary>
    /// Provides methods for logging.
    /// </summary>
    [Serializable]
    public class NLogLogger : ILogger
    {
        #region Fields

        private readonly NLog.Logger _logger;
        private static readonly Lazy<NLogLogger> Logger = new Lazy<NLogLogger>(() => new NLogLogger());

        #endregion

        #region Properties

        /// <summary>
        /// Gets a singleton instance of NLog logger. 
        /// </summary>
        public static NLogLogger Instance { get { return Logger.Value; } }

        #endregion

        #region Constructors

        private NLogLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Logs a warn-level message.
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        /// <summary>
        /// Logs a error-level message.
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Error(string message)
        {
            _logger.Error(message);
        }

        /// <summary>
        /// Logs a warn-level exception and message.
        /// </summary>
        /// <param name="e">An exception to log</param>
        /// <param name="message">Message to log</param>
        public void Error(Exception e, string message)
        {
            _logger.Error(e, message);
        }

        /// <summary>
        /// Logs a info-level message.
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Info(string message)
        {
            _logger.Info(message);
        }

        #endregion
    }
}
