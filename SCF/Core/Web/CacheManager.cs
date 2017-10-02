using System;
using System.Web;

namespace Peyton.Core.Web
{
    public class CacheManager
    {
        public static T Get<T>(string key)
        {
            var val = HttpRuntime.Cache[key];
            return ConvertExt.ChangeType<T>(val);
        }

        public static void Set<T>(string key, T obj)
        {
            HttpRuntime.Cache[key] = obj;
        }

        public static void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        public static bool Exist(string key)
        {
            return HttpRuntime.Cache[key] != null;
        }
    }
}