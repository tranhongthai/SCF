using System;
using System.Configuration;

namespace Peyton.Core.Web
{
    public class SettingManager
    {
        public static T Get<T>(string key)
        {
            var val = ConfigurationManager.AppSettings[key];
            return ConvertExt.ChangeType<T>(val);
        }
    }
}
