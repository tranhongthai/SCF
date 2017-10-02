// ReSharper disable once CheckNamespace

namespace System
{
    public class ConvertExt
    {
        public static T ChangeType<T>(object val)
        {
            if (val == null)
                return default(T);
            return (T) Convert.ChangeType(val, typeof (T));
        }

        public static T ChangeType<T>(object val, T defaultValue)
        {
            if (val == null)
                return defaultValue;
            return (T) Convert.ChangeType(val, typeof (T));
        }
    }
}