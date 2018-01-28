using System;
using log4net;

namespace Peyton.Core.Extensions
{
    public static class Logger
    {
        private static readonly ILog Log = LogManager.GetLogger("SystemLogger");

        public static void LogError(object objectMessage, Exception ex)
        {
            Log.Error(objectMessage, ex);
        }

        public static void LogError(object objectMessage)
        {
            Log.Error(objectMessage);
        }

        public static void LogError(Exception ex)
        {
            Log.Error(string.Format("Source: {0} - TargetSite: {1} - Message: {2} - StackTrace: {3}", ex.Source,
                ex.TargetSite, ex.Message, ex.StackTrace));
        }

        public static void LogInfo(string userName, string className, string methodName, string message)
        {
            const string content = "Username: {0} | {1}/{2}: {3}";
            Log.Info(string.Format(content, userName, className, methodName, message));
        }

        public static void LogInfo(object objectMessage)
        {
            Log.Info(objectMessage);
        }

        public static void LogProfile(string message)
        {
            Log.Info(message);
        }
    }
}