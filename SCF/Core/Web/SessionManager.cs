using System;
using System.Web;

namespace Peyton.Core.Web
{
    public class SessionManager
    {
        public static Guid ProfileId
        {
            get
            {
                //return Exist("ProfileId") ? Get<Guid>("ProfileId") : Guid.Empty;
                return new Guid("86A609F0-9FFE-4F95-B586-A59242DF349E");
            }
            set { Set("ProfileId", value); }
        }

        public static Guid ApplicationId
        {
            get { return Exist("ApplicationId") ? Get<Guid>("ApplicationId") : Guid.Empty; }
            set { Set("ApplicationId", value); }
        }

        public static T Get<T>(string key)
        {
            if (Exist(key))
                return (T) HttpContext.Current.Session[key];
            return Activator.CreateInstance<T>();
        }

        public static void Set<T>(string key, T val)
        {
            if(val == null)
                val = Activator.CreateInstance<T>();
            HttpContext.Current.Session[key] = val;
        }

        public static void Clear(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }

        public static bool Exist(string key)
        {
            if (HttpContext.Current.Session == null)
                return false;
            return HttpContext.Current.Session[key] != null;
        }
    }
}