using System;
using NLog;

namespace Logger
{
    public class NLogLogger : ILogger
    {
        private readonly NLog.Logger _logger;
        private static readonly Lazy<NLogLogger> Logger = new Lazy<NLogLogger>(() => new NLogLogger());

        public static NLogLogger Instance { get { return Logger.Value; } }

        private NLogLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception e, string message)
        {
            _logger.Error(e, message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }
    }
}
