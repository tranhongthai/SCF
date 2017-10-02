using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Peyton.Core.Common
{
    public static class ValidationModelExt
    {
        public static void AddRange(this List<ValidationModel> obj, List<ValidationResult> data)
        {
            foreach (var item in data)
                obj.Add(item);
        }

        public static List<ValidationResult> Clone(this List<ValidationModel> obj)
        {
            var result = new List<ValidationResult>();
            foreach (var item in obj)
                result.Add(item);
            return result;
        }

        public static List<ValidationModel> Add(this List<ValidationModel> obj, string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
                obj.Add(new ValidationModel(message, "Unknown"));
            return obj;
        }

        public static List<ValidationModel> Add(this List<ValidationModel> obj, string message, string memberNames)
        {
            memberNames = StringExt.Get(memberNames);
            if (!string.IsNullOrWhiteSpace(message))
                obj.Add(new ValidationModel(message, new string[] { memberNames }));
            return obj;
        }


        public static List<ValidationModel> Add(this List<ValidationModel> obj, string message, string memberNames, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                obj.Add(message, memberNames);
            return obj;
        }

        public static List<ValidationModel> Add(this List<ValidationModel> obj, string message, string memberNames, bool value)
        {
            if (value)
                obj.Add(message, memberNames);
            return obj;
        }

        public static List<ValidationModel> Add(this List<ValidationModel> obj, string message, string memberNames, long value)
        {
            if (value == 0)
                obj.Add(message, memberNames);
            return obj;
        }

        public static List<ValidationModel> Add(this List<ValidationModel> obj, string message, string memberNames, long? value)
        {
            if (!value.HasValue)
                obj.Add(message, memberNames);
            return obj;
        }
    }
}