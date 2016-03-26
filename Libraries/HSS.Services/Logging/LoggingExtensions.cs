using HSS.Models.CommonEnum;
using System;

namespace HSS.Services.Logging
{
    /// <summary>
    /// 日志扩展类
    /// </summary>
    public static class LoggingExtensions
    {
        public static void Debug(this ILogger logger, string message, LogSource source = LogSource.Web, Exception exception = null, int? customerId = null)
        {
            FilteredLog(logger, LogLevel.Debug, message, source, exception, customerId);
        }
        public static void Information(this ILogger logger, string message, LogSource source = LogSource.Web, Exception exception = null, int? customerId = null)
        {
            FilteredLog(logger, LogLevel.Information, message, source, exception, customerId);
        }
        public static void Warning(this ILogger logger, string message, LogSource source = LogSource.Web, Exception exception = null, int? customerId = null)
        {
            FilteredLog(logger, LogLevel.Warning, message, source, exception, customerId);
        }
        public static void Error(this ILogger logger, string message, LogSource source = LogSource.Web, Exception exception = null, int? customerId = null)
        {
            FilteredLog(logger, LogLevel.Error, message, source, exception, customerId);
        }
        public static void Fatal(this ILogger logger, string message, LogSource source = LogSource.Web, Exception exception = null, int? customerId = null)
        {
            FilteredLog(logger, LogLevel.Fatal, message, source, exception, customerId);
        }

        private static void FilteredLog(ILogger logger, LogLevel level, string message, LogSource source = LogSource.Web, Exception exception = null, int? customerId = null)
        {
            if (exception is System.Threading.ThreadAbortException)
                return;

            if (logger.IsEnabled(level))
            {
                string fullMessage = exception == null ? string.Empty : exception.ToString();
                logger.InsertLog(level, message, source, fullMessage, customerId);
            }
        }
    }
}
