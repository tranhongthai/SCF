using System.Collections.Generic;

// ReSharper disable once CheckNamespace

namespace System.Web.Mvc
{
    public static class SelectListExt
    {
        public static void Add(this IList<SelectListItem> list, string value = "", string text = "",
            bool selected = false)
        {
            list.Insert(0, new SelectListItem {Value = value, Text = text, Selected = selected});
        }
    }
}