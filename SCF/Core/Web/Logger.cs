using log4net;
using System;

namespace Peyton.Core
{
    public static class Logger
    {
        static Logger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        private static readonly ILog ILog = LogManager.GetLogger("SystemLogger");

        public static void LogError(object objectMessage, Exception ex)
        {
            ILog.Error(objectMessage, ex);
        }

        public static void LogError(object objectMessage)
        {
            ILog.Error(objectMessage);
        }

        public static void LogError(Exception ex)
        {
            ILog.Error(string.Format("Source: {0} - TargetSite: {1} - Message: {2} - StackTrace: {3}", ex.Source, ex.TargetSite, ex.Message, ex.StackTrace));
        }

        public static void LogInfo(string userName, string className, string methodName, string message)
        {
            const string content = "Username: {0} | Class: {1} | Method: {2} | Message: {3}";
            ILog.Info(string.Format(content, userName, className, methodName, message));
        }

        public static void LogInfo(object objectMessage)
        {
            ILog.Info(objectMessage);
        }

        public static void LogInfo(string message)
        {
            ILog.Info(message);
        }

    }
}
