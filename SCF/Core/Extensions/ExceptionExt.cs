using System.Linq;

// ReSharper disable once CheckNamespace

namespace System
{
    public static class ExceptionExt
    {
        public static string Details(this Exception exception, bool isFull = false)
        {
            if (!isFull)
                return exception.Message;

            if (exception == null)
                return "";
            var properties = exception.GetType().GetProperties();
            var fields = properties
                .Select(property => new
                {
                    property.Name,
                    Value = property.GetValue(exception, null)
                })
                .Select(x => string.Format(
                    "{0} = {1}",
                    x.Name,
                    x.Value != null ? x.Value.ToString() : string.Empty
                    ));
            var s = string.Join("\n", fields);
            s += "\n\nInner Exception : \n";
            s += Details(exception.InnerException);
            return s;
        }
    }
}