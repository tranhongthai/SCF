using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

// ReSharper disable once CheckNamespace

namespace System
{
    public static class EnumExt
    {
        public static string DisplayName(this Enum value)
        {
            var enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            var field = enumType.GetField(enumValue);
            var attrs = field.GetCustomAttributes(typeof (DisplayAttribute), false);
            return attrs.Length > 0 ? ((DisplayAttribute) attrs[0]).Name : field.Name;
        }

        public static byte Value(this Enum value)
        {
            return (byte) Convert.ChangeType(value, typeof (byte));
        }

        public static int GetCount(Type type)
        {
            return Enum.GetNames(type).Length;
        }

        public static List<SelectListItem> ToList(Type enumType)
        {
            var result = new List<SelectListItem>();
            foreach (var val in Enum.GetValues(enumType))
                result.Add(((byte) val).ToString(), (val as Enum).DisplayName());
            return result;
        }

        public static SelectList ToList(Type enumType, bool isBlank, string blankValue = "",
            string blankText = "--Select--")
        {
            var list = ToList(enumType).OrderBy(i => i.Value).ToList();
            if (isBlank)
                list.Add(blankValue, blankText);
            return new SelectList(list, "Value", "Text");
        }
    }
}