using System.Web.Mvc;

// ReSharper disable once CheckNamespace

namespace System.Text
{
    public static class StringBuilderExt
    {
        public static MvcHtmlString ToHtml(this StringBuilder builder)
        {
            return builder.ToString().ToHtml();
        }
    }
}