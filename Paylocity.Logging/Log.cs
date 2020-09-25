using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.Logging
{
    public enum TracingLevel
    {
        ALL, DEBUG, INFO, WARN, ERROR, FATAL, OFF
    }
    public class Log
    {
        private static ILog _log = null;

        public static void Initialize(string configFile, string loggerName)
        {
            if (_log == null)
            {
                log4net.Config.XmlConfigurator.Configure(new FileInfo(configFile));
                _log = LogManager.GetLogger(loggerName);
            }
        }

        public static void LogMessage(TracingLevel Level, Exception exception)
        {
            LogMessage(Level, exception.Message);
        }
        public static void LogMessage(TracingLevel Level, string Message)
        {
            switch (Level)
            {
                case TracingLevel.DEBUG:
                    _log.Debug(Message);
                    break;

                case TracingLevel.INFO:
                    _log.Info(Message);
                    break;

                case TracingLevel.WARN:
                    _log.Warn(Message);
                    break;

                case TracingLevel.ERROR:
                    _log.Error(Message);
                    break;

                case TracingLevel.FATAL:
                    _log.Fatal(Message);
                    break;
            }
        }
    }
}
